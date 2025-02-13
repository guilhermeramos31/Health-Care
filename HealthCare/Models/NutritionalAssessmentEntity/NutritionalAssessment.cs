using HealthCare.Models.Base;
using HealthCare.Models.PatientEntity;

namespace HealthCare.Models.NutritionalAssessmentEntity;

public class NutritionalAssessment : BaseEntity
{
    public string Cb { get; set; } = string.Empty;
    public string Aj {get; set;} = string.Empty;
    public string Cp { get; set; } = string.Empty;
    public float EstimatedWeight { get; set; }
    public float EstimatedStature { get; set; }
    public float Imc { get; set; }
    
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
}