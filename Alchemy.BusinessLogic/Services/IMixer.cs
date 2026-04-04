using Alchemy.BusinessLogic.Models;

namespace Alchemy.BusinessLogic.Services;

public interface IMixer
{
    List<Mix> Mix(IReadOnlySet<int> ingredientIds);
}
