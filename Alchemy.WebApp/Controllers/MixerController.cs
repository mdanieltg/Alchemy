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
        return View();
    }

    [HttpPost]
    public IActionResult Index(ItemsSelection selection)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IngredientsAsJson = ToJsonString(_ingredients.GetAll());
            return View(selection);
        }

        return RedirectToAction("Mix", new { selection.Ingredients });
    }

    [HttpGet("mix")]
    public async Task<IActionResult> Mix(
        [FromQuery] IEnumerable<int> Ingredients,
        [FromServices] IMixer mixer)
    {
        var mixes = await mixer.Mix(Ingredients);
        return View(mixes);
    }

    private static string ToJsonString(IEnumerable<Ingredient> ingredients)
    {
        return JsonSerializer.Serialize(
            ingredients.Select(i => new
            {
                i.Id,
                i.Name
            }),
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}
