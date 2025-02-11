using AutoMapper;
using HealthCare.Models.AddressEntity;
using HealthCare.Models.AddressEntity.Dto;
using HealthCare.Models.PatientEntity;
using HealthCare.Models.PatientEntity.Dto;

namespace HealthCare.Models.Profiles;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientResponseDto>().ReverseMap();
        CreateMap<Patient, PatientRequestDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}