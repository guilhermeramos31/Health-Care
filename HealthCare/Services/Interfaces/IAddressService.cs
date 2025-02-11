using HealthCare.Models.AddressEntity;
using HealthCare.Models.AddressEntity.Dto;

namespace HealthCare.Services.Interfaces;

public interface IAddressService
{
    Task<AddressDto> Add(AddressDto address);
    Address Update(AddressDto address);
}