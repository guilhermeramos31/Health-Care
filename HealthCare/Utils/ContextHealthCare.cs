using System.Security.Claims;
using HealthCare.Infrastructure.Configurations.Authentication;
using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace HealthCare.Utils;

public static class ContextHealthCare
{
    public static HttpContext Get(this IHttpContextAccessor accessor)
    {
        return accessor.HttpContext ??
               throw new InvalidOperationException("Context is missing.");
    }

    public static string GetToken(this IHttpContextAccessor accessor)
    {
        var token = accessor.Get().Request.Headers.Authorization.ToString().Substring("Bearer ".Length).Trim();
        if (string.IsNullOrEmpty(token)) throw new BadHttpRequestException("Missing token.");
        return token;
    }

    public static async Task<Employee> GetEmployee(this IHttpContextAccessor accessor, IOptions<JwtSetting> jwtSetting,
        ITokenService tokenService, IManagerUow uowManager)
    {
        var token = accessor.GetToken();

        var tokenValidation = tokenService.ValidateToken(token, jwtSetting.TokenValidationParams());
        if (tokenValidation == null) throw new UnauthorizedAccessException("User not Authenticated.");

        var idClaims = tokenValidation.FindFirstValue(ClaimTypes.NameIdentifier);
        if (idClaims == null) throw new BadHttpRequestException("Claims invalid.");

        var userById = await uowManager.UserManager.FindByIdAsync(idClaims);
        if (userById == null) throw new BadHttpRequestException("User not found");

        return userById;
    }

    public static T GetSettings<T>(this IConfiguration configuration, string settingsKey)
    {
        return configuration.GetRequiredSection(settingsKey).Get<T>()
               ?? throw new InvalidOperationException("Settings are not configured.");
    }
}