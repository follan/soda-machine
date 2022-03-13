namespace ConsoleApp1;

public class Inventory
{
    public InventoryLine[] InventoryLines { get; set; }

    public Inventory(InventoryLine[] inventoryLines)
    {
        InventoryLines = inventoryLines;
    }
}