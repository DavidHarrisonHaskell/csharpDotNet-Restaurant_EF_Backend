using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Talent;

public class CatchAllFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        //string message = context.Exception.Message; // old way 
        // don't tell the user internal things that happened in production mode... only in development mode
        string message = GetInnerMessage(context.Exception);
        InternalServerError error = new InternalServerError(message);
        JsonResult result = new JsonResult(error);
        result.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = result;
        context.ExceptionHandled = true; // state that we handled the exception
        // add this to the level of the whole application
        // go to Program.cs and add it
    }

    // recursion: a function that calls itself
    private string GetInnerMessage(Exception ex)
    {
        if (ex == null) return "";
        if (ex.InnerException == null) return ex.Message;
        return GetInnerMessage(ex.InnerException);
    }
}
