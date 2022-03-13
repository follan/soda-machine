using ConsoleApp1.Domain;

namespace ConsoleApp1;

public class SodaMachineService
{
    private static readonly Soda Coke = new() { Name = "coke", Price = 20 };
    private static readonly Soda Sprite = new() { Name = "sprite", Price = 15 };
    private static readonly Soda Fanta = new() { Name = "fanta", Price = 15 };
    private static readonly Soda Pepsi = new() { Name = "pepsi", Price = 16 };

    /// <summary>
    /// This is the starter method for the machine
    /// </summary>
    public void Start()
    {
        var inventoryLines = new[]
        {
            new InventoryLine { Soda = Coke, Quantity = 5 },
            new InventoryLine { Soda = Sprite, Quantity = 3 },
            new InventoryLine { Soda = Fanta, Quantity = 3 },
            new InventoryLine { Soda = Pepsi, Quantity = 1 }
        };

        var sodaMachine = new SodaMachine(inventoryLines);
        var availableSodas = sodaMachine.GetSodaNames();
        var availableSodasFormatted = string.Join(", ", availableSodas);

        while (true)
        {
            Console.WriteLine("\n\nAvailable commands:");
            Console.WriteLine("insert (money) - Money put into money slot");
            Console.WriteLine($"order ({availableSodasFormatted}) - Order from machines buttons");
            Console.WriteLine($"sms order ({availableSodasFormatted}) - Order sent by sms");
            Console.WriteLine("recall - gives money back");
            Console.WriteLine("-------");
            Console.WriteLine($"Inserted money: {sodaMachine.TotalMoney}");
            Console.WriteLine("-------\n\n");

            var input = Console.ReadLine();

            var isInsertCommand = input.StartsWith("insert");
            if (isInsertCommand)
            {
                var insertedMoney = int.Parse(input.Split(' ')[1]);
                sodaMachine.InsertMoney(insertedMoney);
                Console.WriteLine($"Adding {insertedMoney} to credit. New total credit is {sodaMachine.TotalMoney}");
            }

            var isOrderCommand = input.StartsWith("order");
            if (isOrderCommand)
            {
                // split string on space
                var orderedSodaName = input.Split(' ')[1];

                if (!sodaMachine.SodaExists(orderedSodaName))
                {
                    Console.WriteLine("No such soda");
                    continue;
                }

                if (!sodaMachine.HasQuantityAvailable(orderedSodaName))
                {
                    Console.WriteLine($"No {orderedSodaName} left");
                    continue;
                }

                if (!sodaMachine.HasEnoughBalance(orderedSodaName))
                {
                    var missingAmount = sodaMachine.GetMissingAmount(orderedSodaName);
                    Console.WriteLine($"Need {missingAmount} more");
                    continue;
                }

                Console.WriteLine($"Giving {orderedSodaName} out");
                sodaMachine.DispenseSoda(orderedSodaName);
                Console.WriteLine($"Giving {sodaMachine.TotalMoney} out in change");
                sodaMachine.RecallBalance();
            }

            var isSmsOrderCommand = input.StartsWith("sms order");
            if (isSmsOrderCommand)
            {
                var orderedSodaName = input.Split(' ')[2];
                if (!sodaMachine.SodaExists(orderedSodaName))
                {
                    Console.WriteLine("No such soda");
                    continue;
                }

                if (sodaMachine.HasQuantityAvailable(orderedSodaName))
                {
                    Console.WriteLine($"Giving {orderedSodaName} out");
                    sodaMachine.DispenseSmsOrderedSoda(orderedSodaName);
                }
            }

            var isRecallCommand = input.Equals("recall");
            if (isRecallCommand)
            {
                Console.WriteLine($"Returning {sodaMachine.TotalMoney} to customer");
                sodaMachine.RecallBalance();
            }
        }
    }
}