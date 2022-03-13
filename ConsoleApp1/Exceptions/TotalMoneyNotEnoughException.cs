namespace ConsoleApp1.Exceptions;

public class TotalMoneyNotEnoughException : Exception
{
    public TotalMoneyNotEnoughException() : base("Not enough money")
    {
    }
}