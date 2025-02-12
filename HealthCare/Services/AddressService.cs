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

    public async Task<Address> Update(Guid addressId, AddressDto updateAddress)
    {
        var address = await repositoryUow.AddressRepository.GetAddress(addressId);
        if (address == null) throw new NullReferenceException("Address not found");
        address = mapper.Map(updateAddress, address);

        var addressUpdate = repositoryUow.AddressRepository.Update(mapper.Map<Address>(address));
        await repositoryUow.CommitAsync();

        return addressUpdate;
    }

    public async Task Delete(Guid addressId)
    {
        var addressToDelete = await repositoryUow.AddressRepository.GetAddress(addressId);
        if (addressToDelete == null) throw new NullReferenceException("Address not found");
        repositoryUow.AddressRepository.Delete(addressToDelete);

        await repositoryUow.CommitAsync();
    }
}