using FinancePlannerAPI.Extensions;
using FinancePlannerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FinancePlannerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{

    private readonly TransactionService _transactionService;
    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionRequest request)
    {

        var userId = User.GetUserId();

        var created = await _transactionService.Create(request, userId);

        if (!created)
            return BadRequest("erro ao registrar categoria.");



        return Created();
    }

    [Authorize]
    [HttpGet("{publicId:guid}")]
    public async Task<IActionResult> GetById(Guid publicId)
    {
        var userId = User.GetUserId();

        var account = await _transactionService.GetByPublicId(publicId);

        if (account is null)
            return NotFound();

        return Ok(account);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetByUserId()
    {
        var userId = User.GetUserId();

        var categories = await _transactionService.GetByUserId(userId);

        return Ok(categories);
    }

    [Authorize]
    [HttpPut("{publicId:guid}")]
    public async Task<IActionResult> Update(
        Guid publicId,
        UpdateTransactionRequest request)
    {
        var userId = User.GetUserId();

        var updated = await _transactionService.Update(publicId, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{publicId:guid}")]
    public async Task<IActionResult> Delete(Guid publicId)
    {

        var deleted = await _transactionService.Delete(publicId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
    