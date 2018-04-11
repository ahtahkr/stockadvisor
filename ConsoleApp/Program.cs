using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = IEXTrading.WebApi.Api_1.Previous("aapl");
            Console.WriteLine(test);
            Console.ReadLine();
        }
    }
}
