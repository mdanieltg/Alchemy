using System.Net.Mime;
using Alchemy.BusinessLogic.Validation;
using Alchemy.Domain.Entities;
using Alchemy.Domain.Models;
using Alchemy.Domain.Repositories;
using Alchemy.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/ingredients")]
[Produces(MediaTypeNames.Application.Json)]
public class IngredientsController : ControllerBase
{
    private readonly IPagedRepository<Ingredient> _ingredients;
    private readonly IMapper _mapper;

    public IngredientsController(IPagedRepository<Ingredient> ingredientsRepository, IMapper mapper)
    {
        _ingredients = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<IngredientLimited> GetIngredients([FromQuery] int limit = 20, [FromQuery] int offset = 1)
    {
        QueryParameterValidation.EnsureLimitIsBetweenLimits(ref limit);
        QueryParameterValidation.EnsureOffsetIsBetweenLimits(ref offset);

        PagedCollection<Ingredient> pagedCollection = _ingredients.List(limit, offset);

        if (pagedCollection.PreviousPage is not null)
        {
            string? prevUrl = Url.ActionLink(
                action: nameof(GetIngredients),
                values: new { limit, offset = pagedCollection.PreviousPage });
            Response.Headers.Add("X-Previous", prevUrl);
        }

        if (pagedCollection.NextPage is not null)
        {
            string? nextUrl = Url.ActionLink(
                action: nameof(GetIngredients),
                values: new { limit, offset = pagedCollection.NextPage });
            Response.Headers.Add("X-Next", nextUrl);
        }

        Response.Headers.Add("X-MaxOffset", pagedCollection.LastPage.ToString());

        return _mapper.Map<IEnumerable<IngredientLimited>>(pagedCollection.Collection);
    }

    [HttpGet("{ingredientId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IngredientDto> GetIngredient(int ingredientId)
    {
        Ingredient? ingredient = _ingredients.Get(ingredientId);
        if (ingredient is null) return NotFound();

        string? url = Url.ActionLink(
            action: nameof(GetIngredient),
            values: new { ingredientId = ingredient.Id });

        var ingredientToReturn = new IngredientDto { Url = url };
        _mapper.Map(ingredient, ingredientToReturn);

        return Ok(ingredientToReturn);
    }
}
