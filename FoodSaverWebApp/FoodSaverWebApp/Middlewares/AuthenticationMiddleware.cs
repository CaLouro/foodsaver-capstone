using FoodSaverWebApp.Services;

namespace FoodSaverWebApp.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HashSet<string> _publicPaths;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;

            _publicPaths = new HashSet<string>()
            {
                "/",
                "/Signup",
                "/Login",
                "/Privacy",
            };
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            var user = await authService.GetActiveUser();
            var requestPath = context.Request.Path;

            var isNotAuthenticated = user == null;
            var isNotPublicPath = !_publicPaths.Contains(requestPath);

            if (isNotPublicPath && isNotAuthenticated)
            {
                context.Response.Redirect("/Login");
            }

            await _next(context);
        }
    }
}