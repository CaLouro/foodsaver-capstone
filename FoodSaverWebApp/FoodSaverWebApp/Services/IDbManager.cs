using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;

namespace FoodSaverWebApp.Services
{
    public interface IDbManager
    {
        public Task<string> SignIn(LoginModel loginModel);
		public void SignOut();
		public Task<string> CreateAccount(RegisterModel registerModel);

		public Task<User> GetActiveUser();
	}
}
