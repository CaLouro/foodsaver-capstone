using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;

namespace FoodSaverWebApp.Services
{
    public class AuthService : IAuthService
    {
        private IDbConnection _connection;
        public AuthService(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public async Task<string?> SignIn(LoginModel loginModel)
        {
            Supabase.Gotrue.Session? session = await _connection.AccessDatabase()
                .Auth
                .SignIn(loginModel.Email, loginModel.Password);
            
            return session?.AccessToken;
        }

        public async void SignOut()
        {
            await _connection.AccessDatabase()
                .Auth
                .SignOut();
        }

        public async Task<string?> CreateAccount(RegisterModel registerModel)
        {
            Supabase.Gotrue.Session? session = await _connection.AccessDatabase()
                .Auth
                .SignUp(registerModel.Email, registerModel.Password);

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

            await _connection.AccessDatabase()
                .From<User>()
                .Insert(user);
        }

        public async Task<User?> GetActiveUser()
        {
            Supabase.Gotrue.User? activeUser = _connection.AccessDatabase()
                .Auth
                .CurrentSession?
                .User;

            if (activeUser == null)
            {
                return null;
            }

            User? result = await _connection.AccessDatabase()
                .From<User>()
                .Where(x => x.AccountId == activeUser.Id)
                .Single();

            return result;
        }
    }
}
