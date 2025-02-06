using AutoMapper;
using HealthCare.Models.EmployeeEntity;
using HealthCare.Models.EmployeeEntity.DTO;

namespace HealthCare.Models.Profiles;
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeRequest>().ReverseMap();
        CreateMap<Employee, EmployeeResponse>().ReverseMap();
    }
}
