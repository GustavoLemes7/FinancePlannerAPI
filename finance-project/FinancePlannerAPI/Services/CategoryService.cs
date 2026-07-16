using System.Data;
using FinancePlannerAPI.Data;
using FinancePlannerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancePlannerAPI.Services;

public class CategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Create(CreateCategoryRequest request)
    {

        var category = new Category
        {
     
            Name = request.Name,
            Type = request.Type,
            PublicId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Categories.Add(category);

        await _context.SaveChangesAsync();

        return true;
    }

      public async Task<List<Category>> GetAll()
    {
        return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Category?> GetByPublicId(Guid publicId)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(x =>
                x.PublicId == publicId);
    }

    public async Task<bool> Update(Guid publicId, UpdateCategoryRequest request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(category is null)
            return false;

        category.Name = request.Name;
        category.Type = request.Type;
        

        var rows = await _context.SaveChangesAsync();

         if(rows == 0)
            return false;

        return true;
        

    }

    public async Task<bool> Delete(Guid publicId)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.PublicId == publicId);

        if(category is null)
            return false;

        var rows = await _context.Categories.Where(x => x.PublicId == publicId).ExecuteDeleteAsync();

        if(rows == 0)
            return false;

        return true;
    }
   
}

  