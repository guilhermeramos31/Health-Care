using AutoMapper;
using HealthCare.Models.MedicationEntity;
using HealthCare.Models.MedicationEntity.Dto;

namespace HealthCare.Models.Profiles;

public class MedicationProfile : Profile
{
    public MedicationProfile()
    {
        CreateMap<Medication, MedicationResult>().ReverseMap().PreserveReferences();
        CreateMap<Medication, MedicationRequest>().ReverseMap().PreserveReferences();
    }
}