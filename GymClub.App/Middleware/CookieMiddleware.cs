namespace GymClub.App.Middleware
{
    public class CookieMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (Path.Any(x => x.ToLower() == context.Request.Path.ToString().ToLower()))
                goto result;

            if (context.User.Identity is { IsAuthenticated: false })
            {
                context.Response.Redirect("/");
            }

        result:
            await next.Invoke(context);
        }

        private string[] Path =
        {
            "/",
            "/_blazor",
            "/_blazor/initializers",
            "/_blazor/negotiate",
            "/notificationHub/negotiate",
            "/notificationHub",
            "/register",
            "/signIn",
            "/_framework/blazor.web.js",
        };
    }
}
