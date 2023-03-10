using FoodSaverWebApp.Services;

namespace FoodSaverWebApp.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;
        private readonly PathString _adminPath;

        public AuthorizationMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;
            _adminPath = new PathString("/Admin");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = await _authService.GetActiveUser();
            if (user == null)
            {
                // This middleware does not makes checks in case the user is not authenticated
                // because it's not supposed to. The AuthenticationMiddleware should always come
                // first in the middleware stack and it should redirect before reaching here due
                // to `/Admin` not being a public path.

                await _next(context);
                return;
            }

            var requestPath = context.Request.Path;
            if (!requestPath.StartsWithSegments(_adminPath))
            {
                await _next(context);
                return;
            }

            if (!user!.IsAdmin())
            {
                context.Response.Redirect("/Dashboard");
            }

            await _next(context);
        }
    }
}
