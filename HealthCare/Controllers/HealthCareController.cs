using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HealthCare.Models.EntityEmployee.DTO;

namespace HealthCare.Controllers;

[ApiController]
[Route( "[controller]" )]
public class HealthCareController( IEmployeeService employeeService, ITokenService tokenService, IEmployeeRoleService employeeRoleService ) : ControllerBase
{
    private readonly ITokenService _tokenService = tokenService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IEmployeeRoleService _employeeRoleService = employeeRoleService;

    [HttpPost( "register" )]
    public async Task<IActionResult> Register( EmployeeRequestDTO requestDTO )
    {
        var employee = await _employeeService.CreateAsync( requestDTO );
        return Ok( employee );
    }
}
