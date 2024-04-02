using TreasureTracker.Service.Helpers.Exceptions;
using TreasureTracker.UI.Helpers;

namespace TreasureTracker.UI.Middlewares;
public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly List<string> _excludedPaths = new List<string>
        {
            "/access/existemail",
            "/access/login",
            "/access/register"
        };
    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            //if (!_excludedPaths.Contains((context.Request.Path).ToString().ToLower()))
            //{
            //    // Check if the access token is present in the request
            //    if (!context.Request.Cookies.ContainsKey("token"))
            //    {
            //        // Redirect to the login page if the user is not authenticated
            //        context.Response.Redirect("/Access/ExistEmail");
            //        return;
            //    }
            //}
            await _next(context);
        }
        catch (TTrackerException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = 500,
                Message = ex.Message
            });
        }
    }
}
