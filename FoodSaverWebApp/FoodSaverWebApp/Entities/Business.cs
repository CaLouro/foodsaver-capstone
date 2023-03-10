using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table("business")]
    public class Business : BaseModel
    {
        [PrimaryKey("id")]
        public int BusinessId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("contact_email")]
        public string? ContactEmail { get; set; }

        [Column("contact_phone")]
        public string? ContactPhone { get; set; }

        [Reference(typeof(Address))]
        public Address Address { get; set; }

        [Reference(typeof(Tag), shouldFilterTopLevel: false)]
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
