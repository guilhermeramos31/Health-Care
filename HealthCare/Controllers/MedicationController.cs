using HealthCare.Models.MedicationEntity.Dto;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class MedicationController(IServiceUow serviceUow) : ControllerBase
{
    [Authorize(policy: "Bearer")]

    [HttpPost("[action]/{patientId}")]
    public async Task<IActionResult> Create(Guid patientId, [FromBody] MedicationRequest medication)
    {
        return Ok(await serviceUow.MedicationService!.Create(patientId, medication));
    }

    [Authorize(policy: "Bearer")]
    [HttpPut("[action]/{medicationId}")]
    public async Task<IActionResult> Update(Guid medicationId, [FromBody] MedicationRequest medication)
    {
        return Ok(await serviceUow.MedicationService!.Update(medicationId, medication));
    }

    [Authorize(policy: "Bearer")]
    [HttpDelete("[action]/{medicationId}")]
    public async Task<IActionResult> Delete(Guid medicationId)
    {
        await serviceUow.MedicationService!.Delete(medicationId);
        return NoContent();
    }

    [Authorize(policy: "Bearer")]
    [HttpGet("{medicationId}")]
    public async Task<IActionResult> Medication(Guid medicationId)
    {
        return Ok(await serviceUow.MedicationService!.GetMedicationById(medicationId));
    }

    [Authorize(policy: "Bearer")]
    [HttpGet("[action]/{patientId}")]
    public async Task<IActionResult> All(Guid patientId, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        return Ok(await serviceUow.MedicationService!.GetAllMedications(patientId, pageSize, pageNumber));
    }
}