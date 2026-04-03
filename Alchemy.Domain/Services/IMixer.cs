using Alchemy.Domain.Models;

namespace Alchemy.Domain.Services;

public interface IMixer
{
    List<Mix> Mix(IReadOnlySet<int> ingredientIds);
}
