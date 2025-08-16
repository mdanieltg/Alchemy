using Alchemy.BusinessLogic.Services;
using Alchemy.BusinessLogic.Validation;
using Alchemy.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/ingredients")]
[Produces("application/json", "application/xml", "text/json", "text/xml")]
public class IngredientsController : ControllerBase
{
    private readonly IPagedIngredientsRepository _ingredients;
    private readonly IMapper _mapper;

    public IngredientsController(IPagedIngredientsRepository ingredientsRepository, IMapper mapper)
    {
        _ingredients = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet(Name = "ListIngredients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<IngredientLimited> GetIngredients([FromQuery] int limit = 20, [FromQuery] int offset = 1)
    {
        QueryParameterValidation.EnsureLimitIsBetweenLimits(ref limit);
        QueryParameterValidation.EnsureOffsetIsBetweenLimits(ref offset);

        if (offset > 1)
        {
            var prevUrl = Url.Link("ListIngredients", new { limit, offset = offset - 1 });
            Response.Headers.Add("X-Previous", prevUrl);
        }

        var nextUrl = Url.Link("ListIngredients", new { limit, offset = offset + 1 });
        Response.Headers.Add("X-Next", nextUrl);

        var pagedCollection = _ingredients.List(limit, offset);
        return _mapper.Map<IEnumerable<IngredientLimited>>(pagedCollection.Collection);
    }

    [HttpGet("{ingredientId}", Name = "GetIngredient")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Ingredient> GetIngredient(int ingredientId)
    {
        var ingredient = _ingredients.Get(ingredientId);
        if (ingredient is null) return NotFound();

        var url = Url.Link("GetIngredient", new { ingredientId = ingredient.Id });
        var ingredientToReturn = new Ingredient
        {
            Url = url
        };
        _mapper.Map(ingredient, ingredientToReturn);

        return Ok(ingredientToReturn);
    }
}
