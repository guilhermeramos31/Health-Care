using HealthCare.Models.EmployeeEntity.DTO;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IServiceUow serviceUow) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok(await serviceUow.EmployeeService!.LoginAsync(request));
    }

    [Authorize("Bearer")]
    [HttpGet("[action]")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        return Ok(await serviceUow.EmployeeService!.RefreshToken(refreshToken));
    }
}