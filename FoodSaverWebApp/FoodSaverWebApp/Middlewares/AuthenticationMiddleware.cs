using FoodSaverWebApp.Services;
using System.Net;

namespace FoodSaverWebApp.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;
        private readonly List<string> _publicPaths;

        public AuthenticationMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;

            _publicPaths = new List<string>()
            {
                "/",
                "/Register",
                "/Login",
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = await _authService.GetActiveUser();
            var requestPath = context.Request.Path;

            if (!_publicPaths.Contains(requestPath) && user == null)
            {
                context.Response.Redirect("/Login");
            }

            await _next(context);
        }
    }
}
