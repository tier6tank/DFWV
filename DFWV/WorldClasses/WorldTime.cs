namespace DFWV.WorldClasses
{

    /// <summary>
    /// This class represents a unit of time in a DF World.
    /// </summary>
    public class WorldTime
    {
        public int Year { get; private set; }
        public int TotalSeconds { get; private set; }

        // The last year in the World
        public static WorldTime Present;

        public WorldTime(int year, int seconds = 0)
        {
            Year = year;
            TotalSeconds = seconds == -1 ? 0 : seconds;
        }

        public WorldTime(int year, int month, int days)
        {
            const int SecPerDay = 1200;
            const int DayPerMonth = 28;
            Year = year;
            TotalSeconds = (month * DayPerMonth + days) * SecPerDay;
        }

        public WorldTime(int year, int? seconds)
        {

            Year = year;
            if (!seconds.HasValue || seconds.Value == -1)
                TotalSeconds = 0;
            else
                TotalSeconds = seconds.Value;
        }


        /// <summary>
        /// Display the time in number format, on standard display pages
        /// </summary>
        public override string ToString()
        {
            const int SecPerDay = 1200;
            const int DayPerMonth = 28;
            var month = TotalSeconds / SecPerDay / DayPerMonth;
            var day = (TotalSeconds - (month * SecPerDay * DayPerMonth)) / SecPerDay;
            if (Year == -1)
                return "Before Time";
            return (day + 1) + "." + (month + 1) + "." + Year;
        }

        /// <summary>
        /// Display the time in reverse number format, on the timeline.
        /// </summary>
        public string ToStringRev()
        {
            const int SecPerDay = 1200;
            const int DayPerMonth = 28;
            var month = TotalSeconds / SecPerDay / DayPerMonth;
            var day = (TotalSeconds - (month * SecPerDay * DayPerMonth)) / SecPerDay;
            if (Year == -1)
                return "Before Time";
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


            var years = (int)(seconds / SecPerYear);
            var secs = (int)(seconds - (years * SecPerYear));
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

            var secdiff = time1.ToSeconds() - time2.ToSeconds();

            return FromSeconds(secdiff);
        }

        public static WorldTime operator +(WorldTime time1, WorldTime time2)
        {
            var secSum = time1.ToSeconds() + time2.ToSeconds();

            return FromSeconds(secSum);
        }

        public static WorldTime operator /(WorldTime time, int count)
        {
            var secSum = time.ToSeconds() / count;

            return FromSeconds(secSum);
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
            var secondsdiff = Endtotalseconds - Starttotalseconds;
            var years = secondsdiff / SecPerDay / DayPerMonth / MonthPerYear;
            secondsdiff -= (years * SecPerDay * DayPerMonth * MonthPerYear);
            var months = secondsdiff / SecPerDay / DayPerMonth;
            secondsdiff -= (months * SecPerDay * DayPerMonth);
            var days = secondsdiff / SecPerDay;

            var ticksleft = (int)(secondsdiff - (days * SecPerDay));
            var timeDuration = "";
            if (ticksleft != 0) // Double val != 0 causes issues with double conversion, as ticksleft might be very close to but not equal to zero.
            {
                const int HoursPerDay = 24;
                const int MinutesPerHour = 60;
                const int RealSecsPerMinute = 60;

                var hours = (int)((double)ticksleft / (SecPerDay / HoursPerDay));
                ticksleft -= (hours * (SecPerDay / HoursPerDay));

                var minutes = (int)(ticksleft / ((float)SecPerDay / HoursPerDay / MinutesPerHour));
                ticksleft -= (int)(minutes * ((float)SecPerDay / HoursPerDay / MinutesPerHour));

                var secs = (int)(ticksleft / ((float)SecPerDay / HoursPerDay / MinutesPerHour / RealSecsPerMinute));
                if (hours > 0 && minutes > 0 && secs > 0)
                {
                    timeDuration = hours + " h, " + minutes + " m, " + secs + " s";
                }
                else if (hours > 0 && minutes > 0 && secs == 0)
                {
                    timeDuration = hours + " h, " + minutes + " m";
                }
                else if (hours > 0 && minutes == 0 && secs > 0)
                {
                    timeDuration = hours + " h, " + secs + " s";
                }
                else if (hours > 0 && minutes == 0 && secs == 0)
                {
                    timeDuration = hours + " h";
                }
                else if (hours == 0 && minutes > 0 && secs > 0)
                {
                    timeDuration = minutes + " m, " + secs + " s";
                }
                else if (hours == 0 && minutes > 0 && secs == 0)
                {
                    timeDuration = minutes + " m";
                }
                else if (hours == 0 && minutes == 0 && secs > 0)
                {
                    timeDuration = secs + " s";
                }
            }
            timeDuration = " " + timeDuration;
            if (years > 0 && months > 0 && days > 0)
            {
                return years + " years, " + months + " months, " + days + " days" + timeDuration;
            }
            if (years > 0 && months > 0 && days == 0)
            {
                return years + " years, " + months + " months" + timeDuration;
            }
            if (years > 0 && months == 0 && days > 0)
            {
                return years + " years, " + days + " days" + timeDuration;
            }
            if (years > 0 && months == 0 && days == 0)
            {
                return years + " years" + timeDuration;
            }
            if (years == 0 && months > 0 && days > 0)
            {
                return months + " months, " + days + " days" + timeDuration;
            }
            if (years == 0 && months > 0 && days == 0)
            {
                return months + " months" + timeDuration;
            }
            if (years == 0 && months == 0 && days > 0)
            {
                return days + " days" + timeDuration;
            }
            if (years == 0 && months == 0 && days == 0)
            {
                return timeDuration;
            }
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
                return "a time before time";
            if (months == 0 && TotalSeconds == 0)
                return Year.ToString();
            if (months <= 0)
                return "the early spring of " + Year;
            switch (months)
            {
                case 1:
                    return "the midspring of " + Year;
                case 2:
                    return "the late spring of " + Year;
                case 3:
                    return "the early summer of " + Year;
                case 4:
                    return "the midssummer of " + Year;
                case 5:
                    return "the late summer of " + Year;
                case 6:
                    return "the early autumn of " + Year;
                case 7:
                    return "the midautumn of " + Year;
                case 8:
                    return "the late autumn of " + Year;
                case 9:
                    return "the early winter of " + Year;
                case 10:
                    return "the midwinter of " + Year;
                case 11:
                    return "the late spring of " + Year;
            }
            return Year.ToString();
        }


    }
}