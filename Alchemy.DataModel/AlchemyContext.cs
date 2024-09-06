using Alchemy.Domain.Entities;

namespace Alchemy.DataModel;

public sealed class AlchemyContext
{
    public required IReadOnlySet<DownloadableContent> Dlcs { get; init; }
    public required IReadOnlySet<Effect> Effects { get; init; }
    public required IReadOnlySet<Ingredient> Ingredients { get; init; }
}
