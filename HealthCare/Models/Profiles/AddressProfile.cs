using AutoMapper;
using HealthCare.Models.AddressEntity;
using HealthCare.Models.AddressEntity.Dto;

namespace HealthCare.Models.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}