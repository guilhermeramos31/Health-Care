using System.Globalization;
using AutoMapper;
using HealthCare.Models.EntityEmployee.DTO;
using HealthCare.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using HealthCare.Models.EntityEmployee;
using HealthCare.Configurations.Jwt;

namespace HealthCare.Services;

public class TokenServices( IConfiguration configuration, IMapper mapper ) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IMapper _mapper = mapper;

    public string GenerateAccessToken( Employee employee )
    {
        var jwt = _configuration.GetSection( "JwtSettings" ).Get<JwtBody>()
                    ?? throw new InvalidOperationException( "JWT settings are not configured." );

        var rolesNames = new List<string>();
        foreach (var role in employee.Roles)
        {
            rolesNames.Add( role.ToString() ?? throw new ArgumentNullException( nameof( role ) ) );
        }

        var issuer = jwt.Issuer;
        var audience = jwt.Audience;
        var secretKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( jwt.SecretKey ) );
        var credentials = new SigningCredentials( secretKey, SecurityAlgorithms.HmacSha256 );

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
            new (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new ("role",  JsonSerializer.Serialize(rolesNames)),
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
