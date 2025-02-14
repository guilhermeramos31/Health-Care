using HealthCare.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using HealthCare.Infrastructure.Configurations.Authentication;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Encoding = System.Text.Encoding;

namespace HealthCare.Services;

public class TokenService(
    IManagerUow managerUow,
    IHttpContextAccessor accessor,
    IOptions<JwtSetting> jwtSetting)
    : ITokenService
{
    public async Task<string> GenerateAccessToken(Employee employee)
    {
        var employeeRoles = await managerUow.UserManager.GetRolesAsync(employee);

        var audience = jwtSetting.Value.Audience;
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Value.SecretKey));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
        };
        if (employeeRoles.Any())
        {
            foreach (var role in employeeRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        var context = accessor.Get();

        var token = new JwtSecurityToken(
            issuer: context.Request.Headers.Origin,
            claims: claims,
            audience: audience,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshToken()
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Value.SecretKey));

        var context = accessor.Get();

        var token = new JwtSecurityToken(
            context.Request.Headers.Origin,
            jwtSetting.Value.Audience,
            expires: DateTime.Now.AddMinutes(1440),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        );

        return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
    }

    public async Task CreateUserToken(Employee employee, string token)
    {
        var user = await managerUow.UserManager.SetAuthenticationTokenAsync(
            employee,
            jwtSetting.Value.Audience,
            "RefreshToken",
            token
        );

        if (!user.Succeeded)
        {
            throw new BadHttpRequestException("Failed to save Refresh Token to database.");
        }
    }

    public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken()
    {
        var token = accessor.GetToken();
        if (token == null) throw new BadHttpRequestException("Failed to get user token.");

        var validateParameters = jwtSetting.TokenValidationParams();
        return await Task.Run(() =>
        {
            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(token, validateParameters, out var validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return claimsPrincipal;
        });
    }

    public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParams)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return claimsPrincipal;
    }
}