namespace PollBack.Core.AppSettings
{
    public class SecuritySettings
    {
        public static string Name = "SecuritySettings";
        public string Secret { get; set; } = String.Empty;
        public string Salt { get; set; } = String.Empty;
        public int RefeshTokenDaysLive { get; set; }
    }
}
