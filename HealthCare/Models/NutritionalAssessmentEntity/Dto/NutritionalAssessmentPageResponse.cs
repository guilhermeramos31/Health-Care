namespace HealthCare.Models.NutritionalAssessmentEntity.Dto;

public class NutritionalAssessmentPageResponse
{
    public IList<NutritionalAssessmentResponse> NutritionalAssessmentResponses { get; set; } =
        new List<NutritionalAssessmentResponse>();

    public int TotalCount { get; set; }
}