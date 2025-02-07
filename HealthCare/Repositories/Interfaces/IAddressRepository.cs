using HealthCare.Models.AddressEntity;

namespace HealthCare.Repositories.Interfaces;

public interface IAddressRepository
{
    Task<Address> Create(Address address);
    Address Update(Address address);
}