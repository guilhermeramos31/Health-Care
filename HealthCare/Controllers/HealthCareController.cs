using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HealthCare.Models.EntityEmployee.DTO;
using Microsoft.AspNetCore.Authorization;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCareController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(EmployeeRequest request)
    {
        return Ok(await employeeService.CreateAsync(request));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok(await employeeService.LoginAsync(request));
    }

    [Authorize("Bearer")]
    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        Console.WriteLine("ok");
        return Ok(await employeeService.RefreshToken(refreshToken));
    }
}