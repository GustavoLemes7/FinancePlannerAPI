using FinancePlannerAPI.Extensions;
using FinancePlannerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FinancePlannerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContributionController : ControllerBase
{

    private readonly ContributionService _contributionService;
    public ContributionController(ContributionService contributionService)
    {
        _contributionService = contributionService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateContributionRequest request)
    {

        var userId = User.GetUserId();

        var created = await _contributionService.Create(request, userId);

        if (!created)
            return BadRequest("erro ao registrar investimento.");



        return Created();
    }

    [Authorize]
    [HttpGet("{publicId:guid}")]
    public async Task<IActionResult> GetById(Guid publicId)
    {

        var contribution = await _contributionService.GetByPublicId(publicId);

        if (contribution is null)
            return NotFound();

        return Ok(contribution);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetByUserId()
    {
        var userId = User.GetUserId();

        var contributions = await _contributionService.GetByUserId(userId);

        return Ok(contributions);
    }

    [Authorize]
    [HttpPut("{publicId:guid}")]
    public async Task<IActionResult> Update(
        Guid publicId,
        UpdateContributionRequest request)
    {

        var updated = await _contributionService.Update(publicId, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{publicId:guid}")]
    public async Task<IActionResult> Delete(Guid publicId)
    {

        var deleted = await _contributionService.Delete(publicId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
    