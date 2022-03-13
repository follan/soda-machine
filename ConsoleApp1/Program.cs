namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sodaMachineService = new SodaMachineService();
            sodaMachineService.Start();
        }
    }
}