using HealthCare.Models.AddressEntity.Dto;

namespace HealthCare.Services.Interfaces;

public interface IAddressService
{
    Task<AddressDto> Add(AddressDto address);
    AddressDto Update(AddressDto address);
}