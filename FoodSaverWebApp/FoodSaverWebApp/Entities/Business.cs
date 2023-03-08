using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table(tableName: "business")]
    public class Business : BaseModel
    {
        [PrimaryKey(columnName: "id")]
        public int BusinessId { get; set; }

        [Column(columnName: "name")]
        public string Name { get; set; }

        [Column(columnName: "contact_email")]
        public string ContactEmail { get; set; }

        [Column(columnName: "contact_phone")]
        public string ContactPhone { get; set; }

        [Reference(typeof(Address))]
        public Address Address { get; set; }

        [Reference(typeof(Tag))]
        public List<Tag> Tags { get; set; } 
    }
}
