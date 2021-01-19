using System;
using System.Collections.Generic;
using System.Threading;

namespace BreakTimer
{
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
            myMenuArt.ArtPrinter();

            Console.SetCursorPosition(5, 13);
            Console.WriteLine("What version are you using today?");
            Console.SetCursorPosition(5, 15);
            Console.WriteLine("1. School break tracker ");
            Console.SetCursorPosition(5, 17);
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

                mySchoolday = new SchoolDayTime(studyDayStart);

            }


        }
        #endregion

        #region menu



        public void Menu()
        {

            Console.Clear();
            myMenuArt.ArtPrinter();

            do
            {
               


                if (breakTaken)
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
                    Console.WriteLine("<Space to take a break>");
                    Console.WriteLine("<Exit with Escape>");
                    PrintBreaks();

                }
                else
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
                    Console.WriteLine("<Space to take a break>");
                    Console.WriteLine("<Exit with Escape>");
                    Console.WriteLine("\n\n\n\nNo breaks to display");
                }

                if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    StartStopBreak();

                }


               


            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

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
            Console.SetCursorPosition(5, 7);
            Console.WriteLine("On Break");
            Console.SetCursorPosition(5, 11);
            Console.WriteLine("Press space bar to end break");
            Thread.Sleep(1000);




            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }



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

            //if( isNullOrEmpty(myBreaks))


            foreach (var aBreak in myBreaks)
            {
                Console.WriteLine(" ".PadLeft(78, '-'));
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



        public double CalculateStudyBreakRatio(TimeSpan totalBreakTime)
            // calculates how many percent of the day is spent on breaks
        {

            double studyBreakRatio = totalBreakTime.TotalMinutes / dayDuration.TotalMinutes;

            double studyBreakRatioPercent = studyBreakRatio * 100;


            return Math.Round(studyBreakRatioPercent, 1);
        }



    }
}