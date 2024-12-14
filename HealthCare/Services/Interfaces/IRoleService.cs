using HealthCare.Models.Role.DTO;

namespace HealthCare.Services.Interfaces;

public interface IRoleService
{
    Task<RoleResponseDTO> GetByIdAsync( Guid id);
}
