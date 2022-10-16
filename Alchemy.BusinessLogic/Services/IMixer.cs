namespace Alchemy.BusinessLogic.Services;

public interface IMixer
{
    Task<List<Mix>> Mix(IEnumerable<int> ingredientIds);
}
