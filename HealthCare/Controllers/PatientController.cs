using HealthCare.Models.PatientEntity.Dto;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(IServiceUow serviceUow) : ControllerBase
{
    [Authorize("Bearer")]
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(PatientRequestDto patientRequestDto)
    {
        return Ok(await serviceUow.PatientService!.Create(patientRequestDto));
    }

    [Authorize("Bearer")]
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(PatientRequestDto patientRequestDto)
    {
        return Ok(await serviceUow.PatientService!.Update(patientRequestDto));
    }

    [Authorize("Bearer")]
    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(string cpf)
    {
        await serviceUow.PatientService!.Delete(cpf);
        return NoContent();
    }
}