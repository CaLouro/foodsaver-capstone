using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Models
{
    public class StoreDashboardModel
    {
        public List<Business> Businesses { get; set; } = new List<Business>();

        public List<UserBusiness> FavoriteBusinesses { get; set; } = new List<UserBusiness>();

        public bool IsFavoriteBusiness(Business business)
        {
            return FavoriteBusinesses.Exists(b => b.BusinessId == business.BusinessId);
        }
    }
}