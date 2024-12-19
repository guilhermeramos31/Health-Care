using AutoMapper;
using HealthCare.Models.EntityEmployee;
using HealthCare.Models.EntityEmployee.DTO;
using HealthCare.Models.EntityRole;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Role = HealthCare.Models.EntityRole.Enum.Role;

namespace HealthCare.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRoleService _employeeRoleService;
    private readonly IRepositoryUow _repositoryUow;

    private readonly UserManager<Employee> _userManager;

    public EmployeeService( IMapper mapper, IEmployeeRoleService employeeRoleService, IRepositoryUow repositoryUow, UserManager<Employee> userManager )
    {
        _mapper = mapper;
        _employeeRoleService = employeeRoleService;
        _repositoryUow = repositoryUow;
        _userManager = userManager;
    }

    public async Task<EmployeeResponse> CreateAsync( EmployeeRequest? request )
    {
        if (request == null) throw new ArgumentNullException( nameof( request ) );

        var employee = _mapper.Map<Employee>( request );

        var result = await _userManager.CreateAsync( employee, request.Password );
        if (!result.Succeeded) throw new BadHttpRequestException( "Failed to create user" );

        await _employeeRoleService.CreateAsync( employee, request.Role );

        var response = _mapper.Map<EmployeeResponse>(employee);

        return response;
    }
}
