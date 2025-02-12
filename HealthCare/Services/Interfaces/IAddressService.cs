using HealthCare.Models.AddressEntity;
using HealthCare.Models.AddressEntity.Dto;

namespace HealthCare.Services.Interfaces;

public interface IAddressService
{
    Task<AddressDto> Add(AddressDto address);
    Task<Address> Update(Guid addressId, AddressDto updateAddress);
    Task Delete(Guid addressId);
}