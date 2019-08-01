using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TimeMath.Models;
using System.Linq;

namespace TimeMath.Models
{
    public class Token
    {
        public int Number { get; set; }
        public string Word { get; set; }

        public Token(int number, string word)
        {
            Number = number;
            Word = word;
        }
    }

    public class DateTimeSpan
    {
        public int Years { get; set; }
        public int Months { get; set; }
        public int Weeks { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public bool IsEmpty { get { return (Years + Months + Weeks + Days + Hours + Minutes + Seconds) == 0; } }

        private static readonly string[] second = new string[] { "s", "second", "seconds" };
        private static readonly string[] minute = new string[] { "m", "minute", "minutes" };
        private static readonly string[] hour = new string[] { "h", "hour", "hours" };
        private static readonly string[] day = new string[] { "d", "day", "days" };
        private static readonly string[] week = new string[] { "w", "week", "weeks" };
        private static readonly string[] month = new string[] { "month", "months" };
        private static readonly string[] year = new string[] { "y", "year", "years" };

        public DateTimeSpan(int years, int months, int weeks, int days, int hours, int minutes, int seconds)
        {
            Years = years;
            Months = months;
            Weeks = weeks;
            Days = days;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds)
        {
            Years = years;
            Months = months;
            Weeks = 0;
            Days = days;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public DateTimeSpan(int years, int months, int days)
        {
            Years = years;
            Months = months;
            Weeks = 0;
            Days = days;
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public DateTimeSpan()
        {
            Years = 0;
            Months = 0;
            Weeks = 0;
            Days = 0;
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public static DateTime Add(DateTime dateTime, DateTimeSpan dateTimeSpan)
        {
            dateTime = dateTime.AddSeconds(dateTimeSpan.Seconds);
            dateTime = dateTime.AddMinutes(dateTimeSpan.Minutes);
            dateTime = dateTime.AddHours(dateTimeSpan.Hours);
            dateTime = dateTime.AddDays(dateTimeSpan.Days);
            dateTime = dateTime.AddDays(dateTimeSpan.Weeks * 7);
            dateTime = dateTime.AddMonths(dateTimeSpan.Months);
            dateTime = dateTime.AddYears(dateTimeSpan.Years);

            return dateTime;
        }

        public static DateTime Subtract(DateTime dateTime, DateTimeSpan dateTimeSpan)
        {
            dateTime = dateTime.AddSeconds(dateTimeSpan.Seconds * -1);
            dateTime = dateTime.AddMinutes(dateTimeSpan.Minutes * -1);
            dateTime = dateTime.AddHours(dateTimeSpan.Hours * -1);
            dateTime = dateTime.AddDays(dateTimeSpan.Days * -1);
            dateTime = dateTime.AddDays(dateTimeSpan.Weeks * 7 * -1);
            dateTime = dateTime.AddMonths(dateTimeSpan.Months * -1);
            dateTime = dateTime.AddYears(dateTimeSpan.Years * -1);

            return dateTime;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;

            DateTimeSpan dateTimeSpan = (DateTimeSpan)obj;

            return this.Days == dateTimeSpan.Days &&
                   this.Hours == dateTimeSpan.Hours &&
                   this.Minutes == dateTimeSpan.Minutes &&
                   this.Months == dateTimeSpan.Months &&
                   this.Seconds == dateTimeSpan.Seconds &&
                   this.Weeks == dateTimeSpan.Weeks &&
                   this.Years == dateTimeSpan.Years;
        }

        public override string ToString()
        {
            if (IsEmpty) return "0 seconds";

            string s = string.Empty;
            
            s += PropertyToString(Years, "year", true);
            s += PropertyToString(Months, "month", true);
            s += PropertyToString(Weeks, "week", true);
            s += PropertyToString(Days, "day", true);
            s += PropertyToString(Hours, "hour", true);
            s += PropertyToString(Minutes, "minute", true);
            s += PropertyToString(Seconds, "second", false);

            return s.TrimEnd();
        }

        private string PropertyToString(int value, string name, bool addSpace)
        {
            if (value == 0) return string.Empty;
            const string space = " ";

            return value.ToString() + space + name + (value > 1 ? "s" : string.Empty) + (addSpace ? space : string.Empty);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        
        // Parsing 

        public static DateTimeSpan Parse(string s)
        {
            s = s.Trim().ToLower();
            DateTimeSpan dateTimeSpan = new DateTimeSpan();

            Regex tokenRegex = new Regex(@"\d+\s{0,1}\w+");
            Regex numberRegex = new Regex(@"\d{1,}");
            Regex wordRegex = new Regex(@"[a-z]+");

            var matches = tokenRegex.Matches(s);

            List<Token> tokens = new List<Token>();

            for (int i = 0; i < matches.Count; i++)
            {
                tokens.Add(new Token(int.Parse(numberRegex.Match(matches[i].Value).Value),wordRegex.Match(matches[i].Value).Value));
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (second.Contains(tokens[i].Word))
                {
                    dateTimeSpan.Seconds = tokens[i].Number;
                }
                else if (minute.Contains(tokens[i].Word))
                {
                    dateTimeSpan.Minutes = tokens[i].Number;
                }
                else if (hour.Contains(tokens[i].Word))
                {
                    dateTimeSpan.Hours = tokens[i].Number;
                }
                else if (day.Contains(tokens[i].Word))
                {
                    dateTimeSpan.Days = tokens[i].Number;
                }
                else if (week.Contains(tokens[i].Word))
                {
                    dateTimeSpan.Weeks = tokens[i].Number;
                }
                else if (month.Contains(tokens[i].Word))
                {
                    dateTimeSpan.Months = tokens[i].Number;
                }
                else if (year.Contains(tokens[i].Word))
                {
                    dateTimeSpan.Years = tokens[i].Number;
                }
            }

            return dateTimeSpan;
        }

        public static bool TryParse(string s, out DateTimeSpan dateTimeSpan)
        {
            dateTimeSpan = Parse(s);
            return !dateTimeSpan.IsEmpty;
        }
    }
}
