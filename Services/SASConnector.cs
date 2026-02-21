using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using Nomor_Whatsapp_Sender.Helpers;

namespace Nomor_Whatsapp_Sender.Services
{
    public class SASConnector
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private string? _token;

        private readonly string _username;
        private readonly string _password;
        private readonly string _portal;

        private const string AES_PASSPHRASE = "abcdefghijuklmno0123456789012345";

        public SASConnector(string host, string username, string password, string portal = "acp")
        {
            _client = new HttpClient();
            _username = username;
            _password = password;
            _portal = portal;

            string sub = (portal == "ucp") ? "user" : "admin";
            _baseUrl = $"http://{host}/{sub}/api/index.php/api/";
        }

        public async Task LoginAsync()
        {
            string route = (_portal == "ucp") ? "auth/login" : "login";
            var payloadObj = new Dictionary<string, string>()
            {
                { "username", _username },
                { "password", _password }
            };

            string? jsonResponse = await PostAsync(route, payloadObj, encryptPayload: true, addAuthHeader: false);

            if (!string.IsNullOrEmpty(jsonResponse))
            {
                try
                {
                    using var doc = JsonDocument.Parse(jsonResponse);
                    if (doc.RootElement.TryGetProperty("token", out JsonElement tokenProp))
                    {
                        _token = tokenProp.GetString();
                    }
                }
                catch (Exception)
                {
                    // Could not parse JSON or no token property
                }
            }

            if (string.IsNullOrEmpty(_token))
            {
                throw new Exception("Login failed. No token in response.");
            }
        }

        public async Task<string?> PostAsync(string route, object payloadObj,
                                            bool encryptPayload = true,
                                            bool addAuthHeader = true)
        {
            string url = _baseUrl + route;

            string rawJson = JsonSerializer.Serialize(payloadObj);

            string finalPayload = encryptPayload
                ? AESHelper.Encrypt(rawJson, AES_PASSPHRASE)
                : rawJson;

            var toSend = new { payload = finalPayload };

            using var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Content = new StringContent(JsonSerializer.Serialize(toSend), Encoding.UTF8, "application/json");

            if (addAuthHeader && !string.IsNullOrEmpty(_token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }

            using var response = await _client.SendAsync(req);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<string?> GetAsync(string route)
        {
            string url = _baseUrl + route;

            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            if (!string.IsNullOrEmpty(_token))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }

            using var response = await _client.SendAsync(req);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
    }
}
