namespace HealthCare.Models.PatientEntity.Dto;

public class PatientPageResult
{
    public List<PatientResponseDto> Patients { get; init; } = new();
    public int TotalCount { get; init; }
}