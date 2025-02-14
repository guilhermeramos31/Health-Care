using HealthCare.Models.Base;
using HealthCare.Models.PatientEntity;

namespace HealthCare.Models.HealthSituationEntity;

public class HealthSituation : BaseEntity
{
    public string Shortcoming { get; init; } = string.Empty;
    public bool Bedridden { get; init; }
    public bool WheelchairUser { get; init; }
    public bool Wanders { get; init; }
    public string Comorbidities { get; init; } = string.Empty;
    public string Historic { get; init; } = string.Empty;

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
}