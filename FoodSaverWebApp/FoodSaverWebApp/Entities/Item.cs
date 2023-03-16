using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table("item")]
    public class Item : BaseModel
    {
        [PrimaryKey("id")]
        public int ItemId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("standard_price")]
        public float? StandardPrice { get; set; }

        [Column("business_id")]
        public int BusinessId { get; set; }
        
        [Reference(typeof(Business), shouldFilterTopLevel: false)]
        public Business Business { get; set; }

        
        [Reference(typeof(DiscountInfo), shouldFilterTopLevel: false)]
        public List<DiscountInfo> DiscountInfo { get; set; } = new List<DiscountInfo>();

        public DiscountInfo? ActiveDiscount()
        {
            foreach (DiscountInfo discountInfo in DiscountInfo)
            {
                if (discountInfo.isActive())
                    return discountInfo;
            }

            return null;
        }
    }
}
