namespace Alchemy.Application.Exceptions;

public class InvalidFileLocationException(string file)
    : Exception($"The URI for {file} was missing from the settings.");
