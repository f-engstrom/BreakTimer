using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace BreakTimer
{
    class Program
    {
        static void Main(string[] args)
        {

            
            BreakStudyTimer myBreakStudyTimer = new BreakStudyTimer();
            myBreakStudyTimer.VersionChooser();
            myBreakStudyTimer.Menu();

           

        }

       

        
    }




    #region studytimedurationclass

    


    class SchoolDayTime
    //class that contains information about start end end of the school day
    // and calculates duration based on the start and end time
    {
        public  string StartTime { get; }
        public  string EndTime { get; }

        public TimeSpan Duration { get; internal set; }

        
        public SchoolDayTime(string startTime, string endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            DurationCalculator();
        }

        public SchoolDayTime(string startTime)
        {
            StartTime = startTime;


        }

        private void DurationCalculator()
        { 
         Duration = DateTime.Parse(EndTime).Subtract(DateTime.Parse(StartTime));

        }


    }

    #endregion


    #region breakclass



    class ABreak
        //class that contains the information for a break
        // and that calculates and stores the duration based on that

    {
        public string BreakStartTime;
        public string BreakEndTime;
        public TimeSpan BreakDuration;

        public ABreak(string breakStartTime, string breakEndTime)
        {
            BreakStartTime = breakStartTime;
            BreakEndTime = breakEndTime;
            DurationCalculator();

        }

        private void DurationCalculator()
        {
            BreakDuration = DateTime.Parse(BreakEndTime).Subtract(DateTime.Parse(BreakStartTime));

        }

        public override string ToString()
        {
            return "|Break start time: " + BreakStartTime + "| Break end time: " + BreakEndTime + "| Duration: " + BreakDuration + " |";
        }
    }
    #endregion


    #region menuartclass



    class MenuArt
    //Class to hold menu art
    {
        public string[] art = new[]
        {
            @"###                                                                              #####  ",
            @" #   ####     # #####  #####  #####  ######   ##   #    #    #   # ###### ##### #     # ",
            @" #  #         #   #    #    # #    # #       #  #  #   #      # #  #        #         # ",
            @" #   ####     #   #    #####  #    # #####  #    # ####        #   #####    #      ###  ",
            @" #       #    #   #    #    # #####  #      ###### #  #        #   #        #      #    ",
            @" #  #    #    #   #    #    # #   #  #      #    # #   #       #   #        #           ",
            @"###  ####     #   #    #####  #    # ###### #    # #    #      #   ######   #      #    ",
        };

        public void ArtPrinter()
        {
            Console.WindowWidth = 100;
            Console.WriteLine("\n\n");
            foreach (var line in art)
            {
                Console.WriteLine(line);
            }
        }
    }

    #endregion

    #region breakstudytimerclass



    class BreakStudyTimer
    {
        //Struct list to keep track of break times and duration

        private List<ABreak> myBreaks = new List<ABreak>();
        private TimeSpan dayDuration;
        private bool breakTaken = false;
        MenuArt myMenuArt = new MenuArt();
        private SchoolDayTime mySchoolday;
        private bool schoolBreakTracker = false;


        #region versionchoice

        

        
        public void VersionChooser()
        {
            Console.SetCursorPosition(5, 5);
            Console.WriteLine("What version are you using today?");
            Console.SetCursorPosition(5, 7);
            Console.WriteLine("1. School break tracker ");
            Console.SetCursorPosition(5, 9);
            Console.WriteLine("2. Study break tracker ");

            ConsoleKeyInfo keyPressed;
            bool invalidKeyPress = true;


            do
            {
                keyPressed = Console.ReadKey(true);

                invalidKeyPress = !(keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.D2);

            } while (invalidKeyPress);

            if (keyPressed.Key == ConsoleKey.D1)
            {
                
                //standard values for duration of day to be set if user does not change them
                string startTime = "08:30";
                string endTime = "15:00";

                //Gives user the option to change duration of the "school day"
                Console.Clear();
                Console.SetCursorPosition(5, 7);
                Console.WriteLine("Current School day duration: 08.30 - 15:00\n");
                Console.SetCursorPosition(5, 9);
                Console.WriteLine(@"Do you want to change it? Y\N ?");



                do
                {
                    keyPressed = Console.ReadKey(true);

                    invalidKeyPress = !(keyPressed.Key == ConsoleKey.Y || keyPressed.Key == ConsoleKey.N);

                } while (invalidKeyPress);

                Console.Clear();

                if (keyPressed.KeyChar == 'y')
                {
                    Console.SetCursorPosition(5, 7);
                    Console.WriteLine("Start of day: ");
                    Console.SetCursorPosition(5, 9);
                    Console.WriteLine("End of day: ");
                    Console.SetCursorPosition(20, 7);
                    startTime = Console.ReadLine();
                    Console.SetCursorPosition(20, 9);
                    endTime = Console.ReadLine();
                    Console.Clear();
                    Console.SetCursorPosition(5, 7);
                    Console.WriteLine("Time Successfully changed");
                    Thread.Sleep(1000);

                }
                else if (keyPressed.KeyChar == 'n')
                {
                    Console.SetCursorPosition(5, 7);
                    Console.WriteLine("Time Accepted");
                    Thread.Sleep(1000);
                }

                mySchoolday = new SchoolDayTime(startTime, endTime);
                dayDuration = mySchoolday.Duration;
                schoolBreakTracker = true;
            }
            else if (keyPressed.Key == ConsoleKey.D2)
            {
                string studyDayStart = DateTime.Now.ToString("HH:mm");

                mySchoolday= new SchoolDayTime(studyDayStart);

            }
            

        }
        #endregion

        #region menu

        

        public void Menu()
        {
            
            int choice;

            do
            {
                Console.Clear();
              
                
                myMenuArt.ArtPrinter();

                if (schoolBreakTracker)
                {
                    Console.WriteLine($"\nSchool day duration: {mySchoolday.StartTime} - {mySchoolday.EndTime} Duration time: {mySchoolday.Duration}\n");

                }
                else
                {
                    
                    dayDuration = DateTime.Now.Subtract(DateTime.Parse(mySchoolday.StartTime));

                    Console.WriteLine($"\n Started studying: {mySchoolday.StartTime} Duration: {dayDuration.ToString(@"hh\:mm\:ss")}\n");

                }


                Console.WriteLine("1. Start break");
                Console.WriteLine("2. List breaks");

                Console.WriteLine("3. Exit");


                ConsoleKeyInfo userInput = Console.ReadKey(true);


                if (char.IsDigit(userInput.KeyChar))
                {
                    choice = int.Parse(userInput.KeyChar.ToString());

                }
                else
                {
                    choice = -1;
                }

                switch (choice)

                {
                    case 1:
                        StartStopBreak();
                        break;

                    case 2:
                        if (breakTaken)
                        {
                            Console.Clear();
                            myMenuArt.ArtPrinter();
                            PrintBreaks();

                        }
                        else
                        {
                            Console.Clear();
                            myMenuArt.ArtPrinter();
                            Console.WriteLine("\n\n\n\nTake a break First");
                            Thread.Sleep(1000);
                        }
                        break;


                }

            } while (choice != 3);

        }
        #endregion

        #region startstopbreakmethod

        

        
        public void StartStopBreak()
        {
            //Start and stop break
            // method that starts and stops a break 
            // and stores the start and end time in a breaktime class when done
            // and intialises a break timer that prints to the console
             string breakStartTime;
             string breakEndTime;
             breakTaken = true;
             breakStartTime = DateTime.Now.ToString("HH:mm:ss");

             TimerInfo myTimerInfo = new TimerInfo();
             myTimerInfo.resetTimer();
             TimerCallback callback = new TimerCallback(myTimerInfo.Tick);
             Timer stateTimer = new Timer(callback, null, 0, 1000);


                    Console.Clear();
                    Console.SetCursorPosition(5,7);    
                    Console.WriteLine("On Break");
                    Console.SetCursorPosition(5, 11);
                    Console.WriteLine("Press space bar to end break");
                    Thread.Sleep(1000);




                    while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) {}
                

            
            breakEndTime = DateTime.Now.ToString("HH:mm:ss");
            myBreaks.Add(new ABreak(breakStartTime, breakEndTime));
            stateTimer.Dispose();
        }
        #endregion


        #region printbreaksmethod

        

        
        public void PrintBreaks()
            //Print breaks taken
            // print total break time
            // last break and time since last break
            //break time study time ratio
        {
            int breakNumber = 1;
            TimeSpan totalBreakTime = new TimeSpan();
            int lastBreak = myBreaks.Count - 1;


            foreach (var aBreak in myBreaks)
            {
                Console.WriteLine(" ".PadLeft( 78, '-'));
                Console.WriteLine(breakNumber + "." + " " + aBreak.ToString());
                breakNumber++;
                totalBreakTime += aBreak.BreakDuration;
            }
            Console.WriteLine(" ".PadLeft(78, '-'));


            Console.WriteLine("\nTotal Break Time: " + totalBreakTime);

            TimeSpan timeSinceLastBreak = DateTime.Now.Subtract(DateTime.Parse(myBreaks[lastBreak].BreakEndTime));

            string strTimeSinceLastBreak = timeSinceLastBreak.ToString(@"hh\:mm\:ss");

            Console.WriteLine($"\nLast break at:{myBreaks[lastBreak].BreakEndTime}");
            Console.WriteLine($"\ntime since last break: {strTimeSinceLastBreak}");



            Console.WriteLine("\nPercent of day on break: " + CalculateStudyBreakRatio(totalBreakTime) + "%");

            Console.WriteLine("\nPress any key to return to menu");
            Console.ReadKey();
        }

        #endregion

        #region studybreakratiomethod


        public double CalculateStudyBreakRatio(TimeSpan totalBreakTime)
        // calculates how many percent of the day is spent on breaks
        {
            
            double studyBreakRatio = totalBreakTime.TotalMinutes / dayDuration.TotalMinutes;

            double studyBreakRatioPercent = studyBreakRatio * 100;


            return Math.Round(studyBreakRatioPercent, 1);
        }

      

    }
    #endregion
   
    #endregion


    #region timerclass



    class TimerInfo
    // class that contains timer tick for the timer uptick 
    // and reset timer to make sure the timer calculates from when the user starts it
    {
        public static string BreakTimerStartTime;
        public TimeSpan timeSinceLastBreak;
        public void Tick(Object stateInfo)
        {
            TimeSpan timeSinceLastBreak = DateTime.Now.Subtract(DateTime.Parse(BreakTimerStartTime));

            string strTimeSinceLastBreak = timeSinceLastBreak.ToString(@"hh\:mm\:ss");
            Console.SetCursorPosition(5,9);
            Console.Write("Current Break duration: " + strTimeSinceLastBreak );


        }

        public void MenuTick(Object stateInfo)
        {

            //timeSinceLastBreak = DateTime.Now.Subtract(DateTime.Parse(BreakTimerStartTime));

            
        }


        public void resetTimer()
        {
         BreakTimerStartTime = DateTime.Now.ToString("HH:mm:ss");

        }

      

    }
    #endregion


}
