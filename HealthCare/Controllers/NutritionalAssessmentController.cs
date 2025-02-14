using HealthCare.Models.NutritionalAssessmentEntity.Dto;
using HealthCare.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Controllers;

[ApiController]
[Route("[controller]")]
public class NutritionalAssessmentController(IServiceUow serviceUow) : ControllerBase
{
    [Authorize("Bearer")]
    [HttpPost("[action]/{patientId}")]
    public async Task<IActionResult> Create(Guid patientId, [FromBody] NutritionalAssessmentRequest request)
    {
        return Ok(await serviceUow.NutritionalAssessmentService!.Create(patientId, request));
    }

    [Authorize("Bearer")]
    [HttpPut("[action]/{patientId}")]
    public async Task<IActionResult> Update(Guid nutritionalAssessmentId,
        [FromBody] NutritionalAssessmentRequest request)
    {
        return Ok(await serviceUow.NutritionalAssessmentService!.Update(nutritionalAssessmentId, request));
    }

    [Authorize("Bearer")]
    [HttpDelete("[action]/{nutritionalAssessmentId}")]
    public async Task<IActionResult> Delete(Guid nutritionalAssessmentId)
    {
        await serviceUow.NutritionalAssessmentService!.Delete(nutritionalAssessmentId);
        return NoContent();
    }

    [Authorize("Bearer")]
    [HttpGet("{nutritionalAssessmentId}")]
    public async Task<IActionResult> Get(Guid nutritionalAssessmentId)
    {
        return Ok(await serviceUow.NutritionalAssessmentService!.GetNutritionalAssessmentById(nutritionalAssessmentId));
    }

    [Authorize("Bearer")]
    [HttpGet("[action]/{patientId}")]
    public async Task<IActionResult> All(Guid patientId, int page, int pageSize)
    {
        return Ok(await serviceUow.NutritionalAssessmentService!.GetAllNutritionalAssessments(patientId,
            page, pageSize));
    }
}