using System.Net.Mime;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Repositories;
using Alchemy.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/dlcs")]
[Produces(MediaTypeNames.Application.Json)]
public class DlcsController : ControllerBase
{
    private readonly IRepository<DownloadableContent> _dlcs;
    private readonly IMapper _mapper;

    public DlcsController(IRepository<DownloadableContent> dlcRepository, IMapper mapper)
    {
        _dlcs = dlcRepository ?? throw new ArgumentNullException(nameof(dlcRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<DownloadableContentDto> GetDlcs()
    {
        return _mapper.Map<IEnumerable<DownloadableContentDto>>(_dlcs.List());
    }

    [HttpGet("{dlcId}")]
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
