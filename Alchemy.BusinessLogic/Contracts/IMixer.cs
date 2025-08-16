namespace Alchemy.BusinessLogic.Contracts;

public interface IMixer
{
    Task<List<Mix>> Mix(IEnumerable<int> ingredientIds);
}
