using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightToggle
{
    public class ChromeHelper
    {
        public bool GetChromeRunState()
        {
            try
            {
                if (Process.GetProcessesByName("Chrome").Length > 0)
                {
                    //return true // 1 for Chrome found.
                    Console.WriteLine("(~) Chrome found...");
                    return true;
                }
                //return false // 0 for Chrome not found.
                Console.WriteLine("(~) Chrome not found!");
                return false;
            }
            catch
            {
                Console.WriteLine("Something fatal went wrong while fetching Chrome processes!");
                return true;
            }
            
        }
    }
}
