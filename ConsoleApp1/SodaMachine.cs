namespace ConsoleApp1;

public class SodaMachine
{
    private static int _totalMoney;


    /// <summary>
    /// This is the starter method for the machine
    /// </summary>
    public void Start()
    {
        var inventory = new[]
        {
            new Soda { Name = "coke", Nr = 5, Price = 20 }, new Soda { Name = "sprite", Nr = 3, Price = 15 }, new Soda { Name = "fanta", Nr = 3, Price = 15 }
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
                var orderedSoda = inventory.SingleOrDefault(s => s.Name == orderedSodaName);
                if (orderedSoda is null)
                {
                    Console.WriteLine("No such soda");
                    continue;
                }

                if (orderedSoda.Nr == 0)
                {
                    Console.WriteLine($"No {orderedSoda.Name} left");
                    continue;
                }

                if (_totalMoney < orderedSoda.Price)
                {
                    var missingAmount = orderedSoda.Price - _totalMoney;
                    Console.WriteLine($"Need {missingAmount} more");
                    continue;
                }

                Console.WriteLine($"Giving {orderedSoda.Name} out");
                _totalMoney -= orderedSoda.Price;
                Console.WriteLine("Giving " + _totalMoney + " out in change");
                _totalMoney = 0;
                orderedSoda.Nr--;
            }

            if (input.StartsWith("sms order"))
            {
                var orderedSodaName = input.Split(' ')[2];
                var orderedSoda = inventory.SingleOrDefault(s => s.Name == orderedSodaName);
                if (orderedSoda is null)
                {
                    continue;
                }
                
                if (orderedSoda.Nr > 0)
                {
                    Console.WriteLine($"Giving {orderedSoda.Name} out");
                    orderedSoda.Nr--;
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