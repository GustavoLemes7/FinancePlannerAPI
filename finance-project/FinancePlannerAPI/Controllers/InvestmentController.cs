using FinancePlannerAPI.Extensions;
using FinancePlannerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FinancePlannerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestmentController : ControllerBase
{

    private readonly InvestmentService _investmentService;
    public InvestmentController(InvestmentService investmentService)
    {
        _investmentService = investmentService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateInvestmentRequest request)
    {

        var userId = User.GetUserId();

        var created = await _investmentService.Create(request, userId);

        if (!created)
            return BadRequest("erro ao registrar investimento.");



        return Created();
    }

    [Authorize]
    [HttpGet("{publicId:guid}")]
    public async Task<IActionResult> GetById(Guid publicId)
    {

        var investment = await _investmentService.GetByPublicId(publicId);

        if (investment is null)
            return NotFound();

        return Ok(investment);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetByUserId()
    {
        var userId = User.GetUserId();

        var investments = await _investmentService.GetByUserId(userId);

        return Ok(investments);
    }

    [Authorize]
    [HttpPut("{publicId:guid}")]
    public async Task<IActionResult> Update(
        Guid publicId,
        UpdateInvestmentRequest request)
    {

        var updated = await _investmentService.Update(publicId, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{publicId:guid}")]
    public async Task<IActionResult> Delete(Guid publicId)
    {

        var deleted = await _investmentService.Delete(publicId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
    