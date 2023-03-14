using FoodSaverWebApp.Entities;
using Postgrest;
using Postgrest.Responses;

namespace FoodSaverWebApp.Services
{
    public class AddressService : IAddressService
    {
        private readonly IDbConnection _connection;
        public AddressService(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public async Task<ICollection<Address>> GetAllAddresses()
        {
            var result = await _connection.AccessDatabase()
                .From<Address>()
                .Get();

            return result.Models;
        }

        public async Task<Address?> GetAddress(int addressId)
        {
            Address? result = await _connection.AccessDatabase()
                .From<Address>()
                .Where(x => x.AddressId == addressId)
                .Single();

            return result;
        }

        public async Task InsertAddress(Address address)
        {
            await _connection.AccessDatabase()
                .From<Address>()
                .Insert(address);
        }

        public async Task<Address?> ReturnAddressOnInsert(Address address)
        {
            ModeledResponse<Address> result =  await _connection.AccessDatabase()
                .From<Address>()
                .Insert(address, new QueryOptions{ Returning = QueryOptions.ReturnType.Representation});

            return result.Models[0];
        }

        public async Task UpdateAddress(Address address)
        {
            await _connection.AccessDatabase()
                .From<Address>()
                .Update(address);
        }

        public async Task DeleteAddress(int addressId)
        {
            await _connection.AccessDatabase()
                .From<Address>()
                .Where(x => x.AddressId == addressId)
                .Delete();
        }
    }
}