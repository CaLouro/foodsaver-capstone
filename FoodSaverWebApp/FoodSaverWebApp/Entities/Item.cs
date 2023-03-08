using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table(tableName: "item")]
    public class Item : BaseModel
    {
        [PrimaryKey(columnName: "id")]
        public int ItemId { get; set; }

        [Column(columnName: "name")]
        public string Name { get; set; }

        [Column(columnName: "description")]
        public string? Description { get; set; }

        [Column(columnName: "standard_price")]
        public float? StandardPrice { get; set; }

        [Reference(typeof(Business))]
        public Business Business { get; set; }
    }
}
