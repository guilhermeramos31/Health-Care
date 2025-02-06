using HealthCare.Models.EmployeeEntity.DTO;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IEmployeeService employeeService) : ControllerBase
{
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