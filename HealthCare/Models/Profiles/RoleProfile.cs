using AutoMapper;
using HealthCare.Models.Role.DTO;

namespace HealthCare.Models.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<EntityRole.Role, RoleRequestDTO>().ReverseMap();
        CreateMap<EntityRole.Role, RoleResponseDTO>().ReverseMap();
    }
}
