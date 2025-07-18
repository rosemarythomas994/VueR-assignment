namespace Revv_car_CQRS.Model
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;  
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiryMinutes { get; set; }
    }

}
