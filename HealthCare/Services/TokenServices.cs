﻿using System.Globalization;
using AutoMapper;
using HealthCare.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using HealthCare.Models.EntityEmployee;
using HealthCare.Configurations.Jwt;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Services;

public class TokenServices( IConfiguration configuration, IMapper mapper, UserManager<Employee> userManager ) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<Employee> _userManager = userManager;

    public async Task<string> GenerateAccessToken( Employee employee )
    {
        var jwt = _configuration.GetSection( "JwtSettings" ).Get<JwtBody>()
                    ?? throw new InvalidOperationException( "JWT settings are not configured." );

        var employeeRoles = await _userManager.GetRolesAsync( employee );

        var rolesNames = new List<string>();
        foreach (var role in employeeRoles)
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

    public async Task<string> GenerateRefreshToken()
    {
        var jwt = _configuration.GetSection( "JwtSettings" ).Get<JwtBody>()
                  ?? throw new InvalidOperationException( "JWT settings are not configured." );

        var issuer = jwt.Issuer;
        var audience = jwt.Audience;
        var secretKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( jwt.SecretKey ) );
        var credentials = new SigningCredentials( secretKey, SecurityAlgorithms.HmacSha256 );

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddMinutes( 1440 ),
            signingCredentials: credentials
        );

        return await Task.FromResult( new JwtSecurityTokenHandler().WriteToken( token ) );
    }

    public async Task CreateUserToken( Employee employee, string token )
    {
        var jwt = _configuration.GetSection( "JwtSettings" ).Get<JwtBody>()
                  ?? throw new InvalidOperationException( "JWT settings are not configured." );

        var user = await _userManager.SetAuthenticationTokenAsync(
            employee,
            jwt.Audience,
            "RefreshToken",
            token
            );

        if (!user.Succeeded)
        {
            throw new BadHttpRequestException( "Failed to save Refresh Token to database." );
        }
    }
}
