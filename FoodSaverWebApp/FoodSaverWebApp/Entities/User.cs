using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table("user")]
    public class User : BaseModel
    {
        [PrimaryKey("id")]
        public int UserId { get; set; }

        [Column("display_name")]
        public string? DisplayName { get; set; }

        [Column("account_uid")]
        public string AccountId { get; set; }

        [Reference(typeof(UserBusiness), shouldFilterTopLevel: false)]
        public List<UserBusiness> Businesses { get; set; } = new List<UserBusiness>();
    }
}
