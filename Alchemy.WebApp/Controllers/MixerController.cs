using System.Text.Json;
using Alchemy.BusinessLogic.Contracts;
using Alchemy.BusinessLogic.Services;
using Alchemy.DataModel.Entities;
using Alchemy.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebApp.Controllers;

[Route("/mixer")]
public class MixerController : Controller
{
    private readonly IIngredientsRepository _ingredients;

    public MixerController(IIngredientsRepository ingredients)
    {
        _ingredients = ingredients;
    }

    public IActionResult Index()
    {
        ViewBag.IngredientsAsJson = ToJsonString(_ingredients.GetAll());
        ViewBag.Ingredients = _ingredients.GetAll();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromServices] IMixer mixer, ItemsSelection selection)
    {
        if (!ModelState.IsValid || selection.Ingredients is null)
        {
            ViewBag.IngredientsAsJson = ToJsonString(_ingredients.GetAll());
            ViewBag.Ingredients = _ingredients.GetAll();
            return View(selection);
        }

        var mixes = await mixer.Mix(selection.Ingredients);
        return View("Mix", mixes);
    }

    private static string ToJsonString(IEnumerable<Ingredient> ingredients)
    {
        return JsonSerializer.Serialize(
            ingredients.Select(i => new
            {
                i.Id,
                i.Name,
                Selected = false
            }),
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}
