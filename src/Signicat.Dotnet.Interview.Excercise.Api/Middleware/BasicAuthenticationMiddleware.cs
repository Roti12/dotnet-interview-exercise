using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Signicat.Dotnet.Interview.Excercise.Api.Middleware;

public static class BasicAuthenticationMiddleware
{
    public static void UseBasicAuthentication(this IApplicationBuilder builder)
    {
        builder.Use(async (ctx, next) =>
        {
            try
            {
                if (!ctx.Request.Path.StartsWithSegments("/admin"))
                {
                    await next();

                    return;
                }

                var hasAuthHeader =
                    AuthenticationHeaderValue.TryParse(ctx.Request.Headers.Authorization, out var authHeader);

                if (hasAuthHeader && IsAuthenticated(authHeader))
                {
                    await next();

                    return;
                }

                ctx.Response.StatusCode = 401;
                await ctx.Response.WriteAsJsonAsync(new
                {
                    detail = "Not authenticated"
                });
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = 500;
                await ctx.Response.WriteAsJsonAsync(new
                {
                    ErrorCode = "internal_server_error",
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    Title = ex.Message,
                    Description = ex.ToString()
                });
            }
        });
    }

    private static bool IsAuthenticated(AuthenticationHeaderValue? authHeader)
    {
        if (authHeader is not { Scheme: "Basic", Parameter: not null })
        {
            return false;
        }

        var apiTokenAsBytes = Convert.FromBase64String(authHeader.Parameter);
        var apiCredentials = Encoding.UTF8.GetString(apiTokenAsBytes).Split(":", 2);

        if (apiCredentials.Length != 2)
        {
            return false;
        }

        var apiUsername = apiCredentials[0];
        var apiPassword = apiCredentials[1];

        return apiUsername == Constants.AdminApiUser && apiPassword == Constants.AdminApiPassword;
    }
}