using FoodSaverWebApp.Services;

namespace FoodSaverWebApp.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;
        private readonly HashSet<string> _publicPaths;

        public AuthenticationMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;

            _publicPaths = new HashSet<string>()
            {
                "/",
                "/Signup",
                "/Login",
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = await _authService.GetActiveUser();
            var requestPath = context.Request.Path;

            bool isNotAuthenticated = user == null;
            bool isNotPublicPath = !_publicPaths.Contains(requestPath);

            if (isNotPublicPath && isNotAuthenticated)
            {
                context.Response.Redirect("/Login");
            }

            await _next(context);
        }
    }
}
