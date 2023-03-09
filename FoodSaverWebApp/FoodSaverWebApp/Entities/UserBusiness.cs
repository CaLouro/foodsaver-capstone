using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table(tableName: "user_business")]
    public class UserBusiness : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("business_id")]
        public int BusinessId { get; set; }

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        [Column("is_favorite")]
        public bool IsFavorite { get; set; }
    }
}
