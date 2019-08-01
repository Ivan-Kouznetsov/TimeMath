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
                    Console.WriteLine("Jan 1 2000 + 10 days");
                    Console.WriteLine("Jan 1 2000 - Dec 25 1999");
                    Console.WriteLine("2:23 - 1:11");
                }
                else if (input.ToLower() == "help")
                {
                    Console.WriteLine("Usage: DateTime +/- DateTime or DateTime span expression");
                }
                else
                {
                    string response = NaturalLanguageCalculator.Calculate(input);
                    if (response == null)
                    {
                        Console.WriteLine("There is a mistake in the input. For help type 'help'. For examples type 'examples'.");
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
