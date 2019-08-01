using System;
using System.Collections.Generic;
using System.Text;
using TimeMath.Models;

namespace TimeMath.Services
{
    public static class Calculator
    {
        public static DateTime Add(DateTime dateTime1, DateTime dateTime2)
        {
            dateTime1 = dateTime1.AddYears(dateTime2.Year);
            dateTime1 = dateTime1.AddMonths(dateTime2.Month);
            dateTime1 = dateTime1.AddDays(dateTime2.Day);
            dateTime1 = dateTime1.AddHours(dateTime2.Hour);
            dateTime1 = dateTime1.AddMinutes(dateTime2.Minute);
            dateTime1 = dateTime1.AddSeconds(dateTime2.Second);
            dateTime1 = dateTime1.AddMilliseconds(dateTime2.Millisecond);
            return dateTime1;
        }

        public static DateTime Add(DateTime dateTime1, DateTimeSpan dateTimeSpan)
        {
            return DateTimeSpan.Add(dateTime1, dateTimeSpan);
        }
        
        public static DateTimeSpan Subtract(DateTime dateTime1, DateTime dateTime2)
        {
            if (dateTime1 == dateTime2) return new DateTimeSpan();
            if (dateTime1 < dateTime2) return Subtract(dateTime2, dateTime1);

            DateTimeSpan dateTimeSpan = new DateTimeSpan();
            TimeSpan timeDifferece = dateTime1 - dateTime2;

            dateTimeSpan.Hours = timeDifferece.Hours;
            dateTimeSpan.Minutes = timeDifferece.Minutes;
            dateTimeSpan.Seconds = timeDifferece.Seconds;

            // years, months, days
            // example Jan 10 2000 - April 12 1999
            int dayDifference = dateTime1.Day - dateTime2.Day;
            int monthDifference = dateTime1.Month - dateTime2.Month;
            int yearDifference = dateTime1.Year - dateTime2.Year;

            if (monthDifference < 0)
            {
                yearDifference--;
                monthDifference = 12 + monthDifference;
            }

            if (dayDifference < 0)
            {
                monthDifference--;
                dayDifference = DateTime.DaysInMonth(dateTime2.Year, dateTime2.Month + monthDifference) + dayDifference;
            }

            dateTimeSpan.Years = yearDifference;
            dateTimeSpan.Months = monthDifference;
            dateTimeSpan.Days = dayDifference;

            return dateTimeSpan;
        }

        public static DateTime Subtract(DateTime dateTime1, DateTimeSpan dateTimeSpan)
        {
            return DateTimeSpan.Subtract(dateTime1, dateTimeSpan);
        }
        
        /*
        public static string MeaningfulAnswer(DateTime dateTime1, char operand, DateTime dateTime2)
        {

        }*/

    }
}
