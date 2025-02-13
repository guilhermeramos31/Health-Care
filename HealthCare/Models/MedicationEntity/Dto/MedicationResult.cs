namespace HealthCare.Models.MedicationEntity.Dto;

public class MedicationResult
{
    public Guid id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan Time { get; set; }
    public int Period { get; set; }
    public string Quantity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}