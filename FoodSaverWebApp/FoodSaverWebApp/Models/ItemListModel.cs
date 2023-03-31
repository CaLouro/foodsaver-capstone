using FoodSaverWebApp.Entities;

namespace FoodSaverWebApp.Models
{
    public class ItemListModel
    {
        public ICollection<Item> Items { get; set; } = new List<Item>();

        public ICollection<DiscountInfo> Deals { get; set; } = new List<DiscountInfo>();
        
        public Business? Business { get; set; }
    }
}