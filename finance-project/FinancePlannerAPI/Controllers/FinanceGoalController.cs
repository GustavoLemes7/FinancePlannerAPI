using FinancePlannerAPI.Extensions;
using FinancePlannerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FinancePlannerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinancialGoalController : ControllerBase
{

    private readonly FinancialGoalService _financialGoalService;
    public FinancialGoalController(FinancialGoalService financialGoalService)
    {
        _financialGoalService = financialGoalService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateFinancialGoalRequest request)
    {

        var userId = User.GetUserId();

        var created = await _financialGoalService.Create(request, userId);

        if (!created)
            return BadRequest("erro ao objetivo investimento.");



        return Created();
    }

    [Authorize]
    [HttpGet("{publicId:guid}")]
    public async Task<IActionResult> GetById(Guid publicId)
    {

        var financialGoal = await _financialGoalService.GetByPublicId(publicId);

        if (financialGoal is null)
            return NotFound();

        return Ok(financialGoal);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetByUserId()
    {
        var userId = User.GetUserId();

        var financialGoals = await _financialGoalService.GetByUserId(userId);

        return Ok(financialGoals);
    }

    [Authorize]
    [HttpPut("{publicId:guid}")]
    public async Task<IActionResult> Update(
        Guid publicId,
        UpdateFinancialGoalRequest request)
    {
        var userId = User.GetUserId();

        var updated = await _financialGoalService.Update(publicId, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{publicId:guid}")]
    public async Task<IActionResult> Delete(Guid publicId)
    {

        var deleted = await _financialGoalService.Delete(publicId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
    