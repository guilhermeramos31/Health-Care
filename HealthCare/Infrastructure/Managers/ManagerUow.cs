using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.RoleEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HealthCare.Infrastructure.Managers;

public class ManagerUow(
    IUserStore<Employee> store,
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<Employee> passwordHasher,
    IEnumerable<IUserValidator<Employee>> userValidators,
    IEnumerable<IPasswordValidator<Employee>> passwordValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    IServiceProvider services,
    ILogger<UserManager<Employee>> logger,
    IRoleStore<Role> storeRole,
    IEnumerable<IRoleValidator<Role>> roleValidators,
    ILookupNormalizer keyNormalizerRole,
    IdentityErrorDescriber errorsRole,
    ILogger<RoleManager<Role>> loggerRole) : IManagerUow
{
    private UserManager<Employee>? _userManager;
    private RoleManager<Role>? _roleManager;

    public UserManager<Employee> UserManager => _userManager ??= new(store, optionsAccessor, passwordHasher,
        userValidators, passwordValidators, keyNormalizer, errors, services, logger);

    public RoleManager<Role> RoleManager =>
        _roleManager ??= new(storeRole, roleValidators, keyNormalizerRole, errorsRole, loggerRole);
}