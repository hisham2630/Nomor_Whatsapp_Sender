using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nomor_Whatsapp_Sender.Models
{
    public class Root
    {
        [JsonProperty("data")]
        public List<UserData> Data { get; set; } = new List<UserData>();
    }

    public class UserData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("firstname")]
        public string Firstname { get; set; } = string.Empty;

        [JsonProperty("lastname")]
        public string Lastname { get; set; } = string.Empty;

        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonProperty("expiration")]
        public string Expiration { get; set; } = string.Empty;

        [JsonProperty("last_online")]
        public string Last_Online { get; set; } = string.Empty;

        [JsonProperty("notes")]
        public string Notes { get; set; } = string.Empty;

        [JsonProperty("remaining_days")]
        public int Remaining_days { get; set; }

        [JsonProperty("parent_username")]
        public string Parent_Username { get; set; } = string.Empty;

        [JsonProperty("profile_details")]
        public ProfileDetails? Profile_Details { get; set; }

        // Extended fields used by invoice search
        [JsonProperty("balance")]
        public string Balance { get; set; } = string.Empty;

        [JsonProperty("loan_balance")]
        public string Loan_Balance { get; set; } = string.Empty;

        [JsonProperty("group_name")]
        public string Group_Name { get; set; } = string.Empty;

        [JsonProperty("traffic")]
        public string Traffic { get; set; } = string.Empty;

        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;

        [JsonProperty("contract_id")]
        public string Contract_Id { get; set; } = string.Empty;
    }

    public class ProfileDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("type")]
        public int Type { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
