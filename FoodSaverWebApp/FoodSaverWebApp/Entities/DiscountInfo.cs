using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table(tableName: "discount_info")]
    public class DiscountInfo : BaseModel
    {
        [PrimaryKey(columnName: "id")]
        public int DiscountInfoId { get; set; }

        [Column(columnName: "quantity_available")]
        public int? QuantityAvailable { get; set; }

        [Column(columnName: "price")]
        public float Price { get; set; }

        [Column(columnName: "availability_starts")]
        public DateTime? AvailabilityStarts { get; set; }

        [Column(columnName: "availability_ends")]
        public DateTime? AvailabilityEnds { get; set; }

        [Reference(typeof(Item))]
        public Item Item { get; set; }
    }
}
