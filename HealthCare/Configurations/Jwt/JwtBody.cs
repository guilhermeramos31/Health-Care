namespace HealthCare.Configurations.Jwt;

public class JwtBody
{
    public string SecretKey { get; set; } = string.Empty;
    public ICollection<string> Issuer { get; set; } = [];
    public string Audience { get; set; } = string.Empty;
}