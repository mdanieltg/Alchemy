using System.Globalization;
using System.Net;
using System.Security.Authentication;
using Alchemy.Domain.Exceptions;
using Alchemy.WebAPI.CsvModels;
using CsvHelper;
using CsvHelper.Configuration;

namespace Alchemy.WebAPI.Services;

public class CsvHelperService
{
    private const string CsvFileDlc = "CsvFiles:DLC";
    private const string CsvFileEffects = "CsvFiles:Effects";
    private const string CsvFileIngredients = "CsvFiles:Ingredients";
    private const string CsvFileIngredienteffects = "CsvFiles:IngredientEffects";

    private readonly CsvConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly string _dlcFileLocation;
    private readonly string _effectsFileLocation;
    private readonly string _ingredientEffectsFileLocation;
    private readonly string _ingredientsFileLocation;

    public CsvHelperService(IConfiguration configuration)
    {
        _dlcFileLocation = configuration.GetSection(CsvFileDlc).Value ??
                           throw new InvalidFileLocationException(CsvFileDlc);
        _effectsFileLocation = configuration.GetSection(CsvFileEffects).Value ??
                               throw new InvalidFileLocationException(CsvFileEffects);
        _ingredientsFileLocation = configuration.GetSection(CsvFileIngredients).Value ??
                                   throw new InvalidFileLocationException(CsvFileIngredients);
        _ingredientEffectsFileLocation = configuration.GetSection(CsvFileIngredienteffects).Value ??
                                         throw new InvalidFileLocationException(CsvFileIngredienteffects);

        if (string.IsNullOrWhiteSpace(_dlcFileLocation))
            throw new ArgumentException("DLC file path missing or empty");

        if (string.IsNullOrWhiteSpace(_effectsFileLocation))
            throw new ArgumentException("Effects file path missing or empty");

        if (string.IsNullOrWhiteSpace(_ingredientsFileLocation))
            throw new ArgumentException("Ingredients file path missing or empty");

        if (string.IsNullOrWhiteSpace(_ingredientEffectsFileLocation))
            throw new ArgumentException("IngredientEffects file path missing or empty");

        _configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            NewLine = "\n"
        };

        _httpClient = new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13
        });
    }

    private async Task<StreamReader> Fetch(string uri)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            Stream contentStream = await response.Content.ReadAsStreamAsync();
            return new StreamReader(contentStream);
        }
        catch (HttpRequestException)
        {
            throw new UnreachableFileException(uri);
        }
    }

    public async IAsyncEnumerable<DlcDto> GetDlcs()
    {
        using StreamReader streamReader = await Fetch(_dlcFileLocation);
        using var csvReader = new CsvReader(streamReader, _configuration);

        IAsyncEnumerable<DlcDto> dlcs = csvReader.GetRecordsAsync<DlcDto>();
        await foreach (DlcDto dlc in dlcs)
        {
            yield return dlc;
        }
    }

    public async IAsyncEnumerable<EffectDto> GetEffects()
    {
        using StreamReader streamReader = await Fetch(_effectsFileLocation);
        using var csvReader = new CsvReader(streamReader, _configuration);

        IAsyncEnumerable<EffectDto> effects = csvReader.GetRecordsAsync<EffectDto>();
        await foreach (EffectDto effect in effects)
            yield return effect;
    }

    public async IAsyncEnumerable<IngredientDto> GetIngredients()
    {
        using StreamReader streamReader = await Fetch(_ingredientsFileLocation);
        using var csvReader = new CsvReader(streamReader, _configuration);

        IAsyncEnumerable<IngredientDto> ingredients = csvReader.GetRecordsAsync<IngredientDto>();
        await foreach (IngredientDto ingredient in ingredients)
            yield return ingredient;
    }

    public async IAsyncEnumerable<IngredientEffectsDto> GetIngredientEffects()
    {
        using StreamReader streamReader = await Fetch(_ingredientEffectsFileLocation);
        using var csvReader = new CsvReader(streamReader, _configuration);

        IAsyncEnumerable<IngredientEffectsDto> ingredientEffects = csvReader.GetRecordsAsync<IngredientEffectsDto>();
        await foreach (IngredientEffectsDto ingredientEffect in ingredientEffects)
            yield return ingredientEffect;
    }
}
