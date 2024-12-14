using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HealthCare.Configurations.Jwt;

public static class JwtSettings
{
    public static void AddAuthenticationJwt( this WebApplicationBuilder builder )
    {
        var jwtBody = builder.Configuration.GetSection( "JwtSettings" ).Get<JwtBody>()
                      ?? throw new InvalidOperationException( "JWT settings are not configured." );

        builder.Services.AddAuthentication( options =>
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
                ValidIssuer = builder.Configuration[ jwtBody.Issuer ],
                ValidAudience = builder.Configuration[ jwtBody.Audience ],
                IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( jwtBody.SecretKey ) )
            };
        } );
    }
}
