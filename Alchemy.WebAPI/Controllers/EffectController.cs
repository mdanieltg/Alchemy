using Alchemy.WebAPI.Models;
using Alchemy.WebAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/effect")]
public class EffectController : ControllerBase
{
    private readonly IAlchemyRepository _repository;
    private readonly IMapper _mapper;

    public EffectController(IAlchemyRepository alchemyRepository, IMapper mapper)
    {
        _repository = alchemyRepository ?? throw new ArgumentNullException(nameof(alchemyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public IEnumerable<Effect> GetAllEffects() => _mapper.Map<IEnumerable<Effect>>(_repository.GetEffects());

    [HttpGet("{effectId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<Effect> GetEffect(int effectId)
    {
        var effect = _repository.GetEffect(effectId);

        return effect == null
            ? NotFound()
            : Ok(_mapper.Map<Effect>(effect));
    }
}
