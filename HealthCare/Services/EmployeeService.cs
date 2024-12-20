using AutoMapper;
using HealthCare.Models.EntityEmployee;
using HealthCare.Models.EntityEmployee.DTO;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRoleService _employeeRoleService;
    private readonly IRepositoryUow _repositoryUow;
    private readonly ITokenService _tokenService;

    private readonly UserManager<Employee> _userManager;

    public EmployeeService( IMapper mapper, IEmployeeRoleService employeeRoleService, IRepositoryUow repositoryUow, ITokenService tokenService, UserManager<Employee> userManager )
    {
        _mapper = mapper;
        _employeeRoleService = employeeRoleService;
        _repositoryUow = repositoryUow;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<EmployeeResponse> CreateAsync( EmployeeRequest? request )
    {
        if (request == null) throw new ArgumentNullException( nameof( request ) );

        var employee = _mapper.Map<Employee>( request );

        var result = await _userManager.CreateAsync( employee, request.Password );
        if (!result.Succeeded) throw new BadHttpRequestException( "Failed to create user" );
        await _employeeRoleService.CreateAsync( employee, request.Role );

        return _mapper.Map<EmployeeResponse>( employee );
    }

    public async Task<LoginResponse> LoginAsync( LoginRequest? request )
    {
        if (request == null) throw new ArgumentNullException( nameof( request ) );

        var employee = await _userManager.Users.FirstOrDefaultAsync( employee => employee.Email == request.UserName );
        if (employee == null) throw new BadHttpRequestException( "Invalid username" );

        var password = await _userManager.CheckPasswordAsync( employee, request.Password );
        if (!password) throw new BadHttpRequestException( "Invalid password" );

        var refreshToken = await _tokenService.GenerateRefreshToken();

        var result = new LoginResponse()
        {
            Employee = _mapper.Map<EmployeeResponse>( employee ),
            AccessToken = await _tokenService.GenerateAccessToken( employee ),
            RefreshToken = refreshToken
        };

        return result;
    }
}
