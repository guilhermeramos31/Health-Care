using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HealthCare.Models.EmployeeEntity.DTO;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController(IServiceUow serviceUow) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Register(EmployeeRequest request)
    {
        return Ok(await serviceUow.EmployeeService!.CreateAsync(request));
    }
}