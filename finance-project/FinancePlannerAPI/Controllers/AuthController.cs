using Microsoft.AspNetCore.Mvc;
using FinancePlannerAPI.Services;
using FinancePlannerAPI.DTOs.Auth;
namespace FinancePlannerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Auth Controller funcionando!");
    }

    private readonly AuthService _authService;
    private readonly JwtService _jwtService;

    public AuthController(AuthService authService,  JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.Register(request);

        if (!result)
            return BadRequest("Email already registered.");

        return Ok("User created successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _authService.Login(request);

        if (user is null)
            return BadRequest("Email ou senha inválidos.");


        var token = _jwtService.GenerateToken(user);


        return Ok(new
        {
            token,
            user = new
            {
                user.Name,
                user.Email,
                user.PublicId
            }
        });
    }
}