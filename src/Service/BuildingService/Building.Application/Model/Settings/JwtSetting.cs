namespace Building.Application.Model.Settings
{
    public class JwtSetting
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
    }
}
