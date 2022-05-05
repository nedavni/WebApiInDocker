using System;
using DatabaseLayer.Data;

namespace ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseContext.SeedData();
            Console.WriteLine("Hello Wljklj,orld!");
        }
    }
}
