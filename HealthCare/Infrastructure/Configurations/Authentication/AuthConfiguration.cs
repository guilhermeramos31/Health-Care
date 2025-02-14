using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.EmployeeEntity;
using Microsoft.AspNetCore.Identity;
using HealthCare.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace HealthCare.Infrastructure.Configurations.Authentication;

public static class AuthConfiguration
{
    public static void AddAuthenticationJwt(this IServiceCollection services)
    {
        var jwtSetting = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSetting>>();

        services.AddIdentity<Employee, Models.RoleEntity.Role>(employee =>
            {
                employee.Password.RequireDigit = true;
                employee.Password.RequireLowercase = true;
                employee.Password.RequireUppercase = true;
                employee.Password.RequireNonAlphanumeric = true;
            })
            .AddRoleManager<RoleManager<Models.RoleEntity.Role>>()
            .AddUserManager<UserManager<Employee>>()
            .AddEntityFrameworkStores<HealthCareContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        }).AddJwtBearer(options => { options.TokenValidationParameters = jwtSetting.TokenValidationParams(); });

        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer",
                new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
        });
    }
}