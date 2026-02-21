using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nomor_Whatsapp_Sender.Models;

namespace Nomor_Whatsapp_Sender.Services
{
    public class SasService
    {
        /// <summary>
        /// Creates SASConnector instances for ALL enabled credentials and logs in to each.
        /// Returns only the connectors that logged in successfully.
        /// </summary>
        private async Task<List<SASConnector>> CreateAndLoginAllAsync()
        {
            var enabledCreds = CredentialManager.GetEnabled();
            if (enabledCreds.Count == 0)
                return new List<SASConnector>();

            var connectors = new List<SASConnector>();
            var loginTasks = new List<Task>();

            foreach (var cred in enabledCreds)
            {
                var connector = new SASConnector(cred.Host, cred.Username, cred.Password);
                connectors.Add(connector);
                loginTasks.Add(SafeLoginAsync(connector));
            }

            await Task.WhenAll(loginTasks);

            // Return only connectors that logged in successfully
            var successful = new List<SASConnector>();
            for (int i = 0; i < connectors.Count; i++)
            {
                if (loginTasks[i].IsCompletedSuccessfully)
                    successful.Add(connectors[i]);
            }

            return successful;
        }

        private async Task SafeLoginAsync(SASConnector connector)
        {
            try
            {
                await connector.LoginAsync();
            }
            catch
            {
                // Login failed for this credential — skip silently
            }
        }

        /// <summary>
        /// Replaces: get_Expire_506_510.php
        /// Fetches expiring users from ALL enabled credential scopes and merges results.
        /// </summary>
        public async Task<List<UserData>> GetExpiringUsersAsync(string status, int count = 500, int page = 1)
        {
            int statusValue;
            switch (status)
            {
                case "expiring_soon":
                    statusValue = 4;
                    break;
                case "expiring_today":
                    statusValue = 5;
                    break;
                default:
                    statusValue = 5;
                    break;
            }

            var apis = await CreateAndLoginAllAsync();
            if (apis.Count == 0) return new List<UserData>();

            var payload = new Dictionary<string, object>
            {
                { "page", page },
                { "count", count },
                { "columns", new[] { "username", "firstname", "lastname", "expiration", "parent_username", "name", "remaining_days", "phone", "address" } },
                { "status", statusValue }
            };

            var tasks = apis.Select(api => api.PostAsync("index/user", payload)).ToList();
            await Task.WhenAll(tasks);

            var result = new List<UserData>();
            foreach (var task in tasks)
            {
                ParseAndAddUsers(task.Result, result);
            }

            return result;
        }

        /// <summary>
        /// Replaces: get_dashboard_506_510.php
        /// Fetches dashboard widget data from ALL enabled credential scopes.
        /// Returns a dictionary keyed by credential name.
        /// </summary>
        public async Task<Dictionary<string, DashboardWidgets>> GetDashboardDataAsync()
        {
            var enabledCreds = CredentialManager.GetEnabled();
            var result = new Dictionary<string, DashboardWidgets>();

            if (enabledCreds.Count == 0) return result;

            var endpoints = new[]
            {
                "wd_users_count",
                "wd_users_active_count",
                "wd_users_online",
                "wd_users_expired_count"
            };

            var apis = await CreateAndLoginAllAsync();

            for (int i = 0; i < apis.Count && i < enabledCreds.Count; i++)
            {
                var widgets = await CollectWidgetDataAsync(apis[i], endpoints);
                result[enabledCreds[i].Name] = widgets;
            }

            return result;
        }

        private async Task<DashboardWidgets> CollectWidgetDataAsync(SASConnector api, string[] endpoints)
        {
            var widgets = new DashboardWidgets();
            var results = new Dictionary<string, string?>();

            foreach (var endpoint in endpoints)
            {
                string? response = await api.GetAsync($"widgetData/internal/{endpoint}");
                results[endpoint] = response;
            }

            widgets.UsersCount = ParseWidget(results.GetValueOrDefault("wd_users_count"));
            widgets.UsersActiveCount = ParseWidget(results.GetValueOrDefault("wd_users_active_count"));
            widgets.UsersOnline = ParseWidget(results.GetValueOrDefault("wd_users_online"));
            widgets.UsersExpiredCount = ParseWidget(results.GetValueOrDefault("wd_users_expired_count"));

            return widgets;
        }

        private WidgetValue ParseWidget(string? json)
        {
            if (string.IsNullOrEmpty(json)) return new WidgetValue();
            try
            {
                return JsonConvert.DeserializeObject<WidgetValue>(json) ?? new WidgetValue();
            }
            catch
            {
                return new WidgetValue();
            }
        }

        /// <summary>
        /// Replaces: SAS4-online-Disconnect.php
        /// Disconnects a user session by account ID. Tries all enabled credentials.
        /// </summary>
        public async Task<string?> DisconnectUserAsync(string acctId)
        {
            var apis = await CreateAndLoginAllAsync();

            foreach (var api in apis)
            {
                try
                {
                    string? response = await api.GetAsync($"user/disconnect/acctid/{acctId}");
                    if (!string.IsNullOrEmpty(response)) return response;
                }
                catch { }
            }

            return null;
        }

        /// <summary>
        /// Replaces: get_506_510_user_invoices.php (user search part)
        /// Searches users by username across ALL enabled credential scopes.
        /// </summary>
        public async Task<List<UserData>> SearchUsersAsync(string search, int count = 500, int page = 1)
        {
            var apis = await CreateAndLoginAllAsync();
            if (apis.Count == 0) return new List<UserData>();

            var payload = new Dictionary<string, object>
            {
                { "page", page },
                { "count", count },
                { "sortBy", "username" },
                { "direction", "asc" },
                { "search", search },
                { "columns", new[] { "n_row", "idx", "username", "firstname", "lastname", "expiration", "parent_username", "name", "balance", "loan_balance", "group_name", "traffic", "remaining_days", "phone", "address", "contract_id" } }
            };

            var tasks = apis.Select(api => api.PostAsync("index/user", payload)).ToList();
            await Task.WhenAll(tasks);

            var result = new List<UserData>();
            foreach (var task in tasks)
            {
                ParseAndAddUsers(task.Result, result);
            }

            return result;
        }

        /// <summary>
        /// Replaces: get_506_510_user_invoices.php (full flow)
        /// Searches for a user across all scopes, then fetches invoices from the correct scope.
        /// </summary>
        public async Task<UserInvoiceResult> SearchUserAndInvoicesAsync(string search, int count = 500, int page = 1)
        {
            var apis = await CreateAndLoginAllAsync();
            if (apis.Count == 0) return new UserInvoiceResult();

            var payload = new Dictionary<string, object>
            {
                { "page", page },
                { "count", count },
                { "sortBy", "username" },
                { "direction", "asc" },
                { "search", search },
                { "columns", new[] { "n_row", "idx", "username", "firstname", "lastname", "expiration", "parent_username", "name", "balance", "loan_balance", "group_name", "traffic", "remaining_days", "phone", "address", "contract_id" } }
            };

            // Track which users came from which API
            var apiUserMap = new Dictionary<SASConnector, List<UserData>>();
            var tasks = new List<(SASConnector api, Task<string?> task)>();

            foreach (var api in apis)
            {
                tasks.Add((api, api.PostAsync("index/user", payload)));
            }

            await Task.WhenAll(tasks.Select(t => t.task));

            var allUsers = new List<UserData>();
            SASConnector? sourceApi = null;

            foreach (var (api, task) in tasks)
            {
                var users = new List<UserData>();
                ParseAndAddUsers(task.Result, users);
                if (users.Any() && sourceApi == null)
                    sourceApi = api;
                allUsers.AddRange(users);
            }

            if (!allUsers.Any() || sourceApi == null)
                return new UserInvoiceResult();

            int userId = allUsers[0].Id;

            var invoicePayload = new Dictionary<string, object>
            {
                { "page", 1 },
                { "count", 10 },
                { "sortBy", "created_at" },
                { "direction", "desc" },
                { "search", "" },
                { "columns", new[] { "invoice_number", "created_at", "type", "amount", "description", "username", "payment_method", "paid" } }
            };

            string? invoiceRes = await sourceApi.PostAsync($"index/UserInvoices/{userId}", invoicePayload);
            var invoices = new List<InvoiceItem>();
            if (!string.IsNullOrEmpty(invoiceRes))
            {
                try
                {
                    var parsed = JObject.Parse(invoiceRes);
                    var dataToken = parsed["data"];
                    if (dataToken != null)
                    {
                        invoices = dataToken.ToObject<List<InvoiceItem>>() ?? new List<InvoiceItem>();
                    }
                }
                catch { }
            }

            return new UserInvoiceResult
            {
                Users = allUsers,
                Invoices = invoices
            };
        }

        /// <summary>
        /// Replaces: get_cards_506_510.php (series listing)
        /// Fetches series data from ALL enabled credential scopes.
        /// </summary>
        public async Task<List<SeriesItem>> GetSeriesAsync(string search = "", int count = 500, int page = 1)
        {
            var apis = await CreateAndLoginAllAsync();
            if (apis.Count == 0) return new List<SeriesItem>();

            var payload = new Dictionary<string, object>
            {
                { "page", page },
                { "count", count },
                { "sortBy", "id" },
                { "direction", "asc" },
                { "search", search }
            };

            var tasks = apis.Select(api => api.PostAsync("index/series", payload)).ToList();
            await Task.WhenAll(tasks);

            var result = new List<SeriesItem>();
            foreach (var task in tasks)
            {
                ParseAndAdd<SeriesItem>(task.Result, result);
            }

            return result;
        }

        /// <summary>
        /// Replaces: get_cards_506_510.php (card listing for a specific series)
        /// Fetches cards from ALL enabled credential scopes.
        /// </summary>
        public async Task<List<CardItem>> GetCardsAsync(string seriesId, int count = 500, int page = 1)
        {
            var apis = await CreateAndLoginAllAsync();
            if (apis.Count == 0) return new List<CardItem>();

            var payload = new Dictionary<string, object>
            {
                { "page", page },
                { "count", count },
                { "columns", new[] { "id", "serialnumber", "pin", "username", "password", "used_at" } }
            };

            var tasks = apis.Select(api => api.PostAsync($"index/card/{seriesId}", payload)).ToList();
            await Task.WhenAll(tasks);

            var result = new List<CardItem>();
            foreach (var task in tasks)
            {
                ParseAndAdd<CardItem>(task.Result, result);
            }

            return result;
        }

        /// <summary>
        /// Replaces: get_cards_506_510.php (pin_search flow)
        /// Searches for a PIN across ALL enabled credential scopes.
        /// </summary>
        public async Task<List<CardItem>> SearchByPinAsync(string pinSearch, int count = 500, int page = 1)
        {
            var apis = await CreateAndLoginAllAsync();
            if (apis.Count == 0) return new List<CardItem>();

            var result = new List<CardItem>();

            foreach (var api in apis)
            {
                try
                {
                    var searchPayload = new Dictionary<string, object>
                    {
                        { "page", 1 },
                        { "count", 10 },
                        { "sortBy", "series_date" },
                        { "direction", "desc" },
                        { "search", pinSearch },
                        { "columns", new[] { "series_date", "series", "type", "value", "qty", "used", "username", "name", "expiration", "owner_details" } }
                    };

                    string? seriesRes = await api.PostAsync("index/series", searchPayload);
                    if (string.IsNullOrEmpty(seriesRes)) continue;

                    var seriesParsed = JObject.Parse(seriesRes);
                    var dataArray = seriesParsed["data"] as JArray;
                    if (dataArray == null || !dataArray.Any()) continue;

                    var seriesData = dataArray[0].ToObject<SeriesItem>();
                    if (seriesData == null) continue;

                    var cardPayload = new Dictionary<string, object>
                    {
                        { "page", page },
                        { "count", count },
                        { "columns", new[] { "id", "serialnumber", "pin", "username", "password", "used_at" } }
                    };

                    string? cardRes = await api.PostAsync($"index/card/{seriesData.Series}", cardPayload);
                    if (string.IsNullOrEmpty(cardRes)) continue;

                    var cardParsed = JObject.Parse(cardRes);
                    var cardArray = cardParsed["data"] as JArray;
                    if (cardArray == null) continue;

                    var cards = cardArray.ToObject<List<CardItem>>() ?? new List<CardItem>();

                    foreach (var card in cards)
                    {
                        if (card.Pin != null && card.Pin.Contains(pinSearch))
                        {
                            card.SeriesInfo = seriesData;
                            result.Add(card);
                        }
                    }
                }
                catch { }
            }

            return result;
        }

        // --- Private helpers ---

        private void ParseAndAddUsers(string? json, List<UserData> target)
        {
            if (string.IsNullOrEmpty(json)) return;
            try
            {
                var parsed = JsonConvert.DeserializeObject<Root>(json);
                if (parsed?.Data != null) target.AddRange(parsed.Data);
            }
            catch { }
        }

        private void ParseAndAdd<T>(string? json, List<T> target)
        {
            if (string.IsNullOrEmpty(json)) return;
            try
            {
                var parsed = JObject.Parse(json);
                var dataToken = parsed["data"];
                if (dataToken != null)
                {
                    var items = dataToken.ToObject<List<T>>();
                    if (items != null) target.AddRange(items);
                }
            }
            catch { }
        }
    }
}
