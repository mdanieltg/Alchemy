using Alchemy.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebApp.Controllers;

[Route("/ingredients")]
public class IngredientsController : Controller
{
    private readonly IIngredientsRepository _ingredients;

    public IngredientsController(IIngredientsRepository ingredients)
    {
        _ingredients = ingredients;
    }

    public IActionResult Index()
    {
        var ingredients = _ingredients.List();
        return View(ingredients);
    }

    [HttpGet("{ingredientId}")]
    public IActionResult Detail(int ingredientId)
    {
        var ingredient = _ingredients.Get(ingredientId);
        if (ingredient is null)
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        return View(ingredient);
    }
}
