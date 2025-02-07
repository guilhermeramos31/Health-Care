using HealthCare.Infrastructure.Data.Context;
using HealthCare.Models.AddressEntity;
using HealthCare.Repositories.Interfaces;

namespace HealthCare.Repositories;

public class AddressRepository(HealthCareContext context) : IAddressRepository
{
    public async Task<Address> Create(Address newAddress)
    {
        var address = await context.Addresses.AddAsync(newAddress);
        return address.Entity;
    }

    public Address Update(Address updateAddress)
    {
        var address = context.Addresses.Update(updateAddress);
        return address.Entity;
    }
}