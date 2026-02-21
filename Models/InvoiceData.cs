using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nomor_Whatsapp_Sender.Models
{
    public class InvoiceItem
    {
        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; } = string.Empty;

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("amount")]
        public string Amount { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("payment_method")]
        public string PaymentMethod { get; set; } = string.Empty;

        [JsonProperty("paid")]
        public string Paid { get; set; } = string.Empty;
    }

    public class UserInvoiceResult
    {
        public List<UserData> Users { get; set; } = new List<UserData>();
        public List<InvoiceItem> Invoices { get; set; } = new List<InvoiceItem>();
    }
}
