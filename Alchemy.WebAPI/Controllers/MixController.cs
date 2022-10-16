using Alchemy.BusinessLogic.Services;
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Mix>>> Mix([FromQuery] IEnumerable<int> id)
    {
        var mixes = await _mixer.Mix(id);
        return Ok(_mapper.Map<IEnumerable<Mix>>(mixes));
    }
}
