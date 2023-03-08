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
            string url = "https://bjgctikxmsxwksxpcbpr.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJqZ2N0aWt4bXN4d2tzeHBjYnByIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY3ODIzODc4MiwiZXhwIjoxOTkzODE0NzgyfQ.qIUUqkzzcXnzzRhQKv72qmrUXsU3zFolmpVHtS2PeHY";

            _database = new Supabase.Client(url, key);
            await _database.InitializeAsync();
        }
    }
}
