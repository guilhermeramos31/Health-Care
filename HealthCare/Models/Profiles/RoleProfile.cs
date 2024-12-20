using AutoMapper;
using HealthCare.Models.EntityRole.DTO;

namespace HealthCare.Models.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<EntityRole.Role, RoleRequest>().ReverseMap();
        CreateMap<EntityRole.Role, RoleResponse>().ReverseMap();
    }
}
