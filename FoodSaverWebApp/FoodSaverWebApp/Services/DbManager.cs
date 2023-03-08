using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;

namespace FoodSaverWebApp.Services
{
    public class DbManager : IDbManager
    {
        private Supabase.Client _database;

        public DbManager()
        {
            InitializeDatabaseConnection();
        }

        public async void InitializeDatabaseConnection()
        {
            const string url = "https://bjgctikxmsxwksxpcbpr.supabase.co";
            const string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJqZ2N0aWt4bXN4d2tzeHBjYnByIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY3ODIzODc4MiwiZXhwIjoxOTkzODE0NzgyfQ.qIUUqkzzcXnzzRhQKv72qmrUXsU3zFolmpVHtS2PeHY";

            _database = new Supabase.Client(url, key);
            await _database.InitializeAsync();
        }

        public async Task<string?> SignIn(LoginModel loginModel)
        {
            Supabase.Gotrue.Session? session = await _database.Auth.SignIn(loginModel.Email, loginModel.Password);
            return session?.AccessToken;
        }

        public async void SignOut()
        {
            await _database.Auth.SignOut();
        }

        public async Task<string?> CreateAccount(RegisterModel registerModel)
        {
            Supabase.Gotrue.Session? session = await _database.Auth.SignUp(registerModel.Email, registerModel.Password);

            AddUserInformation(registerModel, session.User.Id);

            return session?.AccessToken;
        }

        private async void AddUserInformation(RegisterModel registerModel, string uid)
        {
            User user = new User
            {
                DisplayName = registerModel.Name,
                AccountId = uid
            };

            await _database.From<User>().Insert(user);
        }

        public async Task<User?> GetActiveUser()
        {
            Supabase.Gotrue.User? activeUser = _database.Auth.CurrentSession?.User;

            if (activeUser == null)
            {
                return null;
            }

            User? result = await _database
                .From<User>()
                .Where(x => x.AccountId == activeUser.Id)
                .Single();

            return result;
        }
    }
}
