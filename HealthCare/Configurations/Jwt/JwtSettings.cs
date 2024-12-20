using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HealthCare.Configurations.Jwt;

public static class JwtSettings
{
    public static IServiceCollection AddAuthenticationJwt( this IServiceCollection services )
    {
        var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();

        var jwtBody = configuration.GetSection( "JwtSettings" ).Get<JwtBody>()
                      ?? throw new InvalidOperationException( "JWT settings are not configured." );

        services.AddAuthentication( options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        } ).AddJwtBearer( options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration[ jwtBody.Issuer ],
                ValidAudience = configuration[ jwtBody.Audience ],
                IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( jwtBody.SecretKey ) )
            };
        } );

        return services;
    }
}
