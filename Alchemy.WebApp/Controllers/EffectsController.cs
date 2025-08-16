using Alchemy.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebApp.Controllers;

[Route("/effects")]
public class EffectsController : Controller
{
    private readonly IEffectsRepository _effects;

    public EffectsController(IEffectsRepository effects)
    {
        _effects = effects;
    }

    public IActionResult Index()
    {
        var effects = _effects.GetAll();
        return View(effects);
    }

    [HttpGet("{effectId}")]
    public IActionResult Detail(int effectId)
    {
        var effect = _effects.Get(effectId);
        if (effect is null)
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        return View(effect);
    }
}
