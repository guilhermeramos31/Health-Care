using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HealthCare.Models.EmployeeEntity.DTO;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(EmployeeRequest request)
    {
        return Ok(await employeeService.CreateAsync(request));
    }
}