using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace TwilightToggle
{
    public class TwilightToggle
    {
        public String Main(int[] passedUserAMTimes, int[] passedUserPMTimes)
        {
            Console.WriteLine("Compiled passed user AM Time: " + passedUserAMTimes[0] + ":" + passedUserAMTimes[1] + ".");
            Console.WriteLine("Compiled passed user PM Time: " + passedUserPMTimes[0] + ":" + passedUserPMTimes[1] + ".");
            ChromeHelper chromeHelper = new ChromeHelper();
            FileHelper fileHelper = new FileHelper();
            bool chromeRunState;
            int sleepyTime = 2000;
            string userName = "";
            string localStateLocation = "";
            string readLocalState = "";
            string localStateLocator = "enable-force-dark@";

            string darkModeFlag = "3";
            string lightModeFlag = "9";

            string readUserDayTime = "";
            string readUserNightTime = "";

            while (true)
            {
                try
                {
                    //gets the current Windows user
                    userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1]; ;

                    localStateLocation = "C:/Users/" + userName + "/AppData/Local/Google/Chrome/User Data/Local State";
                    Console.WriteLine("Current user Local State directory: " + localStateLocation);

                    readLocalState = fileHelper.readFileToString(localStateLocation);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error!!! Couldn't get local user's chrome directory. :(");
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("Time to go to sleep for 5 seconds...");
                    Thread.Sleep(sleepyTime);
                }

                DateTime currentDateTime = DateTime.Now;
                string currentDateTimeString = currentDateTime.ToString();
                int currentDateTimeHourInt = currentDateTime.Hour;
                int currentDateTimeMinuteInt = currentDateTime.Minute;
                //some date time logic if statement here to nest everything below into..
                Console.WriteLine("Current hour integer is: " + currentDateTimeHourInt);
                Console.WriteLine("Current minute integer is: " + currentDateTimeMinuteInt);
                Console.WriteLine();

                //output the current user System DateTime into Console
                Console.Write("The current time is: ");
                Console.WriteLine(currentDateTimeString);

                //main variable
                chromeRunState = chromeHelper.GetChromeRunState();
                if (chromeRunState)
                {
                    Console.WriteLine("Chrome is currently running at " + currentDateTimeString + "\n");
                    readLocalState = fileHelper.readFileToString(localStateLocation);
                    //read LastState.config here
                    //-logic to see if its time for the user to close chrome so TT can write
                    //popup reminding user to close chrome
                }
                
                else if (!chromeRunState)
                {
                    try
                    {
                        //if it is daytime logic
                        if (passedUserAMTimes[0] <= currentDateTimeHourInt & currentDateTimeHourInt <= passedUserPMTimes[0])
                        {
                            Console.WriteLine("Daytime detected! Writing disabled flag.");
                            // TODO: see if user is already in daytime mode here
                            if (readLocalState.Contains(localStateLocator))
                            {
                                Console.WriteLine("Attempting write to user's Local State file...");
                                //find substring "enable-force-dark", jump x amount of chars to number flag, create two substrings, 0-->firstCharOfFlag        then    after_flag-->endOfString .     Finally, do Substring1 + desiredFlag + Substring2 and write to Local State

                                int identifierPosition = readLocalState.LastIndexOf(localStateLocator);
                                //Console.WriteLine("Position: " + readLocalState.LastIndexOf("enable-force-dark@"));

                                string writeLocalStatePartOne = (readLocalState.Substring(0, identifierPosition + 18));
                                //Console.WriteLine(writeLocalStatePartOne);

                                string writeLocalStatePartTwo = readLocalState.Substring(writeLocalStatePartOne.Length + 1, readLocalState.Length - (writeLocalStatePartOne.Length + 1));
                                //Console.WriteLine(writeLocalStatePartTwo);

                                string writeLocalStateFinal = writeLocalStatePartOne + lightModeFlag + writeLocalStatePartTwo;

                                //write to user's Local State
                                using (StreamWriter outputFile = new StreamWriter(localStateLocation))
                                {
                                    outputFile.WriteLine(writeLocalStateFinal);
                                    Console.WriteLine("Write succeeded! Wrote light mode flag value of " + lightModeFlag + ".");
                                }

                                //after successful write to file, change sleepyTime so the thread now waits for 45 seconds at the end instead of only 1000ms
                                sleepyTime = 45000;
                                Console.WriteLine("Changed sleepy time to: " + sleepyTime + " milliseconds.");
                            }

                            

                            }
                        else
                        {
                            Console.WriteLine("It's not daytime! Writing darkmode flag.");

                            if (readLocalState.Contains(localStateLocator))
                            {
                                Console.WriteLine("Attempting write to user's Local State file...");
                                //find substring "enable-force-dark", jump x amount of chars to number flag, create two substrings, 0-->firstCharOfFlag        then    after_flag-->endOfString .     Finally, do Substring1 + desiredFlag + Substring2 and write to Local State

                                int identifierPosition = readLocalState.LastIndexOf(localStateLocator);
                                //Console.WriteLine("Position: " + readLocalState.LastIndexOf("enable-force-dark@"));

                                string writeLocalStatePartOne = (readLocalState.Substring(0, identifierPosition + 18));
                                //Console.WriteLine(writeLocalStatePartOne);

                                string writeLocalStatePartTwo = readLocalState.Substring(writeLocalStatePartOne.Length + 1, readLocalState.Length - (writeLocalStatePartOne.Length + 1));
                                //Console.WriteLine(writeLocalStatePartTwo);

                                string writeLocalStateFinal = writeLocalStatePartOne + darkModeFlag + writeLocalStatePartTwo;

                                //write to user's Local State
                                using (StreamWriter outputFile = new StreamWriter(localStateLocation))
                                {
                                    outputFile.WriteLine(writeLocalStateFinal);
                                    Console.WriteLine("Write succeeded! Wrote dark mode flag value of " + darkModeFlag + ".");
                                }

                                //after successful write to file, change sleepyTime so the thread now waits for 45 seconds at the end instead of only 1000ms
                                sleepyTime = 45000;
                                Console.WriteLine("Changed sleepy time to: " + sleepyTime + " milliseconds.");
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("[!] Fatal exception, please report this on GitHub : " + e);
                        Console.WriteLine(e);
                    }
                }
                Console.WriteLine("Reached end of logic. Waiting " + sleepyTime + " milliseconds.");
                Console.WriteLine("-----------------------------------------------------");
                Thread.Sleep(sleepyTime);
            }
            
            return "reached the end of TwilightToggle.Main()";
        }

    }
}
