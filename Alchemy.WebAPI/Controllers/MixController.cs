using Alchemy.WebAPI.Models;
using Alchemy.WebAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alchemy.WebAPI.Controllers;

[ApiController]
[Route("api/mix")]
public class MixController : ControllerBase
{
    private readonly IAlchemyRepository _repository;
    private readonly IMapper _mapper;

    public MixController(IAlchemyRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public ActionResult<IEnumerable<Mix>> Mix(IEnumerable<int> ingredientIds)
    {
        var mixes = new HashSet<Mix>();
        var ingredients = new HashSet<Ingredient>();
        var effectIngredientsDictionary = new Dictionary<Effect, HashSet<Ingredient>>();
        var allIngredients = _repository.GetIngredients().ToHashSet();

        foreach (var ingredientId in ingredientIds)
        {
            var ingredient = allIngredients.FirstOrDefault(ingredient => ingredient.Id == ingredientId);

            if (ingredient != null)
            {
                ingredients.Add(ingredient);
            }
        }

        foreach (var ingredient in ingredients)
        {
            foreach (var effect in ingredient.Effects)
            {
                if (effectIngredientsDictionary.ContainsKey(effect))
                {
                    effectIngredientsDictionary[effect].Add(ingredient);
                }
                else
                {
                    effectIngredientsDictionary.Add(effect, new HashSet<Ingredient>() { ingredient });
                }
            }
        }

        foreach (var keyValuePair in effectIngredientsDictionary
            .Where(pair => pair.Value.Count > 1)
            .OrderByDescending(pair => pair.Value.Count))
        {
            mixes.Add(new Mix
            {
                Effect = _mapper.Map<Alchemy.WebAPI.Models.EffectLimited>(keyValuePair.Key),
                Ingredients = _mapper.Map<IEnumerable<Alchemy.WebAPI.Models.IngredientLimited>>(keyValuePair.Value)
            });
        }

        return Ok(mixes);
    }
}
