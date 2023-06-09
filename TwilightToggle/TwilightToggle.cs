using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TwilightToggle
{
    public class TwilightToggle
    {
        public String Main(String passedString)
        {
            ChromeHelper chromeHelper = new ChromeHelper();
            while (true)
            {
                //main variable
                bool chromeRunState = chromeHelper.GetChromeRunState();
                Thread.Sleep(1000);
            }
            
            return "reached end of TwilightToggle.Main()";
        }

    }
}
