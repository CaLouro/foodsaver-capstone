using Postgrest.Attributes;
using Postgrest.Models;

namespace FoodSaverWebApp.Entities
{
    [Table(tableName: "user")]
    public class User : BaseModel
    {
        [PrimaryKey(columnName: "id")]
        public int UserId { get; set; }

        [Column(columnName: "display_name")]
        public string? DisplayName { get; set; }

        [Column(columnName: "account_uid")]
        public string AccountId { get; set; }
    }
}
