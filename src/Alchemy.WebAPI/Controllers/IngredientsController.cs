using System.Net.Mime;
using Alchemy.BusinessLogic.Models;
using Alchemy.BusinessLogic.Repositories;
using Alchemy.DataModel.Entities;
using Alchemy.WebAPI.Models;
using Alchemy.WebAPI.Validation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/ingredients")]
[Produces(MediaTypeNames.Application.Json)]
public class IngredientsController(IPagedRepository<Ingredient> ingredientsRepository, IMapper mapper)
    : ControllerBase
{
    private readonly IPagedRepository<Ingredient> _ingredients = ingredientsRepository;
    private readonly IMapper _mapper = mapper;

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
            Response.Headers["X-Previous"] = prevUrl;
        }

        if (pagedCollection.NextPage is not null)
        {
            string? nextUrl = Url.ActionLink(
                action: nameof(GetIngredients),
                values: new { limit, offset = pagedCollection.NextPage });
            Response.Headers["X-Next"] = nextUrl;
        }

        Response.Headers["X-Max-Offset"] = pagedCollection.LastPage.ToString();

        return _mapper.Map<IEnumerable<IngredientLimited>>(pagedCollection.Collection);
    }

    [HttpGet("{ingredientId:int}")]
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
