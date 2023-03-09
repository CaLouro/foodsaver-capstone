using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;

namespace FoodSaverWebApp.Services
{
    public class AuthService : DbConnection, IAuthService
    {
        public AuthService() { InitializeDatabaseConnection(); }

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
