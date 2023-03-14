using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public interface IAddressService
    {
        /// <summary>
        /// Retrieves a list of type ICollection for Address entities
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<Address>> GetAllAddresses();
        
        /// <summary>
        /// Retrieves a single Address entity given the addressId
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public Task<Address?> GetAddress(int addressId);
        
        /// <summary>
        /// Insert the given Address entity into the database
        /// </summary>
        /// <param name="address"></param>
        public Task InsertAddress(Address address);

        /// <summary>
        /// Returns the address entity that was just inserted. Useful for getting the incremented id
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<Address?> ReturnAddressOnInsert(Address address);
        
        /// <summary>
        /// Update an existing Address entity inside of the database
        /// </summary>
        /// <param name="address"></param>
        public Task UpdateAddress(Address address);
        
        /// <summary>
        /// Delete a Address entity from the database given the addressId
        /// </summary>
        /// <param name="addressId"></param>
        public Task DeleteAddress(int addressId);
    }
}