using Alchemy.Application.Models;

namespace Alchemy.Application.Services;

public interface IMixer
{
    List<Mix> Mix(IReadOnlySet<int> ingredientIds);
}
