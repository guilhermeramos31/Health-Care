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
    [HttpPut("[action]/{patientId}")]
    public async Task<IActionResult> Update(Guid patientId, [FromBody] PatientUpdateDto updateDto)
    {
        return Ok(await serviceUow.PatientService!.Update(patientId, updateDto));
    }

    [Authorize("Bearer")]
    [HttpDelete("[action]{patientId}")]
    public async Task<IActionResult> Delete(Guid patientId)
    {
        await serviceUow.PatientService!.Delete(patientId);
        return NoContent();
    }

    [Authorize("Bearer")]
    [HttpGet("{patientId}")]
    public async Task<IActionResult> Patient(Guid patientId)
    {
        return Ok(await serviceUow.PatientService!.GetPatient(patientId));
    }

    [Authorize("Bearer")]
    [HttpGet("[action]")]
    public async Task<IActionResult> All([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(await serviceUow.PatientService!.Patients(pageNumber, pageSize));
    }
}