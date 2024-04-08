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
            //read in user AM and PM
            //here

            //convert read in user AM and PM to Arrays
            //int[] userAMTimes = [HourInt, MinuteInt]
            //8:30AM
            int[] userAMTimes = { 8, 30 };

            //8:00PM
            int[] userPMTimes = { 20, 0 };


            //use in the method below
            twilightToggle.Main(userAMTimes, userPMTimes);

            Console.WriteLine("Reached the end of the program...");
            Console.ReadLine();
        }
    }
}
