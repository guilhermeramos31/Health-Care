namespace HealthCare.Models.HealthSituationEntity.Dto;

public class HealthSituationPageResult
{
    public IList<HealthSituationResponse> HealthSituations { get; set; } = new List<HealthSituationResponse>();
    public int TotalCount { get; init; }
}