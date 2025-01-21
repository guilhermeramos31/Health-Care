using HealthCare.Configurations.Jwt.Interfaces;

namespace HealthCare.Configurations.Jwt;

public class Jwt(IConfiguration configuration) : IJwt
{
    private readonly IConfiguration _configuration = configuration;

    public Task<JwtBody> GetBody()
    {
        return Task.Run(() =>
        {
            var jwt = _configuration.GetRequiredSection("JwtSettings").Get<JwtBody>()
                      ?? throw new InvalidOperationException("JWT settings are not configured.");
            return jwt;
        });
    }
}