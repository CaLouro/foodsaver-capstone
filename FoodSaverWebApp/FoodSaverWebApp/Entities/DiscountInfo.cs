using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table("discount_info")]
    public class DiscountInfo : BaseModel
    {
        [PrimaryKey("id")]
        public int DiscountInfoId { get; set; }

        [Column("quantity_available")]
        public int? QuantityAvailable { get; set; }

        [Column("price")]
        public float Price { get; set; }

        [Column("availability_starts")]
        public DateTime? AvailabilityStarts { get; set; }

        [Column("availability_ends")]
        public DateTime? AvailabilityEnds { get; set; }

        [Column("item_id")]
        public int ItemId { get; set; }

        public bool IsActive()
        {
            return DateTime.Now >= AvailabilityStarts && DateTime.Now < AvailabilityEnds;
        }
    }
}
