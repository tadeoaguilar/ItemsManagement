using Microsoft.AspNetCore.Mvc;

namespace eItems.Identity.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var (success, token) = await _authService.AuthenticateAsync(request.Email, request.Password);
        if (!success)
        {
            return Unauthorized();
        }

        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(
            request.Email, 
            request.Password,
            request.FirstName,
            request.LastName);

        if (!result)
        {
            return BadRequest("Registration failed");
        }

        return Ok("Registration successful");
    }
}

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
