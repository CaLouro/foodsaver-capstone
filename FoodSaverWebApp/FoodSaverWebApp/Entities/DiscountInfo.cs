namespace FoodSaverWebApp.Entities
{
    public class DiscountInfo
    {
        public string DiscountInfoId { get; set; }
        public int QuantityAvailable { get; set; }
        public double Price { get; set; }
        public DateTime AvailabilityStarts { get; set; }
        public DateTime AvailabilityEnds { get; set; }
    }
}
