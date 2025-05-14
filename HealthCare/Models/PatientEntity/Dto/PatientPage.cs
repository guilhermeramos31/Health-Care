namespace HealthCare.Models.PatientEntity.Dto;

public class PatientPage
{
    public List<Patient> Patients { get; init; } = new();
    public int TotalCount { get; init; }
}