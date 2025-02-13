namespace HealthCare.Models.MedicationEntity.Dto;

public class MedicationPageResult
{
    public IList<MedicationResult> MedicationResults { get; set; } = new List<MedicationResult>();
    public int TotalCount { get; set; }
}