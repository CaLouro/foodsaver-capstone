using NuGet.Protocol.Plugins;

namespace FoodSaverWebApp.Entities
{
    public class Address
    {
        public string Addressid { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string ProvinceCode { get; set; }
        public string PostalCode { get; set; }
    }
}
