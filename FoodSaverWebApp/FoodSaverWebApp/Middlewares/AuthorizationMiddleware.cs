using FoodSaverWebApp.Services;

namespace FoodSaverWebApp.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;

        public AuthorizationMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Implementation pending

            await _next(context);
        }
    }
}
