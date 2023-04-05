using FoodSaverWebApp.Models;
using Supabase.Gotrue;
using User = FoodSaverWebApp.Entities.User;

namespace FoodSaverWebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbConnection _connection;

        public AuthService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<string?> SignIn(LoginModel loginModel)
        {
            Session? session = await _connection.AccessDatabase()
                .Auth
                .SignIn(loginModel.Email, loginModel.Password);

            return session?.AccessToken;
        }

        public async Task SignOut()
        {
            await _connection.AccessDatabase()
                .Auth
                .SignOut();
        }

        public async Task<string?> CreateAccount(RegisterModel registerModel)
        {
            var userMetadata = new Dictionary<string, object> { { "display_name", registerModel.Name } };

            Session? session = await _connection.AccessDatabase()
                .Auth
                .SignUp(registerModel.Email, registerModel.Password, new SignUpOptions { Data = userMetadata });

            await AddUserInformation(registerModel.Name, session.User.Id);

            return await SignIn(new LoginModel
            {
                Email = registerModel.Email,
                Password = registerModel.Password
            });
        }

        private async Task AddUserInformation(string displayName, string uid)
        {
            User user = new User
            {
                DisplayName = displayName,
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