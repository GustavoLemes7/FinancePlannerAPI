using System.Data;
using FinancePlannerAPI.Data;
using FinancePlannerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancePlannerAPI.Services;

public class AccountService
{
    private readonly ApplicationDbContext _context;

    public AccountService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Create(CreateAccountRequest request, int userId)
    {

        var account = new Account
        {
            UserId = userId,
            Name = request.Name,
            Type = request.Type,
            InitialBalance = request.InitialBalance,
            PublicId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Accounts.Add(account);

        await _context.SaveChangesAsync();

        return true;
    }

      public async Task<Account?> GetByUserID(int userId)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(x => x.UserId == userId);

        if (account == null)
            return null;


        return account;
    }

    public async Task<Account?> GetByPublicId(int userId, Guid publicId)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.PublicId == publicId);
    }

    public async Task<bool> Update(int userId, Guid publicId, UpdateAccountRequest request)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(x => x.UserId == userId && x.PublicId == publicId);

        if(account is null)
            return false;

        account.Name = request.Name;
        account.Type = request.Type;
        account.InitialBalance = request.InitialBalance;

        var rows = await _context.SaveChangesAsync();

         if(rows == 0)
            return false;

        return true;
        

    }

    public async Task<bool> Delete(int userId, Guid publicId)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(x => x.UserId == userId && x.PublicId == publicId);

        if(account is null)
            return false;

        var rows = await _context.Accounts.Where(x => x.PublicId == publicId).ExecuteDeleteAsync();

        if(rows == 0)
            return false;

        return true;
    }
   
}

  