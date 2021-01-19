using System;

namespace BreakTimer
{
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
            Console.SetCursorPosition(5, 9);
            Console.Write("Current Break duration: " + strTimeSinceLastBreak);


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
}