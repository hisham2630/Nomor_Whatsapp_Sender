using Newtonsoft.Json;

namespace Nomor_Whatsapp_Sender.Models
{
    public class DashboardResponse
    {
        [JsonProperty("506_users")]
        public DashboardWidgets? Users506 { get; set; }

        [JsonProperty("510_users")]
        public DashboardWidgets? Users510 { get; set; }
    }

    public class DashboardWidgets
    {
        [JsonProperty("wd_users_count")]
        public WidgetValue UsersCount { get; set; } = new WidgetValue();

        [JsonProperty("wd_users_active_count")]
        public WidgetValue UsersActiveCount { get; set; } = new WidgetValue();

        [JsonProperty("wd_users_online")]
        public WidgetValue UsersOnline { get; set; } = new WidgetValue();

        [JsonProperty("wd_users_expired_count")]
        public WidgetValue UsersExpiredCount { get; set; } = new WidgetValue();
    }

    public class WidgetValue
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("data")]
        public object? Data { get; set; }
    }
}
