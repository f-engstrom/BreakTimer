using System;

namespace BreakTimer
{
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
}