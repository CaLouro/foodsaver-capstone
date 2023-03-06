namespace FoodSaverWebApp.Entities
{
    public class Business
    {
        public string BusinessId { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public List<User> Admins { get; set; }
        public List<User> Favorited { get; set; }
        public Address Address { get; set; }
    }
}
