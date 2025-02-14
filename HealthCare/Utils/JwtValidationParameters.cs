using HealthCare.Infrastructure.Configurations.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HealthCare.Utils;

public static class JwtValidationParameters
{
    public static TokenValidationParameters TokenValidationParams(this IOptions<JwtSetting> options)
    {
        return new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuers = options.Value.Issuer,
            ValidAudience = options.Value.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = Encoding.SymmetricSecurityKey(options.Value.SecretKey)
        };
    }
}