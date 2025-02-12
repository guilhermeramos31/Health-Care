using AutoMapper;
using HealthCare.Models.RoleEntity;
using HealthCare.Models.RoleEntity.DTO;

namespace HealthCare.Models.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleRequest>().ReverseMap();
        CreateMap<Role, RoleResponse>().ReverseMap();
    }
}
