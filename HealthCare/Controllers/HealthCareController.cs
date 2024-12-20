using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HealthCare.Models.EntityEmployee.DTO;
using HealthCare.Models.EntityRole.Enum;

namespace HealthCare.Controllers;

[ApiController]
[Route( "[controller]" )]
public class HealthCareController( IEmployeeService employeeService, ITokenService tokenService, IEmployeeRoleService employeeRoleService ) : ControllerBase
{
    private readonly ITokenService _tokenService = tokenService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IEmployeeRoleService _employeeRoleService = employeeRoleService;


    [HttpPost( "register" )]
    public async Task<IActionResult> Register( EmployeeRequest request )
    {
        return Ok( await _employeeService.CreateAsync( request ) );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login( LoginRequest request )
    {
        return Ok( await _employeeService.LoginAsync( request ) );
    }
}
