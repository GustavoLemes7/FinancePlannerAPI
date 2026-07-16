using FinancePlannerAPI.Extensions;
using FinancePlannerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FinancePlannerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{

    private readonly AccountService _accountService;
    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAccountRequest request)
    {
        var userId = User.GetUserId();

        var created = await _accountService.Create(request, userId);

        if (!created)
            return BadRequest("erro ao registrar conta.");



        return Created();
    }

    [Authorize]
    [HttpGet("{publicId:guid}")]
    public async Task<IActionResult> GetById(Guid publicId)
    {
        var userId = User.GetUserId();

        var account = await _accountService.GetByPublicId(userId, publicId);

        if (account is null)
            return NotFound();

        return Ok(account);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetByUserId()
    {
        var userId = User.GetUserId();

        var account = await _accountService.GetByUserID(userId);

        if (account is null)
            return NotFound();

        return Ok(account);
    }

    [Authorize]
    [HttpPut("{publicId:guid}")]
    public async Task<IActionResult> Update(
        Guid publicId,
        UpdateAccountRequest request)
    {
        var userId = User.GetUserId();

        var updated = await _accountService.Update(userId, publicId, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{publicId:guid}")]
    public async Task<IActionResult> Delete(Guid publicId)
    {
        var userId = User.GetUserId();

        var deleted = await _accountService.Delete(userId, publicId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}


