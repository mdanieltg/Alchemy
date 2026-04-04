using System.Globalization;
using Alchemy.Application.Exceptions;
using Alchemy.WebAPI.CsvModels;
using CsvHelper;
using CsvHelper.Configuration;

namespace Alchemy.WebAPI.Services;

public class CsvHelperService(IConfiguration configuration)
{
    private const string CsvFileDlc = "CsvFiles:DLC";
    private const string CsvFileEffects = "CsvFiles:Effects";
    private const string CsvFileIngredients = "CsvFiles:Ingredients";
    private const string CsvFileIngredientEffects = "CsvFiles:IngredientEffects";

    private readonly CsvConfiguration _configuration = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        Delimiter = ",",
        NewLine = "\n"
    };

    private readonly string _dlcFile = configuration[CsvFileDlc] ?? throw new InvalidFileLocationException(CsvFileDlc);

    private readonly string _effectFile = configuration[CsvFileEffects] ??
                                          throw new InvalidFileLocationException(CsvFileEffects);

    private readonly string _ingredientsFile = configuration[CsvFileIngredients] ??
                                               throw new InvalidFileLocationException(CsvFileIngredients);

    private readonly string _ingredientEffectsFile = configuration[CsvFileIngredientEffects] ??
                                                     throw new InvalidFileLocationException(CsvFileIngredientEffects);

    private async ValueTask<IEnumerable<T>> ReadFile<T>(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));

        await using FileStream fileStream = File.OpenRead(fileName);
        using StreamReader streamReader = new(fileStream);
        using CsvReader csvReader = new(streamReader, _configuration);
        return [..csvReader.GetRecords<T>()];
    }

    public async ValueTask<IEnumerable<DlcDto>> GetDlcs() => await ReadFile<DlcDto>(_dlcFile);

    public async ValueTask<IEnumerable<IngredientDto>> GetIngredients() =>
        await ReadFile<IngredientDto>(_ingredientsFile);

    public async ValueTask<IEnumerable<EffectDto>> GetEffects() => await ReadFile<EffectDto>(_effectFile);

    public async ValueTask<IEnumerable<IngredientsEffectsDto>> GetIngredientEffects() =>
        await ReadFile<IngredientsEffectsDto>(_ingredientEffectsFile);
}
