using AutoMapper;
using HealthCare.Models.EntityEmployeeRole.Dto;
using Microsoft.AspNetCore.Identity;

namespace HealthCare.Models.Profiles;

public class EmployeeRoleProfile : Profile
{
    public EmployeeRoleProfile()
    {
        CreateMap<IdentityUserRole<Guid>, EmployeeRoleDTO>().ReverseMap();
    }
}
