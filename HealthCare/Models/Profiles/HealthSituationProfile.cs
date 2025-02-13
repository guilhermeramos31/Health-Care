using AutoMapper;
using HealthCare.Models.HealthSituationEntity;
using HealthCare.Models.HealthSituationEntity.Dto;

namespace HealthCare.Models.Profiles;

public class HealthSituationProfile : Profile
{
    public HealthSituationProfile()
    {
        CreateMap<HealthSituation, HealthSituationRequest>().ReverseMap();
        CreateMap<HealthSituation, HealthSituationResponse>().ReverseMap();
    }
}