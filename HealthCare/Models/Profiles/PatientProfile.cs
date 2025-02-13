using AutoMapper;
using HealthCare.Models.PatientEntity;
using HealthCare.Models.PatientEntity.Dto;

namespace HealthCare.Models.Profiles;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientResponseDto>().ReverseMap().PreserveReferences();
        CreateMap<Patient, PatientRequestDto>().ReverseMap().PreserveReferences();
    }
}