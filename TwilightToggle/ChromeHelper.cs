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
                    return true;
                }
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
