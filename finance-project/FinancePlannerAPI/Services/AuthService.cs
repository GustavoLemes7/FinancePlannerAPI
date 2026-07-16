using FinancePlannerAPI.Data;
using BCrypt.Net;
using FinancePlannerAPI.DTOs.Auth;
using FinancePlannerAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace FinancePlannerAPI.Services;

public class AuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Register(RegisterRequest request)
    {
        var exists = await _context.Users
            .AnyAsync(x => x.Email == request.Email);

        if (exists)
            return false;

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            PublicId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> Login(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null)
            return null;

        bool passwordIsValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!passwordIsValid)
            return null;

        return user;
    }
}