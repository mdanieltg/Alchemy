using System.Net.Mime;
using Alchemy.BusinessLogic.Models;
using Alchemy.BusinessLogic.Repositories;
using Alchemy.DataModel.Entities;
using Alchemy.WebAPI.Models;
using Alchemy.WebAPI.Validation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/effects")]
[Produces(MediaTypeNames.Application.Json)]
public class EffectsController(IPagedRepository<Effect> effectsRepository, IMapper mapper) : ControllerBase
{
    private readonly IPagedRepository<Effect> _effects = effectsRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet(Name = "ListEffects")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<EffectLimited> GetAllEffects([FromQuery] int limit = 10, [FromQuery] int offset = 1)
    {
        QueryParameterValidation.EnsureLimitIsBetweenLimits(ref limit);
        QueryParameterValidation.EnsureOffsetIsBetweenLimits(ref offset);

        PagedCollection<Effect> pagedCollection = _effects.List(limit, offset);

        if (pagedCollection.PreviousPage is not null)
        {
            string? prevUrl = Url.ActionLink(
                action: nameof(GetAllEffects),
                values: new { limit, offset = pagedCollection.PreviousPage });
            Response.Headers["X-Previous"] = prevUrl;
        }

        if (pagedCollection.NextPage is not null)
        {
            string? nextUrl = Url.ActionLink(
                action: nameof(GetAllEffects),
                values: new { limit, offset = pagedCollection.NextPage });
            Response.Headers["X-Next"] = nextUrl;
        }

        Response.Headers["X-Max-Offset"] = pagedCollection.LastPage.ToString();

        return _mapper.Map<IEnumerable<EffectLimited>>(pagedCollection.Collection);
    }

    [HttpGet("{effectId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<EffectDto> GetEffect(int effectId)
    {
        Effect? effect = _effects.Get(effectId);
        if (effect is null) return NotFound();

        string? url = Url.ActionLink(
            action: nameof(GetEffect),
            values: new { effectId = effect.Id });
        var effectToReturn = new EffectDto { Url = url };
        _mapper.Map(effect, effectToReturn);

        return Ok(effectToReturn);
    }
}
