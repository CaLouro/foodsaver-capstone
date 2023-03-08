using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table(tableName: "address")]
    public class Address : BaseModel
    {
        [PrimaryKey(columnName: "id")]
        public int Addressid { get; set; }

        [Column(columnName: "line1")]
        public string Line1 { get; set; }

        [Column(columnName: "line2")]
        public string Line2 { get; set; }

        [Column(columnName: "city")]
        public string City { get; set; }

        [Column(columnName: "province_code")]
        public string ProvinceCode { get; set; }

        [Column(columnName: "postal_code")]
        public string PostalCode { get; set; }
    }
}
