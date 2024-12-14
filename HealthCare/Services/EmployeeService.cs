using AutoMapper;
using HealthCare.Models.EntityEmployee;
using HealthCare.Models.EntityEmployee.DTO;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class EmployeeService( IRepositoryUow uow, IMapper mapper, IRoleService roleService ) : IEmployeeService
{
    private readonly IRepositoryUow _uow = uow;
    private readonly IMapper _mapper = mapper;
    private readonly IRoleService _roleService = roleService;

    public async Task<EmployeeResponseDTO> CreateAsync( EmployeeRequestDTO requestDTO )
    {
        var employee = _mapper.Map<Employee>( requestDTO );

        var roles = new List<string>();
        foreach (var rolesFor in requestDTO.RolesId)
        {
            roles.Add( rolesFor );
        }
        Console.WriteLine( roles );

        return _mapper.Map<EmployeeResponseDTO>( await _uow.EmployeeRepository.CreateAsync( employee ) );
    }

    public Task<EmployeeResponseDTO> GetByEmailAsync( string email )
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeResponseDTO> GetByIdAsync( Guid id )
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeResponseDTO> GetByUserNameAsync( string userName )
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeResponseDTO> GetByUserNameOrEmailAsync( string userNameOrEmail )
    {
        throw new NotImplementedException();
    }
}
