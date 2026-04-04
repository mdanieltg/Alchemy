using System.Net.Mime;
using Alchemy.BusinessLogic.Models;
using Alchemy.BusinessLogic.Services;
using Alchemy.WebAPI.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/mix")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class MixController(IMixer mixer, IMapper mapper) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IMixer _mixer = mixer;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<MixDto>> Mix([FromBody] MixRequestModel model)
    {
        List<Mix> mixes = _mixer.Mix(new HashSet<int>(model.IngredientIds));
        return Ok(_mapper.Map<IEnumerable<MixDto>>(mixes));
    }
}
