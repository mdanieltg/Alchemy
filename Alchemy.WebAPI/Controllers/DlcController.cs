using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Alchemy.WebAPI.Models;
using Alchemy.WebAPI.Services;

[ApiController]
[Route("api/dlc")]
public class DlcController : ControllerBase
{
    private readonly IAlchemyRepository _repository;
    private readonly IMapper _mapper;

    public DlcController(IAlchemyRepository alchemyRepository, IMapper mapper)
    {
        _repository = alchemyRepository ?? throw new ArgumentNullException(nameof(alchemyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public IEnumerable<Dlc> GetDlcs() => _mapper.Map<IEnumerable<Dlc>>(_repository.GetDlcs());

    [HttpGet("{dlcId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<Dlc> GetDlc(int dlcId)
    {
        var dlc = _repository.GetDlc(dlcId);

        return dlc == null
            ? NotFound()
            : Ok(_mapper.Map<Dlc>(dlc));
    }
}
