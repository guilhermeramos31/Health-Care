namespace HealthCare.Models.NutritionalAssessmentEntity.Dto;

public class NutritionalAssessmentResponse
{
    public Guid Id { get; set; }
    public string Cb { get; set; } = string.Empty;
    public string Aj {get; set;} = string.Empty;
    public string Cp { get; set; } = string.Empty;
    public float EstimatedWeight { get; set; }
    public float EstimatedStature { get; set; }
    public float Imc { get; set; }
}