namespace HealthCare.Models.EmployeeEntity.DTO;

public class LoginResponse
{
    public required EmployeeResponse Employee { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
