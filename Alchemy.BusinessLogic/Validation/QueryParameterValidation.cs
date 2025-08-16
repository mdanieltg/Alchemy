namespace Alchemy.BusinessLogic.Validation;

public static class QueryParameterValidation
{
    public static void EnsureLimitIsBetweenLimits(ref int limit)
    {
        if (limit < 1) limit = 1;
        if (limit > 20) limit = 20;
    }

    public static void EnsureOffsetIsBetweenLimits(ref int offset)
    {
        if (offset < 1) offset = 1;
    }
}
