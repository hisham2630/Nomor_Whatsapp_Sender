using Newtonsoft.Json;

namespace Nomor_Whatsapp_Sender.Models
{
    public class SasCredential
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("host")]
        public string Host { get; set; } = string.Empty;

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = true;

        public override string ToString()
        {
            return $"{Name} ({Username}@{Host})";
        }
    }
}
