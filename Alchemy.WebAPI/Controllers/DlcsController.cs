using Alchemy.BusinessLogic.Services;
using Alchemy.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/dlcs")]
[Produces("application/json", "application/xml", "text/json", "text/xml")]
public class DlcsController : ControllerBase
{
    private readonly IDlcRepository _dlcs;
    private readonly IMapper _mapper;

    public DlcsController(IDlcRepository dlcRepository, IMapper mapper)
    {
        _dlcs = dlcRepository ?? throw new ArgumentNullException(nameof(dlcRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Dlc> GetDlcs()
    {
        return _mapper.Map<IEnumerable<Dlc>>(_dlcs.List());
    }

    [HttpGet("{dlcId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Dlc> GetDlc(int dlcId)
    {
        var dlc = _dlcs.Get(dlcId);

        return dlc == null
            ? NotFound()
            : Ok(_mapper.Map<Dlc>(dlc));
    }
}
