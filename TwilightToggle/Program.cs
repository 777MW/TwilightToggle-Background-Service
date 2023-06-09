using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightToggle
{
    class Program
    {
        static void Main(string[] args)
        {
            TwilightToggle twilightToggle = new TwilightToggle();
            ChromeHelper chromeHelper = new ChromeHelper();

            twilightToggle.exampleMethod("example 1");
            chromeHelper.exampleMethod("example 2");

            Console.ReadLine();
        }
    }
}
