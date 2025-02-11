using AutoMapper;
using HealthCare.Infrastructure.Configurations.Jwt.Interfaces;
using HealthCare.Infrastructure.Managers.Interfaces;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.EmployeeEntity.DTO;
using HealthCare.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Services;

public class EmployeeService(
    IMapper mapper,
    ITokenService tokenService,
    IManagerUow managerUow,
    IEmployeeRoleService employeeRoleService,
    IJwt jwt)
    : IEmployeeService
{
    public async Task<EmployeeResponse> CreateAsync(EmployeeRequest? request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var employee = mapper.Map<Employee>(request);

        var result = await managerUow.UserManager.CreateAsync(employee, request.Password);
        if (!result.Succeeded) throw new BadHttpRequestException("Failed to create user");
        await employeeRoleService.CreateAsync(employee, request.Role);

        return mapper.Map<EmployeeResponse>(employee);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest? request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var employee =
            await managerUow.UserManager.Users.FirstOrDefaultAsync(employee => employee.Email == request.UserName);
        if (employee == null) throw new BadHttpRequestException("Invalid username");

        var password = await managerUow.UserManager.CheckPasswordAsync(employee, request.Password);
        if (!password) throw new BadHttpRequestException("Invalid password");

        var refreshToken = await tokenService.GenerateRefreshToken();
        await tokenService.CreateUserToken(employee, refreshToken);

        return new()
        {
            Employee = mapper.Map<EmployeeResponse>(employee),
            AccessToken = await tokenService.GenerateAccessToken(employee),
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginResponse> RefreshToken(string refreshToken)
    {
        var principal = await tokenService.GetPrincipalFromExpiredToken();
        var subject = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        var jwtBody = await jwt.GetBody();
        var user = await managerUow.UserManager.FindByIdAsync(subject) ??
                   throw new BadHttpRequestException("User not found!");

        var userToken = await managerUow.UserManager.GetAuthenticationTokenAsync(user, jwtBody.Audience, refreshToken);

        var newRefreshToken = await tokenService.GenerateRefreshToken();
        await tokenService.CreateUserToken(user, newRefreshToken);

        return new()
        {
            Employee = mapper.Map<EmployeeResponse>(user),
            AccessToken = await tokenService.GenerateAccessToken(user),
            RefreshToken = newRefreshToken
        };
    }

    public async Task<EmployeeResponse> FindById(string userId)
    {
        var user = await managerUow.UserManager.FindByIdAsync(userId) ??
                   throw new BadHttpRequestException("User not found!");
        return mapper.Map<EmployeeResponse>(user);
    }
}