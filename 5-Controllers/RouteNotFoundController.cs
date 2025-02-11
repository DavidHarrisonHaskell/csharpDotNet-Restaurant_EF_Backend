using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

[ApiController]
public class RouteNotFoundController : ControllerBase
{
    [Route("{**path}")]
    public IActionResult RouteNotFound(string path)
    {
        string method = HttpContext.Request.Method;
        RouteNotFoundError error = new RouteNotFoundError(method, path);
        return NotFound(error);
    }
}
