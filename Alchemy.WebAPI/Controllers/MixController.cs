using System.Net.Mime;
using Alchemy.Domain.Models;
using Alchemy.Domain.Services;
using Alchemy.WebAPI.Binders;
using Alchemy.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/mix")]
[Produces(MediaTypeNames.Application.Json)]
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
    public ActionResult<IEnumerable<MixDto>> Mix(
        [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
    {
        List<Mix> mixes = _mixer.Mix(new HashSet<int>(ids));
        return Ok(_mapper.Map<IEnumerable<MixDto>>(mixes));
    }
}
