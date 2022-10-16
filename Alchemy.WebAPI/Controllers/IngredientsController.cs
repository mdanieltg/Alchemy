using Alchemy.BusinessLogic.Contracts;
using Alchemy.BusinessLogic.Services;
using Alchemy.WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/ingredients")]
[Produces("application/json", "application/xml", "text/json", "text/xml")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientsRepository _ingredients;
    private readonly IMapper _mapper;

    public IngredientsController(IIngredientsRepository ingredientsRepository, IMapper mapper)
    {
        _ingredients = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Ingredient> GetIngredients() =>
        _mapper.Map<IEnumerable<Ingredient>>(_ingredients.GetAll().ToList());

    [HttpGet("{ingredientId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Ingredient> GetIngredient(int ingredientId)
    {
        var ingredient = _ingredients.Get(ingredientId);

        return ingredient == null
            ? NotFound()
            : Ok(_mapper.Map<Ingredient>(ingredient));
    }
}
