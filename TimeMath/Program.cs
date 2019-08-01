using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TimeMath.Services;

namespace TimeMath
{
    class Program
    {
        static void Main(string[] args)
        {
            const string prompt = "TimeMath>";
            string input;

            Console.WriteLine(Properties.Resources.Introduction);

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (input.Contains("exit"))
                {
                    break;
                }
                else if (input.ToLower() == "examples")
                {
                    Console.WriteLine(Properties.Resources.Examples.Replace(@"\n",Environment.NewLine));                
                }
                else if (input.ToLower() == "help")
                {
                    Console.WriteLine(Properties.Resources.Usage.Replace(@"\n", Environment.NewLine));
                }
                else
                {
                    string improvedInput = UIHelper.ApplyAllImprovemnets(input);
                    string response = NaturalLanguageCalculator.Calculate(improvedInput);
                    if (response == null)
                    {
                        Console.WriteLine(UIHelper.ExplainSyntaxError(improvedInput));
                    }
                    else
                    {
                        Console.WriteLine(response);
                    }
                }
            }
        }
    }
}
