using FoodSaverWebApp.Models;
using Firebase.Auth;

namespace FoodSaverWebApp.Services
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        FirebaseAuthProvider auth; 

        public FirebaseAuthentication()
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCcpWEdBpAjRuk6DEJO4U9_3fLmkQpY9g4"));
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
            string token = fbAuthLink.FirebaseToken;

            return token;
        }

        public async Task<string> Registration(RegisterModel authModel)
        {
            await auth.CreateUserWithEmailAndPasswordAsync(authModel.Email, authModel.Password);

            var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(authModel.Email, authModel.Password);
            string token = fbAuthLink.FirebaseToken;

            return token;
        }

        public string AdjustErrorMessage(string message)
        {
            string newMessage;

            switch (message)
            {
                case "EMAIL_EXISTS":
                    newMessage = "Email has already been used.";
                    break;
                case "INVALID_PASSWORD":
                    newMessage = "Invalid password";
                    break;
                default:
                    newMessage = message;
                    break;

            }

            return newMessage;
        }
    }
}
