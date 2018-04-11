using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IEXTrading.Operator.Save_Previous_to_File(IEXTrading.Web_Api_Version.One_point_Zero, "IEXTrading");
            Console.WriteLine("done");
            IEXTrading.Operator.Save_Chart_Range_to_File(IEXTrading.Web_Api_Version.One_point_Zero, "IEXTrading", "aapl","3m");
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
