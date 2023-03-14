using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Services
{
    public class DiscountInfoService : IDiscountInfoService
    {
        private readonly IDbConnection _connection;
        public DiscountInfoService(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public async Task<ICollection<DiscountInfo>> GetAllDiscountInfos()
        {
            var result = await _connection.AccessDatabase()
                .From<DiscountInfo>()
                .Get();

            return result.Models;
        }

        public async Task<DiscountInfo?> GetDiscountInfo(int discountInfoId)
        {
            DiscountInfo? result = await _connection.AccessDatabase()
                .From<DiscountInfo>()
                .Where(x => x.DiscountInfoId == discountInfoId)
                .Single();

            return result;
        }

        public async Task InsertDiscountInfo(DiscountInfo discountInfo)
        {
            await _connection.AccessDatabase()
                .From<DiscountInfo>()
                .Insert(discountInfo);
        }

        public async Task UpdateDiscountInfo(DiscountInfo discountInfo)
        {
            await _connection.AccessDatabase()
                .From<DiscountInfo>()
                .Update(discountInfo);
        }

        public async Task DeleteDiscountInfo(int discountInfoId)
        {
            await _connection.AccessDatabase()
                .From<DiscountInfo>()
                .Where(x => x.DiscountInfoId == discountInfoId)
                .Delete();
        }
    }
}