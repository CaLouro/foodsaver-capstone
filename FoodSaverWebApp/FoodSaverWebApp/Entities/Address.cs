using Postgrest.Attributes;
using Postgrest.Models;
using System.Text;

namespace FoodSaverWebApp.Entities
{
    [Table("address")]
    public class Address : BaseModel
    {
        [PrimaryKey("id")]
        public int AddressId { get; set; }

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

        public string PresentAddress(bool showCity = true, bool showProvince = false, bool showPostalCode = false)
        {
            var buffer = new StringBuilder(Line1);

            if (Line2 != null)
            {
                buffer.Append(' ');
                buffer.Append(Line2);
            }

            if (showCity)
            {
                buffer.Append(", ");
                buffer.Append(City);
            }

            if (showProvince)
            {
                buffer.Append(", ");
                buffer.Append(ProvinceCode);
            }

            if (showPostalCode)
            {
                buffer.Append(", ");
                buffer.Append(PostalCode);
            }

            return buffer.ToString();
        }
    }
}
