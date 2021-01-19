using System;

namespace BreakTimer
{
    class SchoolDayTime
        //class that contains information about start end end of the school day
        // and calculates duration based on the start and end time
    {
        public string StartTime { get; }
        public string EndTime { get; }

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
}