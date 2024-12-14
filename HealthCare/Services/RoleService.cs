using AutoMapper;
using HealthCare.Models.Role.DTO;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class RoleService( IRepositoryUow uow, IMapper mapper ) : IRoleService
{
    private readonly IRepositoryUow _uow = uow;
    private readonly IMapper _mapper = mapper;

    public async Task<RoleResponseDTO> GetByIdAsync( Guid id )
    {
        var role = await _uow.RoleRepository.GetByIdAsync( id );
        return _mapper.Map<RoleResponseDTO>( role );
    }
}
