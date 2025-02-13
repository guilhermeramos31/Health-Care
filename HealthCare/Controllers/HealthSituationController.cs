using HealthCare.Models.HealthSituationEntity.Dto;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthSituationController(IServiceUow serviceUow) : ControllerBase
{
    [HttpPost("[action]/{patientId}")]
    public async Task<IActionResult> Create(Guid patientId, HealthSituationRequest healthSituationRequest)
    {
        return Ok(await serviceUow.HealthSituationService!.Create(patientId, healthSituationRequest));
    }

    [HttpPut("[action]/{healthSituationId}")]
    public async Task<IActionResult> Update(Guid healthSituationId, HealthSituationRequest healthSituationRequest)
    {
        return Ok(await serviceUow.HealthSituationService!.Update(healthSituationId, healthSituationRequest));
    }

    [HttpDelete("[action]/{healthSituationId}")]
    public async Task<IActionResult> Delete(Guid healthSituationId)
    {
        await serviceUow.HealthSituationService!.Delete(healthSituationId);
        return NoContent();
    }

    [HttpGet("[action]/{healthSituationId}")]
    public async Task<IActionResult> Get(Guid healthSituationId)
    {
        return Ok(await serviceUow.HealthSituationService!.GetHealthSituationById(healthSituationId));
    }

    [HttpGet("[action]/{patientId}")]
    public async Task<IActionResult> GetAll(Guid patientId, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        return Ok(await serviceUow.HealthSituationService!.GetAllHealthSituations(patientId, pageSize, pageNumber));
    }
}