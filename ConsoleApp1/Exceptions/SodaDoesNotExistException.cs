namespace ConsoleApp1.Exceptions;

public class SodaDoesNotExistException : Exception
{
    public SodaDoesNotExistException() : base("Soda does not exist")
    {
    }
}