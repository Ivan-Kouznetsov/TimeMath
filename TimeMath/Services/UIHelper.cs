using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TimeMath.Models;
using TimeMath.Services;

namespace TimeMath.Services
{
    public static class UIHelper
    {
        public static string ApplyAliases(string s)
        {
            return s.Replace("now", DateTime.Now.ToString())
                    .Replace("today", DateTime.Now.ToShortDateString())
                    .Replace("minus", "-")
                    .Replace("plus", "+")
                    .Replace("yesterday", DateTime.Now.AddDays(-1).ToShortDateString())
                    .Replace("tomorrow", DateTime.Now.AddDays(1).ToShortDateString());
        }

        public static string ReplaceDashyDates(string s)
        {
            Regex dashyDates = new Regex(@"\d{1,4}-\d{1,2}-\d{1,4}");
            var matches = dashyDates.Matches(s);

            foreach (Match m in matches)
            {
                s = s.Replace(m.Value, m.Value.Replace("-", "/"));
            }
            return s;
        }

        public static string CornerCases(string s)
        {
            return s.Replace("sept ", "sep ");
        }

        public static string ApplyAllImprovemnets(string s)
        {
            s = s.ToLower();
            s = ApplyAliases(s);
            s = ReplaceDashyDates(s);
            s = CornerCases(s);
            return s;
        }

        public static string ExplainSyntaxError(string expression)
        {
            string[] parts = expression.Split(new char[] { '+', '-' });

            if (parts.Length<2) return Properties.Resources.SyntaxErrorNoOperand;
            if (parts.Length>2) return Properties.Resources.SyntaxErrorTooManyOperands;
            if (!DateTime.TryParse(parts[0], out _)) return string.Format(Properties.Resources.SyntaxErrorPart1NotValid,parts[0]);
            if (!DateTime.TryParse(parts[1], out _) && !DateTimeSpan.TryParse(parts[1], out _)) return string.Format(Properties.Resources.SyntaxErrorPart2NotValid,parts[1]);
            return Properties.Resources.Usage;
        }
    }
}
