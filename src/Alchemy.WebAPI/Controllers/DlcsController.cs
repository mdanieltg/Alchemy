using System.Net.Mime;
using Alchemy.Application.Repositories;
using Alchemy.DataModel.Entities;
using Alchemy.WebAPI.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/dlcs")]
[Produces(MediaTypeNames.Application.Json)]
public class DlcsController(IRepository<DownloadableContent> dlcRepository, IMapper mapper)
    : ControllerBase
{
    private readonly IRepository<DownloadableContent> _dlcs = dlcRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<DownloadableContentDto> GetDlcs()
    {
        return _mapper.Map<IEnumerable<DownloadableContentDto>>(_dlcs.List());
    }

    [HttpGet("{dlcId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DownloadableContentDto> GetDlc(int dlcId)
    {
        DownloadableContent? dlc = _dlcs.Get(dlcId);

        return dlc is null
            ? NotFound()
            : Ok(_mapper.Map<DownloadableContentDto>(dlc));
    }
}
