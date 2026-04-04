namespace Alchemy.Application.Exceptions;

public class UnreachableFileException(string uri)
    : Exception($"The specified resource coulnd't be located or fetched: {uri}");
