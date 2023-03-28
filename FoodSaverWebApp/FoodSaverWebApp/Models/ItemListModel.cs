using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Models
{
    public class ItemListModel
    {
        public ICollection<Item> Items { get; set; }

        public ICollection<DiscountInfo> Deals { get; set; }
        
        public Business? Business { get; set; }
    }
}