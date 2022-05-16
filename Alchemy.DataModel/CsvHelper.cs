using System.Globalization;
using Alchemy.DataModel.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;

namespace Alchemy.DataModel;

public class CsvHelper
{
    private readonly CsvConfiguration _configuration;
    private readonly string _dlcFile;
    private readonly string _effectsFile;
    private readonly string _ingredientsFile;
    private readonly string _ingredientEffectsFile;

    public CsvHelper(IConfiguration configuration)
    {
        _dlcFile = configuration.GetSection("CsvFiles:DLC").Value;
        _effectsFile = configuration.GetSection("CsvFiles:Effects").Value;
        _ingredientsFile = configuration.GetSection("CsvFiles:Ingredients").Value;
        _ingredientEffectsFile = configuration.GetSection("CsvFiles:IngredientEffects").Value;

        if (string.IsNullOrWhiteSpace(_dlcFile))
        {
            throw new ArgumentException("DLC file path missing or empty");
        }

        if (string.IsNullOrWhiteSpace(_effectsFile))
        {
            throw new ArgumentException("Effects file path missing or empty");
        }

        if (string.IsNullOrWhiteSpace(_ingredientsFile))
        {
            throw new ArgumentException("Ingredients file path missing or empty");
        }

        if (string.IsNullOrWhiteSpace(_ingredientEffectsFile))
        {
            throw new ArgumentException("IngredientEffects file path missing or empty");
        }

        _configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            NewLine = "\n"
        };
    }

    public async IAsyncEnumerable<DlcDto> GetDlcs()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(_dlcFile);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        using var csvReader = new CsvReader(streamReader, _configuration);

        var dlcs = csvReader.GetRecordsAsync<DlcDto>();
        await foreach (var dlc in dlcs)
        {
            yield return dlc;
        }
    }

    public async IAsyncEnumerable<EffectDto> GetEffects()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(_effectsFile);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        using var csvReader = new CsvReader(streamReader, _configuration);

        var effects = csvReader.GetRecordsAsync<EffectDto>();
        await foreach (var effect in effects)
        {
            yield return effect;
        }
    }

    public async IAsyncEnumerable<IngredientDto> GetIngredients()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(_ingredientsFile);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        using var csvReader = new CsvReader(streamReader, _configuration);

        var ingredients = csvReader.GetRecordsAsync<IngredientDto>();
        await foreach (var ingredient in ingredients)
        {
            yield return ingredient;
        }
    }

    public async IAsyncEnumerable<IngredientEffects> GetIngredientEffects()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(_ingredientEffectsFile);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        using var csvReader = new CsvReader(streamReader, _configuration);

        var ingredientEffects = csvReader.GetRecordsAsync<IngredientEffects>();
        await foreach (var ingredientEffect in ingredientEffects)
        {
            yield return ingredientEffect;
        }
    }
}
