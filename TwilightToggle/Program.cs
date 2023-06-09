using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace TwilightToggle
{
    class Program
    {
        static void Main(string[] args)
        {
            TwilightToggle twilightToggle = new TwilightToggle();
            //read in user AM and PM here
            //use in the method below
            twilightToggle.Main("example", "example2");

            Console.WriteLine("Reached the end of the program...");
            Console.ReadLine();
        }
    }
}
