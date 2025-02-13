namespace HealthCare.Models.MedicationEntity.Dto;

public class MedicationRequest
{
    public string Name { get; set; } = string.Empty;
    public TimeSpan Time { get; set; } = TimeSpan.Zero;
    public int Period { get; set; }
    public string Quantity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}