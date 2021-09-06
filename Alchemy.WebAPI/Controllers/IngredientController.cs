using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Alchemy.WebAPI.Models;
using Alchemy.WebAPI.Services;

[ApiController]
[Route("api/ingredient")]
public class IngredientController : ControllerBase
{
    private readonly IAlchemyRepository _repository;
    private readonly IMapper _mapper;

    public IngredientController(IAlchemyRepository alchemyRepository, IMapper mapper)
    {
        _repository = alchemyRepository ?? throw new ArgumentNullException(nameof(alchemyRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public IEnumerable<Ingredient> GetIngredients() =>
        _mapper.Map<IEnumerable<Ingredient>>(_repository.GetIngredients());

    [HttpGet("{ingredientId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public ActionResult<Ingredient> GetIngredient(int ingredientId)
    {
        var ingredient = _repository.GetIngredient(ingredientId);

        return ingredient == null
            ? NotFound()
            : Ok(_mapper.Map<Ingredient>(ingredient));
    }
}
