namespace PollBack.Shared.AppSettings
{
    public class SecuritySettings
    {
        public string Secret { get; set; } = String.Empty;
        public string Salt { get; set; } = String.Empty;
        public int RefeshTokenDaysLive { get; set; }
    }
}
