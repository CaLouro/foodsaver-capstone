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

        public async void InsertDiscountInfo(DiscountInfo discountInfo)
        {
            await _connection.AccessDatabase()
                .From<DiscountInfo>()
                .Insert(discountInfo);
        }

        public async void UpdateDiscountInfo(DiscountInfo discountInfo)
        {
            await _connection.AccessDatabase()
                .From<DiscountInfo>()
                .Update(discountInfo);
        }

        public async void DeleteDiscountInfo(int discountInfoId)
        {
            await _connection.AccessDatabase()
                .From<DiscountInfo>()
                .Where(x => x.DiscountInfoId == discountInfoId)
                .Delete();
        }
    }
}