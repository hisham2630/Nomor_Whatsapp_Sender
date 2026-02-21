using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nomor_Whatsapp_Sender.Models
{
    public class CardItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("serialnumber")]
        public string SerialNumber { get; set; } = string.Empty;

        [JsonProperty("pin")]
        public string Pin { get; set; } = string.Empty;

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        [JsonProperty("used_at")]
        public string UsedAt { get; set; } = string.Empty;

        [JsonProperty("series_info")]
        public SeriesItem? SeriesInfo { get; set; }
    }

    public class SeriesItem
    {
        [JsonProperty("series_date")]
        public string SeriesDate { get; set; } = string.Empty;

        [JsonProperty("series")]
        public string Series { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("used")]
        public int Used { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("expiration")]
        public string Expiration { get; set; } = string.Empty;

        [JsonProperty("owner_details")]
        public object? OwnerDetails { get; set; }
    }

    public class CardSeriesRoot
    {
        [JsonProperty("data")]
        public List<object> Data { get; set; } = new List<object>();
    }
}
