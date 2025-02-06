using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HealthCare.Infrastructure.Configurations.Jwt.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace HealthCare.Infrastructure.Configurations.Jwt;

public static class JwtSettings
{
    public static async Task AddAuthenticationJwt(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var jwt = await provider.GetRequiredService<IJwt>().GetBody();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = jwt.Audience,
                ValidIssuers = jwt.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
                IssuerValidator = (issuer, token, parameters) =>
                {
                    if (!jwt.Issuer.Contains(issuer))
                    {
                        throw new SecurityTokenInvalidIssuerException("Invalid issuer.");
                    }

                    return issuer;
                }
            };
        });
        
        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer",
                new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
        });
    }
}