using System.Data;
using FinancePlannerAPI.Data;
using FinancePlannerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancePlannerAPI.Services;

public class FinancialGoalService
{
    private readonly ApplicationDbContext _context;

    public FinancialGoalService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Create(CreateFinancialGoalRequest request, int userId)
    {
       
        var financialGoal = new FinancialGoal
        {
            UserId = userId,
            Name = request.Name,
            TargetAmount = request.TargetAmount,
            CurrentAmount = request.CurrentAmount,
            TargetDate = request.TargetDate,
            Status = request.Status,
            PublicId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _context.FinancialGoals.Add(financialGoal);

        await _context.SaveChangesAsync();

        return true;
    }

      public async Task<List<FinancialGoal>> GetByUserId(int userId)
    {
        var financialGoals = await _context.FinancialGoals.Where(x => x.UserId == userId).ToListAsync();

        return financialGoals;
    }

    public async Task<FinancialGoal?> GetByPublicId(Guid publicId)
    {
        return await _context.FinancialGoals
            .FirstOrDefaultAsync(x =>
                x.PublicId == publicId);
    }

    public async Task<bool> Update(Guid publicId, UpdateFinancialGoalRequest request)
    {
        var financialGoal = await _context.FinancialGoals
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(financialGoal is null)
            return false;

        financialGoal.Name = request.Name;
        financialGoal.TargetAmount = request.TargetAmount;
        financialGoal.CurrentAmount = request.CurrentAmount;
        financialGoal.TargetDate = request.TargetDate;
        financialGoal.Status = request.Status;
        

        var rows = await _context.SaveChangesAsync();

         if(rows == 0)
            return false;

        return true;
        

    }

    public async Task<bool> Delete(Guid publicId)
    {
        var financialGoal = await _context.FinancialGoals
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(financialGoal is null)
            return false;

        var rows = await _context.FinancialGoals.Where(x => x.PublicId == publicId).ExecuteDeleteAsync();

        if(rows == 0)
            return false;

        return true;
    }
   
}

  