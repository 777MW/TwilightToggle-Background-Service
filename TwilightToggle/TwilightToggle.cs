using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TwilightToggle
{
    public class TwilightToggle
    {
        public String Main(String passedUserDayTime, String passedUserNightTime)
        {
            ChromeHelper chromeHelper = new ChromeHelper();
            FileHelper fileHelper = new FileHelper();
            bool chromeRunState;

            while (true)
            {
                //[!]PUT THIS ALL IN A IF, IF CURRENTSYSTEM TIME >= passedUserDayTimeORNightTime && Some check to ensure it only runs once a cycle here

                //main variable
                chromeRunState = chromeHelper.GetChromeRunState();
                if (chromeRunState)
                {
                    Console.WriteLine("Chrome is currently running.");
                }

                else
                {
                    try
                    {
                        //gets the current Windows user
                        string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1]; ;

                        string localStateLocation = "C:/Users/" + userName + "/AppData/Local/Google/Chrome/User Data/Local State";
                        Console.WriteLine("Current user Local State directory: " + localStateLocation);

                        String readLocalState = fileHelper.readFileReturnString(localStateLocation);
                        if (readLocalState.Contains("enable-force-dark"))
                        {
                            Console.WriteLine("[o] enable-force-dark setting found. Attempting write to Local State file...");
                            //find substring "enable-force-dark", jump x amount of chars to number flag, create two substrings, 0-->firstCharOfFlag        then    after_flag-->endOfString .     Finally, do Substring1 + desiredFlag + Substring2 and write to Local State
                        }
                        else
                        {
                            Console.WriteLine("[!] FATAL! Could NOT find enable-force-dark flag in " + localStateLocation + " ! Not attempting write to Local State file!!!");
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Fatal exception occurred!");
                        Console.WriteLine("Fatal exception:");
                        Console.WriteLine(e);
                    }
                }

                Thread.Sleep(1000);
            }
            
            return "reached the end of TwilightToggle.Main()";
        }

    }
}
