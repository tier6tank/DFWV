using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFWV.WorldClasses
{

    /// <summary>
    /// This class represents a unit of time in a DF World.
    /// </summary>
    public class WorldTime
    {
        public int Year { get; set; }
        public int TotalSeconds { get; set; }

        // The last year in the World
        public static WorldTime Present;

        public WorldTime(int year) : this(year, 0) { }

        public WorldTime(int year, int seconds)
        {
            Year = year;
            if (seconds == -1)
                TotalSeconds = 0;
            else
                TotalSeconds = seconds;
        }

        public WorldTime(int year, int? seconds)
        {

            Year = year;
            if (!seconds.HasValue || seconds.Value == -1)
                seconds = 0;
            else
                seconds = seconds.Value;
        }


        /// <summary>
        /// Display the time in number format, on standard display pages
        /// </summary>
        public override string ToString()
        {
            const int SecPerDay = 1200;
            const int DayPerMonth = 28;
            int month = TotalSeconds / SecPerDay / DayPerMonth;
            int day = (TotalSeconds - (month * SecPerDay * DayPerMonth)) / SecPerDay;
            if (Year == -1)
                return "Before Time";
            else
                return (day + 1) + "." + (month + 1) + "." + Year;
        }

        /// <summary>
        /// Display the time in reverse number format, on the timeline.
        /// </summary>
        public string ToStringRev()
        {
            const int SecPerDay = 1200;
            const int DayPerMonth = 28;
            int month = TotalSeconds / SecPerDay / DayPerMonth;
            int day = (TotalSeconds - (month * SecPerDay * DayPerMonth)) / SecPerDay;
            if (Year == -1)
                return "Before Time";
            else
                return Year + "." + (month + 1) + "." + (day + 1);
        }

        #region Operator Overloading
        /// <summary>
        /// Given a total amount of seconds, return a WorldTime.  Used when comparing times.
        /// </summary>
        private static WorldTime FromSeconds(long seconds)
        {
            const int SecPerDay = 1200;
            const int DayPerMonth = 28;
            const int MonthPerYear = 12;
            const int SecPerYear = SecPerDay * DayPerMonth * MonthPerYear;


            int years = (int)(seconds / SecPerYear);
            int secs = (int)(seconds - (years * SecPerYear));
            return new WorldTime(years, secs);
        }

        /// <summary>
        /// Given this WorldTime, return total seconds.  Used when comparing times.
        /// </summary>
        public long ToSeconds()
        {
            const long SecPerDay = 1200;
            const long DayPerMonth = 28;
            const long MonthPerYear = 12;
            const long SecPerYear = SecPerDay * DayPerMonth * MonthPerYear;

            return Year * SecPerYear + TotalSeconds;

        }

        public static WorldTime operator -(WorldTime time1, WorldTime time2)
        {

            long secdiff = time1.ToSeconds() - time2.ToSeconds();

            return WorldTime.FromSeconds(secdiff);
        }

        public static WorldTime operator +(WorldTime time1, WorldTime time2)
        {
            long secSum = time1.ToSeconds() + time2.ToSeconds();

            return WorldTime.FromSeconds(secSum);
        }

        public static WorldTime operator /(WorldTime time, int count)
        {
            long secSum = time.ToSeconds() / count;

            return WorldTime.FromSeconds(secSum);
        }

        public static bool operator <=(WorldTime time1, WorldTime time2)
        {
            return !(time1 > time2);
        }

        public static bool operator <(WorldTime time1, WorldTime time2)
        {
            return time1.Year < time2.Year ||
                    (time1.Year == time2.Year && time1.TotalSeconds < time2.TotalSeconds);
        }

        public static bool operator >=(WorldTime time1, WorldTime time2)
        {
            return !(time1 < time2);
        }

        public static bool operator >(WorldTime time1, WorldTime time2)
        {
            return time1.Year > time2.Year ||
                    (time1.Year == time2.Year && time1.TotalSeconds > time2.TotalSeconds);
        }

        #endregion


        /// <summary>
        /// Get duration between two times as a string (for displaying length of battles/wars)
        /// </summary>
        internal static string Duration(WorldTime EndTime, WorldTime StartTime)
        {
            const int SecPerDay = 1200;
            const int DayPerMonth = 28;
            const int MonthPerYear = 12;
            long Endtotalseconds = EndTime.Year * MonthPerYear * DayPerMonth * SecPerDay + EndTime.TotalSeconds;
            long Starttotalseconds = StartTime.Year * MonthPerYear * DayPerMonth * SecPerDay + StartTime.TotalSeconds;
            long secondsdiff = Endtotalseconds - Starttotalseconds;
            long years = secondsdiff / SecPerDay / DayPerMonth / MonthPerYear;
            secondsdiff -= (years * SecPerDay * DayPerMonth * MonthPerYear);
            long months = secondsdiff / SecPerDay / DayPerMonth;
            secondsdiff -= (months * SecPerDay * DayPerMonth);
            long days = secondsdiff / SecPerDay;
            if (years > 0 && months > 0 && days > 0)
            {
                return years + " years, " + months + " months, " + days + " days";
            }
            else if (years > 0 && months > 0 && days == 0)
            {
                return years + " years, " + months + " months";
            }
            else if (years > 0 && months == 0 && days > 0)
            {
                return years + " years, " + days + " days";
            }
            else if (years > 0 && months == 0 && days == 0)
            {
                return years + " years";
            }
            else if (years == 0 && months > 0 && days > 0)
            {
                return months + " months, " + days + " days";
            }
            else if (years == 0 && months > 0 && days == 0)
            {
                return months + " months";
            }
            else if (years == 0 && months == 0 && days > 0)
            {
                return days + " days";
            }
            else if (years == 0 && months == 0 && days == 0)
            {
                return "";
            }
            else
                return "";
        }

        /// <summary>
        /// Display time in the same manner as the Legends mode
        /// </summary>
        internal string LegendsTime()
        {
            const int SecPerDay = 1200;
            const int DayPerMonth = 28;
            long months = (TotalSeconds-1) / SecPerDay / DayPerMonth;

            if (Year == -1)
                return "In a time before time";
            else if (months == 0 && TotalSeconds == 0)
                return Year.ToString();
            else if (months <= 0)
                return "the early spring of " + Year.ToString();
            else if (months == 1)
                return "the midspring of " + Year.ToString();
            else if (months == 2)
                return "the late spring of " + Year.ToString();
            else if (months == 3)
                return "the early summer of " + Year.ToString();
            else if (months == 4)
                return "the midssummer of " + Year.ToString();
            else if (months == 5)
                return "the late summer of " + Year.ToString();
            else if (months == 6)
                return "the early autumn of " + Year.ToString();
            else if (months == 7)
                return "the midautumn of " + Year.ToString();
            else if (months == 8)
                return "the late autumn of " + Year.ToString();
            else if (months == 9)
                return "the early winter of " + Year.ToString();
            else if (months == 10)
                return "the midwinter of " + Year.ToString();
            else if (months == 11)
                return "the late spring of " + Year.ToString();
            else
                return Year.ToString();
        }


    }
}
