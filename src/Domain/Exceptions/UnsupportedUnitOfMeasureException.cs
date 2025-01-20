namespace Void.Chef.Domain.Exceptions;

public class UnsupportedUnitOfMeasureException(string code) : Exception($"Unit of measure \"{code}\" is unsupported.");