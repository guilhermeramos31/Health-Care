using HealthCare.Infrastructure.Configurations.Jwt.Interfaces;

namespace HealthCare.Infrastructure.Configurations.Jwt;

public class Jwt(IConfiguration configuration) : IJwt
{
    public Task<JwtBody> GetBody()
    {
        return Task.Run(() =>
        {
            var jwt = configuration.GetRequiredSection("JwtSettings").Get<JwtBody>()
                      ?? throw new InvalidOperationException("JWT settings are not configured.");
            return jwt;
        });
    }
}