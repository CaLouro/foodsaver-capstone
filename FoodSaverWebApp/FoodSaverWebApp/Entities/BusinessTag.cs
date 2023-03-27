using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table("business_tag")]
    public class BusinessTag : BaseModel
    {
        [Column("business_id")]
        public int BusinessId { get; set; }
        
        [Column("tag_id")]
        public int TagId { get; set; }
    }
}