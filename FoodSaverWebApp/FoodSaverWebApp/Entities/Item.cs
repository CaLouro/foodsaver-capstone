namespace FoodSaverWebApp.Entities
{
    public class Item
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double StandardPrice { get; set; }

        public Business BusinessId { get; set; }
        public DiscountInfo? DiscountInfo { get; set; }
    }
}
