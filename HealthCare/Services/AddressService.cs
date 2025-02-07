using AutoMapper;
using HealthCare.Models.AddressEntity;
using HealthCare.Models.AddressEntity.Dto;
using HealthCare.Repositories.Interfaces;
using HealthCare.Services.Interfaces;

namespace HealthCare.Services;

public class AddressService(IRepositoryUow repositoryUow, IMapper mapper) : IAddressService
{
    public async Task<AddressDto> Add(AddressDto address)
    {
        return mapper.Map<AddressDto>(await repositoryUow.AddressRepository.Create(mapper.Map<Address>(address)));
    }

    public AddressDto Update(AddressDto address)
    {
        return mapper.Map<AddressDto>(repositoryUow.AddressRepository.Update(mapper.Map<Address>(address)));
    }
}