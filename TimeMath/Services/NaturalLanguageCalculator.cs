using System;
using System.Collections.Generic;
using System.Text;
using TimeMath.Models;
using TimeMath.Services;

namespace TimeMath.Services
{
   public static class NaturalLanguageCalculator
    {
        /// <summary>
        /// Computes a natural language date/time addition or subtraction expression such as Jan 1, 2000 + 5 days, returns the result of this expression. Returns null if input is not valid.
        /// </summary>
        /// <param name="expression">A natural language date/time addition or subtraction expression. Example: "Jan 1, 2000 + 5 days"</param>
        /// <returns></returns>
        public static string Calculate(string expression)
        {
            string result = null;
            const char Plus = '+';
            const char Minus = '-';

            string[] parts = expression.Split(new char[] { Plus, Minus });
            if (parts.Length != 2) return null;

            char[] operandInput = expression.Replace(parts[0], string.Empty).Replace(parts[1], string.Empty).Trim().ToCharArray();
            if (operandInput.Length!=1) return null;

            char operand = operandInput[0];
            if (operand != Plus && operand != Minus) return null;

            if (!DateTime.TryParse(parts[0], out DateTime dateTime1)) return null;

            if (DateTime.TryParse(parts[1], out DateTime dateTime2))
            {
                switch (operand)
                {
                    case Plus: result = PrettifyDateTime(Calculator.Add(dateTime1, dateTime2)); break;
                    case Minus: result = Calculator.Subtract(dateTime1, dateTime2).ToString(); break;
                }
            }
            else if (DateTimeSpan.TryParse(parts[1], out DateTimeSpan dateTimeSpan))
            {
                switch (operand)
                {
                    case Plus: result = PrettifyDateTime(Calculator.Add(dateTime1, dateTimeSpan)); break;
                    case Minus: result = PrettifyDateTime(Calculator.Subtract(dateTime1, dateTimeSpan)); break;
                }
            }
            return result;
        }

        private static string PrettifyDateTime(DateTime dateTime)
        {
            if (dateTime.Hour == 0 && dateTime.Minute == 0 && dateTime.Second == 0) return dateTime.ToLongDateString();
            return dateTime.ToLongDateString() + " " + dateTime.ToLongTimeString();
        }
    }
}
