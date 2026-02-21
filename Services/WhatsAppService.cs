using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nomor_Whatsapp_Sender.Services
{
    public class WhatsAppService
    {
        private readonly HttpClient _client;

        public WhatsAppService()
        {
            _client = new HttpClient();
        }

        /// <summary>
        /// Sends a WhatsApp message via the configured API URL template.
        /// Supports optional location parameter.
        /// </summary>
        public async Task<bool> SendMessageAsync(string phoneNumber, string message, string? location = null)
        {
            phoneNumber = CleanPhoneNumber(phoneNumber);

            string urlTemplate = Properties.Settings.Default.WhatsAppApiUrl;

            if (string.IsNullOrWhiteSpace(urlTemplate))
            {
                urlTemplate = "http://localhost:3111/send?number={phone}&message={message}";
            }

            string url = urlTemplate
                .Replace("{phone}", phoneNumber)
                .Replace("{message}", Uri.EscapeDataString(message));

            if (!string.IsNullOrWhiteSpace(location))
            {
                // Remove spaces around the comma for clean coords
                string cleanLocation = location.Replace(" ", "");
                url += $"&location={Uri.EscapeDataString(cleanLocation)}";
            }

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<dynamic>(responseBody);

                if (result?.success != null)
                {
                    return (bool)result.success;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cleans a phone number: strips non-digits, removes leading 0, prepends Iraq country code 964.
        /// </summary>
        public static string CleanPhoneNumber(string phoneNumber)
        {
            string digits = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (digits.StartsWith("0"))
            {
                digits = digits.Substring(1);
            }

            if (!digits.StartsWith("964"))
            {
                digits = "964" + digits;
            }

            return digits;
        }
    }
}
