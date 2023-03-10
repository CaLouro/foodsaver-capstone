using FoodSaverWebApp.Entities;
using FoodSaverWebApp.Models;

namespace FoodSaverWebApp.Services
{
    public interface IAuthService
    {
		/// <summary>
		/// Signs into an account with the given LoginModel.
		/// </summary>
		/// <param name="loginModel"></param>
		/// <returns>The account's access token, if present</returns>
        public Task<string?> SignIn(LoginModel loginModel);

		/// <summary>
		/// Signs out the currently authenticated account.
		/// </summary>
		public void SignOut();

        /// <summary>
        /// Signs up an account with the given RegisterModel
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns>The account's access token, if present</returns>
        public Task<string?> CreateAccount(RegisterModel registerModel);

		/// <summary>
		/// Get the currently authenticated user information.
		/// </summary>
		/// <returns>The current user model or null if not authenticated</returns>
		public Task<User?> GetActiveUser();
	}
}
