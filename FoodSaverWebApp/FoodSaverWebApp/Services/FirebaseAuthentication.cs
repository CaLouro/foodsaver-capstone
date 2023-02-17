using FoodSaverWebApp.Models;
using Firebase.Auth;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace FoodSaverWebApp.Services
{
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        private FirebaseAuthProvider _auth; 

        public FirebaseAuthentication()
        {
            _auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCcpWEdBpAjRuk6DEJO4U9_3fLmkQpY9g4"));
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var fbAuthLink = await _auth.SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
            string token = fbAuthLink.FirebaseToken;

            return token;
        }

        public async Task<string> Registration(RegisterModel authModel)
        {
            await _auth.CreateUserWithEmailAndPasswordAsync(authModel.Email, authModel.Password);

            string token = await Login(new LoginModel(authModel.Email, authModel.Password));

            return token;
        }

        public FirebaseError LocalizeAuthExceptionMessage(FirebaseError? exception)
        {
            string genericErrorMessage = "Something went wrong. Please try again.";

            FirebaseError firebaseError = exception;

            if (exception == null)
            {
                firebaseError.error.modelError = String.Empty;
                firebaseError.error.message = genericErrorMessage;
            }

            switch (exception.error.message)
            {
                case "EMAIL_EXISTS":
                    firebaseError.error.modelError = "Email";
                    firebaseError.error.message = "Email already in use.";
                    break;

                case "INVALID_PASSWORD":
                    firebaseError.error.modelError = "Password";
                    firebaseError.error.message = "Wrong password. Please try again.";
                    break;

                case "EMAIL_NOT_FOUND":
                    firebaseError.error.modelError = "Email";
                    firebaseError.error.message = "There is no account with this credentials.";
                    break;

                default:
                    firebaseError.error.modelError = String.Empty;
                    firebaseError.error.message = exception.error.message;
                    break;
                    /*
                case "invalid-email":
                    return "Invalid email format. Please try again.";

                case "user-disabled":
                    return "User is disabled.";

                case "wrong-password":
                    return "Wrong password. Please try again.";

                case "email-already-in-use":
                    return "Email already in use.";

                case "operation-not-allowed":
                    return "Operation not allowed.";

                case "weak-password":
                    return "Weak password. The password must follow the specification.";
                    */
            }

            return firebaseError;
        }

        public FirebaseError ExtractFirebaseException(FirebaseAuthException exception)
        {
            var firebaseError = JsonConvert.DeserializeObject<FirebaseError>(exception.ResponseData);
            return LocalizeAuthExceptionMessage(firebaseError);
        }
    }
}
