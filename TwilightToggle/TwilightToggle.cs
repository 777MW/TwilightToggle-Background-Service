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
            int sleepyTime = 1000;
            string userName = "";
            string localStateLocation = "";
            string readLocalState = "";
            string localStateLocator = "enable-force-dark@";

            try
            {
                //gets the current Windows user
                userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1]; ;

                localStateLocation = "C:/Users/" + userName + "/AppData/Local/Google/Chrome/User Data/Local State";
                Console.WriteLine("Current user Local State directory: " + localStateLocation);

                readLocalState = fileHelper.readFileToString(localStateLocation);
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.ToString());
            }

            while (true)
            {
                //[!]PUT THIS ALL IN A IF, IF CURRENTSYSTEM TIME >= passedUserDayTimeORNightTime && Some check to ensure it only runs once a cycle here

                //main variable
                chromeRunState = chromeHelper.GetChromeRunState();
                if (chromeRunState)
                {
                    Console.WriteLine("Chrome is currently running...");
                    DateTime now = DateTime.Now;
                    Console.WriteLine(now);
                    readLocalState = fileHelper.readFileToString(localStateLocation);
                    //read LastState.config here
                    //-logic to see if its time for the user to close chrome so TT can write
                }

                else
                {
                    try
                    {
                        
                        if (readLocalState.Contains("enable-force-dark"))
                        {
                            Console.WriteLine("[o] enable-force-dark found in Local State. Attempting write to user's Local State file...");

                            //find substring "enable-force-dark", jump x amount of chars to number flag, create two substrings, 0-->firstCharOfFlag        then    after_flag-->endOfString .     Finally, do Substring1 + desiredFlag + Substring2 and write to Local State

                            int identifierPosition = readLocalState.LastIndexOf("enable-force-dark@");
                            Console.WriteLine("Position: " + readLocalState.LastIndexOf("enable-force-dark@"));
                            
                            String writeLocalStatePartOne = (readLocalState.Substring(0, identifierPosition + 18));
                            Console.WriteLine(writeLocalStatePartOne);
                            Console.WriteLine(readLocalState.Length);
                            //Console.WriteLine(readLocalState.Substring(identifierPosition + 18, readLocalState.Length - 1));


                            //after successful write to file, change sleepyTime so the thread now reads every 4.5 seconds instead of every 1000ms
                            sleepyTime = 4500;
                        }
                        else
                        {
                            Console.WriteLine("[!] FATAL! Could NOT find enable-force-dark flag in user's Local State : " + localStateLocation + " ! Skipping write to Local State file!!!");
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("[!] Fatal exception, please report this on GitHub : " + e);
                        Console.WriteLine(e);
                    }
                }
                Thread.Sleep(sleepyTime);
            }
            
            return "reached the end of TwilightToggle.Main()";
        }

    }
}
