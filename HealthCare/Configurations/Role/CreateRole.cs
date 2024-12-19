using Microsoft.AspNetCore.Identity;

namespace HealthCare.Configurations.Role;

public static class Roles
{
    public static async Task CreateRoles( this WebApplication app )
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Models.EntityRole.Role>>();

        foreach (var role in Enum.GetValues<Models.EntityRole.Enum.Role>())
        {
            if (!await roleManager.RoleExistsAsync( role.ToString() ))
                await roleManager.CreateAsync( new Models.EntityRole.Role( role.ToString() ) );
        }
    }
}
