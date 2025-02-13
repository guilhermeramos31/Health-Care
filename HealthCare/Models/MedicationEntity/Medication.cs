using HealthCare.Models.Base;
using HealthCare.Models.PatientEntity;

namespace HealthCare.Models.MedicationEntity;

public class Medication : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public TimeSpan Time { get; set; }
    public int Period { get; set; }
    public string Quantity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
}