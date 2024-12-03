using HealthCare.Configurations;
using HealthCare.Models.EntityEmployee;
using HealthCare.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace HealthCare.Services;

public class TokenServices : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenServices( IConfiguration configuration )
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken( Employee employee )
    {
        var jwtSettings = _configuration.GetSection( "JwtSettings" ).Get<JwtSettings>()
                    ?? throw new InvalidOperationException( "JWT settings are not configured." );

        var rolesNames = new List<string>();
        foreach (var role in employee.RoleId)
        {
            rolesNames.Add( role.Name ?? throw new ArgumentNullException( nameof( employee )));
        }

        var issuer = jwtSettings.Issuer;
        var audience = jwtSettings.Audience;
        var secretKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( jwtSettings.SecretKey ));
        var credentials = new SigningCredentials( secretKey, SecurityAlgorithms.HmacSha256 );

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
            new (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new ("role",  JsonSerializer.Serialize( rolesNames )),
        };
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            claims: claims,
            audience: audience,
            expires: DateTime.Now.AddMinutes( 120 ),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken( token );
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }
}
