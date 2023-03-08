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
            string url = "https://bjgctikxmsxwksxpcbpr.supabase.co";
            string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJqZ2N0aWt4bXN4d2tzeHBjYnByIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY3ODIzODc4MiwiZXhwIjoxOTkzODE0NzgyfQ.qIUUqkzzcXnzzRhQKv72qmrUXsU3zFolmpVHtS2PeHY";

            _database = new Supabase.Client(url, key);
            await _database.InitializeAsync();
        }

        /// <summary>
        /// Authentication methods to handle users ability to login/sign up/sign out of their accounts
        /// </summary>
        /// <param name="loginModel"></param>
		public async Task<string?> SignIn(LoginModel loginModel)
        {
            var session = await _database.Auth.SignIn(loginModel.Email, loginModel.Password);
			return session.AccessToken;
		}
		public async void SignOut()
        {
            await _database.Auth.SignOut();
        }
		public async Task<string?> CreateAccount(RegisterModel registerModel)
        {
            var session = await _database.Auth.SignUp(registerModel.Email, registerModel.Password);

            AddUserInformation(registerModel, session.User.Id);

			return session?.AccessToken;
		}

		public async void AddUserInformation(RegisterModel registerModel, string uid)
        {
            var user = new User
            {
                DisplayName = registerModel.Name,
                AccountId = uid
            };

			await _database.From<User>().Insert(user);
		}
        public async Task<User> GetActiveUser()
        {
            string accountId = _database.Auth.CurrentSession.User.Id;

            var result = await _database
                .From<User>()
                .Where(x => x.DisplayName == "Brandon Hardy")
                .Single();

            return result;
        }
	}
}
