using FoodSaverWebApp.Entities;

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
                .Where(x => x.Addressid == addressId)
                .Single();

            return result;
        }

        public async void InsertAddress(Address address)
        {
            await _connection.AccessDatabase()
                .From<Address>()
                .Insert(address);
        }

        public async void UpdateAddress(Address address)
        {
            await _connection.AccessDatabase()
                .From<Address>()
                .Update(address);
        }

        public async void DeleteAddress(int addressId)
        {
            await _connection.AccessDatabase()
                .From<Address>()
                .Where(x => x.Addressid == addressId)
                .Delete();
        }
    }
}