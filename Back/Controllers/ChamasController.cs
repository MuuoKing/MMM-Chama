using Microsoft.AspNetCore.Mvc;
using MMM.Chama.Api.Models;
using MMM.Chama.Api.Services;

namespace MMM.Chama.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ChamasController(IChamaStore store) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyCollection<Chama>> GetAll() => Ok(store.GetAll());

    [HttpGet("{id:guid}")]
    public ActionResult<Chama> GetById(Guid id)
    {
        var chama = store.Get(id);
        return chama is null ? NotFound(new { message = "Chama not found." }) : Ok(chama);
    }

    [HttpPost]
    public ActionResult<Chama> Create(CreateChamaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            ModelState.AddModelError(nameof(request.Name), "Name is required.");
        if (request.ContributionAmount <= 0)
            ModelState.AddModelError(nameof(request.ContributionAmount), "Contribution amount must be greater than zero.");

        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var chama = store.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = chama.Id }, chama);
    }

    [HttpPost("{id:guid}/members")]
    public ActionResult<Member> AddMember(Guid id, AddMemberRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
            ModelState.AddModelError(nameof(request.FullName), "Full name is required.");
        if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains('@'))
            ModelState.AddModelError(nameof(request.Email), "A valid email is required.");

        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var member = store.AddMember(id, request);
        return member is null
            ? NotFound(new { message = "Chama not found." })
            : Created($"/api/chamas/{id}/members/{member.Id}", member);
    }
}
