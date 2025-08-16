using Alchemy.BusinessLogic.Services;
using Alchemy.WebAPI.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/mix")]
[Produces("application/json", "application/xml", "text/json", "text/xml")]
public class MixController : ControllerBase
{
    private readonly IMixer _mixer;
    private readonly IMapper _mapper;

    public MixController(IMixer mixer, IMapper mapper)
    {
        _mixer = mixer ?? throw new ArgumentNullException(nameof(mixer));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{ids}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Mix>>> Mix(
        [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))]
        IEnumerable<int> ids)
    {
        var mixes = await _mixer.Mix(ids);
        return Ok(_mapper.Map<IEnumerable<Mix>>(mixes));
    }
}
