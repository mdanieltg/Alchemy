using Alchemy.BusinessLogic.Contracts;
using Alchemy.BusinessLogic.Services;
using Alchemy.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/effects")]
[Produces("application/json", "application/xml", "text/json", "text/xml")]
public class EffectsController : ControllerBase
{
    private readonly IEffectsRepository _effects;
    private readonly IMapper _mapper;

    public EffectsController(IEffectsRepository effectsRepository, IMapper mapper)
    {
        _effects = effectsRepository ?? throw new ArgumentNullException(nameof(effectsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Effect> GetAllEffects()
    {
        return _mapper.Map<IEnumerable<Effect>>(_effects.GetAll().ToList());
    }

    [HttpGet("{effectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Effect> GetEffect(int effectId)
    {
        var effect = _effects.Get(effectId);

        return effect == null
            ? NotFound()
            : Ok(_mapper.Map<Effect>(effect));
    }
}
