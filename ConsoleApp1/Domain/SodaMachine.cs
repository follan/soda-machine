using ConsoleApp1.Exceptions;

namespace ConsoleApp1.Domain;

public class SodaMachine
{
    private InventoryLine[] InventoryLines { get; }
    public int TotalMoney { get; private set; }

    public SodaMachine(InventoryLine[] inventoryLines)
    {
        InventoryLines = inventoryLines;
    }

    public bool SodaExists(string sodaName)
    {
        return InventoryLines.SingleOrDefault(s => s.Soda.Name == sodaName) != null;
    }

    public bool HasQuantityAvailable(string sodaName)
    {
        return InventoryLines.SingleOrDefault(s => s.Soda.Name == sodaName)?.Quantity > 0;
    }

    public bool HasEnoughBalance(string sodaName)
    {
        var sodaToBuy = InventoryLines.SingleOrDefault(s => s.Soda.Name == sodaName)?.Soda;
        if (sodaToBuy is null)
        {
            throw new SodaDoesNotExistException();
        }
        
        return TotalMoney >= sodaToBuy?.Price;
    }

    public void InsertMoney(int money)
    {
        TotalMoney += money;
    }

    public void DispenseSoda(string sodaName)
    {
        var inventoryLine = InventoryLines.SingleOrDefault(s => s.Soda.Name == sodaName);
        if (inventoryLine is null)
        {
            throw new SodaDoesNotExistException();
        }

        if (TotalMoney < inventoryLine.Soda.Price)
        {
            throw new TotalMoneyNotEnoughException();
        }

        if (inventoryLine.Quantity == 0)
        {
            throw new NoSodasRemainingException(inventoryLine.Soda);
        }

        inventoryLine.Quantity--;
        TotalMoney -= inventoryLine.Soda.Price;
    }

    public void DispenseSmsOrderedSoda(string sodaName)
    {
        var inventoryLine = InventoryLines.SingleOrDefault(s => s.Soda.Name == sodaName);
        if (inventoryLine is null)
        {
            throw new SodaDoesNotExistException();
        }

        if (inventoryLine.Quantity == 0)
        {
            throw new NoSodasRemainingException(inventoryLine.Soda);
        }

        inventoryLine.Quantity--;
    }

    public void RecallBalance()
    {
        TotalMoney = 0;
    }

    public IEnumerable<string> GetSodaNames()
    {
        return InventoryLines.Select(x => x.Soda.Name);
    }

    public int GetMissingAmount(string sodaName)
    {
        var inventoryLine = InventoryLines.SingleOrDefault(s => s.Soda.Name == sodaName);
        if (inventoryLine is null)
        {
            throw new SodaDoesNotExistException();
        }

        if (TotalMoney >= inventoryLine.Soda.Price)
        {
            return 0;
        }

        return inventoryLine.Soda.Price - TotalMoney;
    }
}