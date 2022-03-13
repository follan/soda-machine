using System.Xml;

namespace ConsoleApp1;

public class SodaMachine
{
    private static int _totalMoney;
    private static readonly Soda Coke = new() { Name = "coke", Price = 20 };
    private static readonly Soda Sprite = new() { Name = "sprite", Price = 15 };
    private static readonly Soda Fanta = new() { Name = "fanta", Price = 15 };

    /// <summary>
    /// This is the starter method for the machine
    /// </summary>
    public void Start()
    {
        var inventory = new[]
        {
            new InventoryLine { Soda = Coke, Quantity = 5 },
            new InventoryLine { Soda = Sprite, Quantity = 3 },
            new InventoryLine { Soda = Fanta, Quantity = 3 }
        };

        while (true)
        {
            Console.WriteLine("\n\nAvailable commands:");
            Console.WriteLine("insert (money) - Money put into money slot");
            Console.WriteLine("order (coke, sprite, fanta) - Order from machines buttons");
            Console.WriteLine("sms order (coke, sprite, fanta) - Order sent by sms");
            Console.WriteLine("recall - gives money back");
            Console.WriteLine("-------");
            Console.WriteLine("Inserted money: " + _totalMoney);
            Console.WriteLine("-------\n\n");

            var input = Console.ReadLine();

            if (input.StartsWith("insert"))
            {
                var insertedMoney = int.Parse(input.Split(' ')[1]);
                _totalMoney += insertedMoney;
                Console.WriteLine($"Adding {insertedMoney} to credit. New total credit is {_totalMoney}");
            }

            if (input.StartsWith("order"))
            {
                // split string on space
                var orderedSodaName = input.Split(' ')[1];
                var orderedSoda = inventory.SingleOrDefault(s => s.Soda.Name == orderedSodaName);
                if (orderedSoda is null)
                {
                    Console.WriteLine("No such soda");
                    continue;
                }

                if (orderedSoda.Quantity == 0)
                {
                    Console.WriteLine($"No {orderedSoda.Soda.Name} left");
                    continue;
                }

                if (_totalMoney < orderedSoda.Soda.Price)
                {
                    var missingAmount = orderedSoda.Soda.Price - _totalMoney;
                    Console.WriteLine($"Need {missingAmount} more");
                    continue;
                }

                Console.WriteLine($"Giving {orderedSoda.Soda.Name} out");
                _totalMoney -= orderedSoda.Soda.Price;
                Console.WriteLine("Giving " + _totalMoney + " out in change");
                _totalMoney = 0;
                orderedSoda.Quantity--;
            }

            if (input.StartsWith("sms order"))
            {
                var orderedSodaName = input.Split(' ')[2];
                var orderedSoda = inventory.SingleOrDefault(s => s.Soda.Name == orderedSodaName);
                if (orderedSoda is null)
                {
                    continue;
                }

                if (orderedSoda.Quantity > 0)
                {
                    Console.WriteLine($"Giving {orderedSoda.Soda.Name} out");
                    orderedSoda.Quantity--;
                }
            }

            if (input.Equals("recall"))
            {
                //Give money back
                Console.WriteLine("Returning " + _totalMoney + " to customer");
                _totalMoney = 0;
            }
        }
    }
}