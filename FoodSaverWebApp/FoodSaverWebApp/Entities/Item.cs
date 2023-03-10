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

        [Reference(typeof(Business))]
        public Business Business { get; set; }
    }
}
