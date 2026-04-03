namespace Alchemy.Domain.Exceptions;

public class InvalidFileLocationException : Exception
{
    public InvalidFileLocationException(string file) : base($"The URI for {file} was missing from the settings.")
    {
    }
}
