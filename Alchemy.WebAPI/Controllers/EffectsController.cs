using Alchemy.BusinessLogic.Services;
using Alchemy.BusinessLogic.Validation;
using Alchemy.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/effects")]
[Produces("application/json", "application/xml", "text/json", "text/xml")]
public class EffectsController : ControllerBase
{
    private readonly IPagedEffectsRepository _effects;
    private readonly IMapper _mapper;

    public EffectsController(IPagedEffectsRepository effectsRepository, IMapper mapper)
    {
        _effects = effectsRepository ?? throw new ArgumentNullException(nameof(effectsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet(Name = "ListEffects")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<EffectLimited> GetAllEffects([FromQuery] int limit = 10, [FromQuery] int offset = 1)
    {
        QueryParameterValidation.EnsureLimitIsBetweenLimits(ref limit);
        QueryParameterValidation.EnsureOffsetIsBetweenLimits(ref offset);

        if (offset > 1)
        {
            var prevUrl = Url.Link("ListEffects", new { limit, offset = offset - 1 });
            Response.Headers.Add("X-Previous", prevUrl);
        }

        var nextUrl = Url.Link("ListEffects", new { limit, offset = offset + 1 });
        Response.Headers.Add("X-Next", nextUrl);

        var pagedCollection = _effects.List(limit, offset);
        return _mapper.Map<IEnumerable<EffectLimited>>(pagedCollection.Collection);
    }

    [HttpGet("{effectId}", Name = "GetEffect")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Effect> GetEffect(int effectId)
    {
        var effect = _effects.Get(effectId);
        if (effect is null) return NotFound();

        var url = Url.Link("GetEffect", new { effectId = effect.Id });
        var effectToReturn = new Effect
        {
            Url = url
        };
        _mapper.Map(effect, effectToReturn);

        return Ok(effectToReturn);
    }
}
