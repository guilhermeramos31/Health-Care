using AutoMapper;
using HealthCare.Models.NutritionalAssessmentEntity;
using HealthCare.Models.NutritionalAssessmentEntity.Dto;

namespace HealthCare.Models.Profiles;

public class NutritionalAssessmentProfile : Profile
{
    public NutritionalAssessmentProfile()
    {
        CreateMap<NutritionalAssessment, NutritionalAssessmentResponse>().ReverseMap();
        CreateMap<NutritionalAssessment, NutritionalAssessmentRequest>().ReverseMap();
    }
}