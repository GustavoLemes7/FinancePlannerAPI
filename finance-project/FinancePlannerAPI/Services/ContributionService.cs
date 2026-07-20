using System.Data;
using FinancePlannerAPI.Data;
using FinancePlannerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancePlannerAPI.Services;

public class ContributionService
{
    private readonly ApplicationDbContext _context;

    public ContributionService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Create(CreateContributionRequest request, int userId)
    {
       
        var contribution = new Contribution
        {
            UserId = userId,
            InvestmentId = request.InvestmentId,
            Amount = request.Amount,
            ContributionDate = request.ContributionDate,
            PublicId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Contributions.Add(contribution);

        await _context.SaveChangesAsync();

        return true;
    }

      public async Task<List<Contribution>> GetByUserId(int userId)
    {
        var contributions = await _context.Contributions.Where(x => x.UserId == userId).ToListAsync();

        return contributions;
    }

    public async Task<Contribution?> GetByPublicId(Guid publicId)
    {
        return await _context.Contributions
            .FirstOrDefaultAsync(x =>
                x.PublicId == publicId);
    }

    public async Task<bool> Update(Guid publicId, UpdateContributionRequest request)
    {
        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(contribution is null)
            return false;

        contribution.Amount = request.Amount;
        contribution.ContributionDate = request.ContributionDate;
        

        var rows = await _context.SaveChangesAsync();

         if(rows == 0)
            return false;

        return true;
        

    }

    public async Task<bool> Delete(Guid publicId)
    {
        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(contribution is null)
            return false;

        var rows = await _context.Contributions.Where(x => x.PublicId == publicId).ExecuteDeleteAsync();

        if(rows == 0)
            return false;

        return true;
    }
   
}

  