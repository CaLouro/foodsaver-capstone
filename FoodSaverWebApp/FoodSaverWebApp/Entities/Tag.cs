using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table("tag")]
    public class Tag : BaseModel
    {
        [PrimaryKey("id")]
        public int TagId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }
    }
}
