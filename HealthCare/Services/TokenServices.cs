using HealthCare.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using HealthCare.Models.EntityEmployee;
using HealthCare.Configurations.Jwt.Interfaces;
using HealthCare.Utils.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Services;

public class TokenServices(
    UserManager<Employee> userManager,
    IContextApi contextApi,
    IJwt jwt)
    : ITokenService
{
    public async Task<string> GenerateAccessToken(Employee employee)
    {
        var jwtBody = await jwt.GetBody();

        var employeeRoles = await userManager.GetRolesAsync(employee);

        var rolesNames = new List<string>();
        foreach (var role in employeeRoles)
        {
            rolesNames.Add(role.ToString() ?? throw new ArgumentNullException(nameof(role)));
        }
        var audience = jwtBody.Audience;
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBody.SecretKey));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new("role", JsonSerializer.Serialize(rolesNames)),
        };

        var context = await contextApi.GetContextAsync();
        
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
        
        var jwtBody = await jwt.GetBody();


        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBody.SecretKey));

        var context = await contextApi.GetContextAsync();
        
        var token = new JwtSecurityToken(
            context.Request.Headers.Origin,
            jwtBody.Audience,
            expires: DateTime.Now.AddMinutes(1440),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        );

        return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
    }

    public async Task CreateUserToken(Employee employee, string token)
    {
        var jwtBody = await jwt.GetBody();
        
        var user = await userManager.SetAuthenticationTokenAsync(
            employee,
            jwtBody.Audience,
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
        var jwtBody = await jwt.GetBody();

        var token = await GetTokenByContext();
        if (token == null) throw new BadHttpRequestException("Failed to get user token.");

        var validateParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuers = jwtBody.Issuer,
            ValidAudience = jwtBody.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBody.SecretKey))
        };
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

    private async Task<string?> GetTokenByContext()
    {
        var context = await contextApi.GetContextAsync();
        return await Task.Run(() =>
        {
            var authorization = context.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                return authorization.Substring("Bearer ".Length).Trim();
            }

            return null;
        });
    }
}