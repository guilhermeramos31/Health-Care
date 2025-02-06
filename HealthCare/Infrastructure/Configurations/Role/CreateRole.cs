using Microsoft.AspNetCore.Identity;

namespace HealthCare.Configurations.Role;

public static class Roles
{
    public static async Task CreateRoles( this WebApplication app )
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Models.RoleEntity.Role>>();

        foreach (var role in Enum.GetValues<Models.RoleEntity.Enum.Role>())
        {
            if (!await roleManager.RoleExistsAsync( role.ToString() ))
                await roleManager.CreateAsync( new Models.RoleEntity.Role( role.ToString() ) );
        }
    }
}
