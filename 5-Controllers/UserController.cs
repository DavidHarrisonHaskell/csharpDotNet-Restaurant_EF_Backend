using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talent;

[ApiController]
public class UserController : ControllerBase, IDisposable
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("api/users/{userId}")]
    public IActionResult GetUserInformation([FromRoute] int userId)
    {
        User? dbUser = _userService.GetUserInformation(userId);
        if (dbUser == null) return NotFound(new ResourceNotFound(userId));
        return Ok(dbUser);
    }

    [HttpPost("api/register")]
    public IActionResult Register([FromBody] User user)
    {
        ModelState.Remove("Role"); // prevent bad request for required role.
        if (!ModelState.IsValid) return BadRequest(new ValidationError(ModelState.GetAllErrors())); //  custom Validation Error extensions but will not work in this case since [StringLength(40)] is basically declared twice. So basically better to just do GetFirstError
        if (_userService.IsEmailTaken(user.Email)) return BadRequest(new ValidationError($"Email {user.Email} is already taken."));
        string token = _userService.Register(user);
        return Created("", token);
    }

    // user must be logged-in
    [HttpPost("api/login")]
    public IActionResult Login([FromBody] Credentials credentials)
    {
        string? token = _userService.Login(credentials);
        if (token == null) return Unauthorized(new UnauthorizedError("Incorrect Email or Password"));
        return Ok(token);
    }

    public void Dispose()
    {
        _userService.Dispose();
    }
}
