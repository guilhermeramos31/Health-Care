namespace HealthCare.Infrastructure.Configurations.Jwt.Interfaces;

public interface IJwt
{
    Task<JwtBody> GetBody();
}