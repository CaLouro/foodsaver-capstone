using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table("address")]
    public class Address : BaseModel
    {
        [PrimaryKey("id")]
        public int Addressid { get; set; }

        [Column("line1")]
        public string Line1 { get; set; }

        [Column("line2")]
        public string? Line2 { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("province_code")]
        public string ProvinceCode { get; set; }

        [Column("postal_code")]
        public string PostalCode { get; set; }
    }
}
