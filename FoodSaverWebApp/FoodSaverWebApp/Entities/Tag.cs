using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table(tableName: "tag")]
    public class Tag : BaseModel
    {
        [PrimaryKey(columnName: "id")]
        public int TagId { get; set; }

        [Column(columnName: "name")]
        public string Name { get; set; }

        [Column(columnName: "description")]
        public string? Description { get; set; }
    }
}
