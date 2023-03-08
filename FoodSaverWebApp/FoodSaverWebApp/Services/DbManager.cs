namespace FoodSaverWebApp.Services
{
    public class DbManager
    {
        private Supabase.Client _database;

        public DbManager()
        {
            InitializeDatabaseConnection();

		}

        public async void InitializeDatabaseConnection()
        {
            string url = "";
            string key = "";

            _database = new Supabase.Client(url, key);
            await _database.InitializeAsync();
        }
    }
}
