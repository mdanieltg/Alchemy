namespace Alchemy.Domain.Exceptions;

public class UnreachableFileException : Exception
{
    public UnreachableFileException(string uri) : base($"The specified resource coulnd't be located or fetched: {uri}")
    {
    }
}
