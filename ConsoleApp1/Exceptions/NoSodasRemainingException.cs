using ConsoleApp1.Domain;

namespace ConsoleApp1.Exceptions;

public class NoSodasRemainingException : Exception
{
    public NoSodasRemainingException(Soda soda) : base($"No {soda.Name} sodas remaining")
    {
    }
}