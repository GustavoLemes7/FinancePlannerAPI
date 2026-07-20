using System.Data;
using FinancePlannerAPI.Data;
using FinancePlannerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancePlannerAPI.Services;

public class TransactionService
{
    private readonly ApplicationDbContext _context;

    public TransactionService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Create(CreateTransactionRequest request, int userId)
    {
        var account = await _context.Accounts
        .FirstOrDefaultAsync(a =>
            a.PublicId == request.AccountPublicId &&
            a.UserId == userId);

        if (account is null)
            return false;

        var category = await _context.Categories
            .FirstOrDefaultAsync(c =>
                c.PublicId == request.CategoryPublicId);

        if (category is null)
            return false;

        var transaction = new Transaction
        {
            UserId = userId,
            AccountId = account.Id,
            CategoryId = category.Id,
            Type = request.Type,
            Description = request.Description,
            Amount = request.Amount,
            TransactionDate = request.TransactionDate,
            PublicId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync();

        return true;
    }

      public async Task<List<Transaction>> GetByUserId(int userId)
    {
        var transactions = await _context.Transactions.Where(x => x.UserId == userId).ToListAsync();

        return transactions;
    }

    public async Task<Transaction?> GetByPublicId(Guid publicId)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(x =>
                x.PublicId == publicId);
    }

    public async Task<bool> Update(Guid publicId, UpdateTransactionRequest request)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(transaction is null)
            return false;

        
        transaction.Type = request.Type;
        transaction.Amount = request.Amount;
        transaction.Description = request.Description;
        

        var rows = await _context.SaveChangesAsync();

         if(rows == 0)
            return false;

        return true;
        

    }

    public async Task<bool> Delete(Guid publicId)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(transaction is null)
            return false;

        var rows = await _context.Transactions.Where(x => x.PublicId == publicId).ExecuteDeleteAsync();

        if(rows == 0)
            return false;

        return true;
    }
   
}

  