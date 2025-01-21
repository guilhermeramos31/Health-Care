namespace HealthCare.Configurations.Jwt.Interfaces;

public interface IJwt
{
    Task<JwtBody> GetBody();
}