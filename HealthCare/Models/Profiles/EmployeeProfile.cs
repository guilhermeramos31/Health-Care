using AutoMapper;
using HealthCare.Models.EntityEmployee;
using HealthCare.Models.EntityEmployee.DTO;

namespace HealthCare.Models.Profiles;
public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeRequestDTO>().ReverseMap();
        CreateMap<Employee, EmployeeResponseDTO>().ReverseMap();
    }
}
