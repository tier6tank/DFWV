namespace DFWV.WorldClasses
{

    /// <summary>
    /// This class represents a unit of time in a DF World.
    /// </summary>
    public class WorldTime 
    {
        public int Year { get; }
        public int TotalSeconds { get; }

        // The last year in the World
        public static WorldTime Present;

        public WorldTime(int year, int seconds = 0)
        {
            Year = year;
            TotalSeconds = seconds == -1 ? 0 : seconds;
        }

        public WorldTime(int year, int month, int days)
        {
            const int secPerDay = 1200;
            const int dayPerMonth = 28;
            Year = year;
            TotalSeconds = (month * dayPerMonth + days) * secPerDay;
        }

        public WorldTime(int year, int? seconds)
        {

            Year = year;
            if (!seconds.HasValue || seconds.Value == -1)
                TotalSeconds = 0;
            else
                TotalSeconds = seconds.Value;
        }

        public override bool Equals(object obj)
        {
            var t = obj as WorldTime;
            if (t == null)
                return false;

            return (Year == t.Year) && (TotalSeconds == t.TotalSeconds);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash*7) + Year.GetHashCode();
            hash = (hash*7) + TotalSeconds.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Display the time in number format, on standard display pages
        /// </summary>
        public override string ToString()
        {
            const int secPerDay = 1200;
            const int dayPerMonth = 28;
            var month = TotalSeconds / secPerDay / dayPerMonth;
            var day = (TotalSeconds - (month * secPerDay * dayPerMonth)) / secPerDay;
            if (Year == -1)
                return "Before Time";
            return (day + 1) + "." + (month + 1) + "." + Year;
        }


        /// <summary>
        /// Display the time in reverse number format, on the timeline.
        /// </summary>
        public string ToStringRev()
        {
            const int secPerDay = 1200;
            const int dayPerMonth = 28;
            var month = TotalSeconds / secPerDay / dayPerMonth;
            var day = (TotalSeconds - (month * secPerDay * dayPerMonth)) / secPerDay;
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
            const int secPerDay = 1200;
            const int dayPerMonth = 28;
            const int monthPerYear = 12;
            const int secPerYear = secPerDay * dayPerMonth * monthPerYear;


            var years = (int)(seconds / secPerYear);
            var secs = (int)(seconds - (years * secPerYear));
            return new WorldTime(years, secs);
        }

        /// <summary>
        /// Given this WorldTime, return total seconds.  Used when comparing times.
        /// </summary>
        public long ToSeconds()
        {
            const long secPerDay = 1200;
            const long dayPerMonth = 28;
            const long monthPerYear = 12;
            const long secPerYear = secPerDay * dayPerMonth * monthPerYear;

            return Year * secPerYear + TotalSeconds;

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
        internal static string Duration(WorldTime endTime, WorldTime startTime)
        {
            const int secPerDay = 1200;
            const int dayPerMonth = 28;
            const int monthPerYear = 12;
            long endtotalseconds = endTime.Year * monthPerYear * dayPerMonth * secPerDay + endTime.TotalSeconds;
            long starttotalseconds = startTime.Year * monthPerYear * dayPerMonth * secPerDay + startTime.TotalSeconds;
            var secondsdiff = endtotalseconds - starttotalseconds;
            var years = secondsdiff / secPerDay / dayPerMonth / monthPerYear;
            secondsdiff -= (years * secPerDay * dayPerMonth * monthPerYear);
            var months = secondsdiff / secPerDay / dayPerMonth;
            secondsdiff -= (months * secPerDay * dayPerMonth);
            var days = secondsdiff / secPerDay;

            var ticksleft = (int)(secondsdiff - (days * secPerDay));
            var timeDuration = "";
            if (ticksleft != 0) // Double val != 0 causes issues with double conversion, as ticksleft might be very close to but not equal to zero.
            {
                const int hoursPerDay = 24;
                const int minutesPerHour = 60;
                const int realSecsPerMinute = 60;

                var hours = (int)((double)ticksleft / (secPerDay / hoursPerDay));
                ticksleft -= (hours * (secPerDay / hoursPerDay));

                var minutes = (int)(ticksleft / ((float)secPerDay / hoursPerDay / minutesPerHour));
                ticksleft -= (int)(minutes * ((float)secPerDay / hoursPerDay / minutesPerHour));

                var secs = (int)(ticksleft / ((float)secPerDay / hoursPerDay / minutesPerHour / realSecsPerMinute));
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
            const int secPerDay = 1200;
            const int dayPerMonth = 28;
            long months = (TotalSeconds-1) / secPerDay / dayPerMonth;

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