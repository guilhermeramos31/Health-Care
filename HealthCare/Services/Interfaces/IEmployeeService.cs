using HealthCare.Models.EntityEmployee.DTO;

namespace HealthCare.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponseDTO> GetByIdAsync( Guid id );
    Task<EmployeeResponseDTO> GetByEmailAsync( string email );
    Task<EmployeeResponseDTO> GetByUserNameAsync( string userName );
    Task<EmployeeResponseDTO> GetByUserNameOrEmailAsync( string userNameOrEmail );
    Task<EmployeeResponseDTO> CreateAsync(EmployeeRequestDTO requestDTO);
}
