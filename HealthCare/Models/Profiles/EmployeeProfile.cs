using AutoMapper;
using HealthCare.Models.EntityEmployee;
using HealthCare.Models.EntityEmployee.DTO;

namespace HealthCare.Models.Profiles;
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeRequest>().ReverseMap();
        CreateMap<Employee, EmployeeResponse>().ReverseMap();
    }
}
