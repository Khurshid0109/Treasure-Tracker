using TreasureTracker.Service.Helpers.Exceptions;
using TreasureTracker.UI.Helpers;

namespace TreasureTracker.UI.Middlewares;
public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly List<string> _excludedPaths = new List<string>
        {
            "/",
            "/access/existemail",
            "/existemail",
            "/access/login",
            "/login",
            "/access/register",
            "/register"
        };
    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            var path = (context.Request.Path).ToString().ToLower();
            if (!_excludedPaths.Contains(path))
            {
                // Check if the access token is present in the request
                if (!context.Request.Cookies.ContainsKey("token"))
                {
                    // Redirect to the login page if the user is not authenticated
                    context.Response.Redirect("/Access/ExistEmail");
                    return;
                }
            }
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                context.Response.Redirect("/Error/GlobalError?statusCode=404");
        }
        catch (TTrackerException ex)
        {
            context.Response.Redirect($"/ErrorHandler/GlobalError?statusCode={ex.StatusCode}");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            context.Response.Redirect($"/ErrorHandler/GlobalError?statusCode=500");
        }
    }
}
