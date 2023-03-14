using FoodSaverWebApp.Services;

namespace FoodSaverWebApp.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PathString _adminPath;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
            _adminPath = new PathString("/Admin");
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            var user = await authService.GetActiveUser();
            if (user == null)
            {
                // This middleware does not makes checks in case the user is not authenticated
                // because it's not supposed to. The AuthenticationMiddleware should always come
                // first in the middleware stack and it should redirect before reaching here due
                // to `/Admin` not being a public path.

                await _next(context);
                return;
            }

            var isProtectedPath = context.Request.Path.StartsWithSegments(_adminPath);
            var isNotAdminUser = !user?.IsAdmin() ?? true;

            if (isProtectedPath && isNotAdminUser)
            {
                context.Response.Redirect("/Dashboard");
            }

            await _next(context);
        }
    }
}