using System.Data;
using FinancePlannerAPI.Data;
using FinancePlannerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancePlannerAPI.Services;

public class InvestmentService
{
    private readonly ApplicationDbContext _context;

    public InvestmentService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Create(CreateInvestmentRequest request, int userId)
    {
       
        var investment = new Investment
        {
            UserId = userId,
            Name = request.Name,
            Type = request.Type,
            InitialRate = request.InitialRate,
            StartRate = request.StartRate,
            PublicId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Investments.Add(investment);

        await _context.SaveChangesAsync();

        return true;
    }

      public async Task<List<Investment>> GetByUserId(int userId)
    {
        var investments = await _context.Investments.Where(x => x.UserId == userId).ToListAsync();

        return investments;
    }

    public async Task<Investment?> GetByPublicId(Guid publicId)
    {
        return await _context.Investments
            .FirstOrDefaultAsync(x =>
                x.PublicId == publicId);
    }

    public async Task<bool> Update(Guid publicId, UpdateInvestmentRequest request)
    {
        var investment = await _context.Investments
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(investment is null)
            return false;

        investment.Name = request.Name;
        investment.Type = request.Type;
        investment.InitialRate = request.InitialRate;
        investment.StartRate = request.StartRate;
        

        var rows = await _context.SaveChangesAsync();

         if(rows == 0)
            return false;

        return true;
        

    }

    public async Task<bool> Delete(Guid publicId)
    {
        var investment = await _context.Investments
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(investment is null)
            return false;

        var rows = await _context.Investments.Where(x => x.PublicId == publicId).ExecuteDeleteAsync();

        if(rows == 0)
            return false;

        return true;
    }
   
}

  