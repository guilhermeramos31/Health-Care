namespace HealthCare.Models.HealthSituationEntity.Dto;

public class HealthSituationRequest
{
    public string Shortcoming { get; init; } = string.Empty;
    public bool Bedridden { get; init; }
    public bool WheelchairUser { get; init; }
    public bool Wanders { get; init; }
    public string Comorbidities { get; init; } = string.Empty;
    public string Historic { get; init; } = string.Empty;
}