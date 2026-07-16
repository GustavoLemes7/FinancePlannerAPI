using FinancePlannerAPI.Extensions;
using FinancePlannerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FinancePlannerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{

    private readonly CategoryService _categoryService;
    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {

        var created = await _categoryService.Create(request);

        if (!created)
            return BadRequest("erro ao registrar categoria.");



        return Created();
    }

    [Authorize]
    [HttpGet("{publicId:guid}")]
    public async Task<IActionResult> GetById(Guid publicId)
    {
        var userId = User.GetUserId();

        var account = await _categoryService.GetByPublicId(publicId);

        if (account is null)
            return NotFound();

        return Ok(account);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        
        var categories = await _categoryService.GetAll();

        return Ok(categories);
    }

    [Authorize]
    [HttpPut("{publicId:guid}")]
    public async Task<IActionResult> Update(
        Guid publicId,
        UpdateCategoryRequest request)
    {
        var userId = User.GetUserId();

        var updated = await _categoryService.Update(publicId, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{publicId:guid}")]
    public async Task<IActionResult> Delete(Guid publicId)
    {

        var deleted = await _categoryService.Delete(publicId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
    