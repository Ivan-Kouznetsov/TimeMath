using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeMath.Models;
using TimeMath.Services;
using System;

namespace TimeMathTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void DateTimeSpanParsing()
        {
            Assert.AreEqual(new DateTimeSpan(1,1,1,1,1,1,1), DateTimeSpan.Parse("1 year 1 month 1 week 1 day 1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0,1,1,1,1,1,1), DateTimeSpan.Parse("1 month 1 week 1 day 1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0,0,1,1,1,1,1), DateTimeSpan.Parse("1 week 1 day 1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0,0,0,1,1,1,1), DateTimeSpan.Parse("1 day 1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0,0,0,0,1,1,1), DateTimeSpan.Parse("1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0,0,0,0,0,1,1), DateTimeSpan.Parse("1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0,0,0,0,0,0,1), DateTimeSpan.Parse("1 second"));

            Assert.AreEqual(new DateTimeSpan(10, 1, 1, 1, 1, 1, 1), DateTimeSpan.Parse("10 year 1 month 1 week 1 day 1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0, 10, 1, 1, 1, 1, 1), DateTimeSpan.Parse("10 month 1 week 1 day 1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 10, 1, 1, 1, 1), DateTimeSpan.Parse("10 week 1 day 1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 0, 10, 1, 1, 1), DateTimeSpan.Parse("10 day 1 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 0, 0, 10, 1, 1), DateTimeSpan.Parse("10 hour 1 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 0, 0, 0, 10, 1), DateTimeSpan.Parse("10 minute 1 second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 0, 0, 0, 0, 10), DateTimeSpan.Parse("10 second"));

            Assert.AreEqual(new DateTimeSpan(1, 1, 1, 1, 1, 1, 1), DateTimeSpan.Parse("1year 1month 1week 1day 1hour 1minute 1second"));
            Assert.AreEqual(new DateTimeSpan(0, 1, 1, 1, 1, 1, 1), DateTimeSpan.Parse("1month 1week 1day 1hour 1minute 1second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 1, 1, 1, 1, 1), DateTimeSpan.Parse("1week 1day 1hour 1minute 1second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 0, 1, 1, 1, 1), DateTimeSpan.Parse("1day 1hour 1minute 1second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 0, 0, 1, 1, 1), DateTimeSpan.Parse("1hour 1minute 1second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 0, 0, 0, 1, 1), DateTimeSpan.Parse("1minute 1second"));
            Assert.AreEqual(new DateTimeSpan(0, 0, 0, 0, 0, 0, 1), DateTimeSpan.Parse("1second"));
        }

        [TestMethod]
        public void AddSpan()
        {
            DateTime dateTime = DateTimeSpan.Add(new DateTime(2000, 10, 10, 10, 10, 10), new DateTimeSpan(1, 1, 1, 1, 1, 1, 1));
            Assert.AreEqual(new DateTime(2001, 11, 18, 11, 11, 11), dateTime);
        }

        [TestMethod]
        public void SubtractSpan()
        {
            DateTime dateTime = DateTimeSpan.Subtract(new DateTime(2000, 10, 10, 10, 10, 10), new DateTimeSpan(1, 1, 0, 1, 1, 1, 1));
            Assert.AreEqual(new DateTime(1999, 9, 9, 9, 9, 9), dateTime);
        }

        [TestMethod]
        public void AddDates()
        {
            Assert.AreEqual(new DateTime(4000, 10, 20), Calculator.Add(new DateTime(2000, 5, 10), new DateTime(2000, 5, 10)));
            Assert.AreEqual(new DateTime(4001, 3, 20), Calculator.Add(new DateTime(2000, 5, 10), new DateTime(2000, 10, 10)));
        }

        [TestMethod]
        public void AddDateAndSpan()
        {
            Assert.AreEqual(new DateTime(2001,10,20), Calculator.Add(new DateTime(2000, 5, 10),new DateTimeSpan(1,5,0,10,0,0,0)));
        }

        [TestMethod]
        public void SubtractDates()
        {
            Assert.AreEqual(new DateTimeSpan(0,8,29), Calculator.Subtract(new DateTime(2000,1,10), new DateTime(1999,4,12)));
        }

        [TestMethod]
        public void SubtractSpanFromDate()
        {
            Assert.AreEqual(new DateTime(1999,04,12), Calculator.Subtract(new DateTime(2000,1,10), new DateTimeSpan(0,8,29)));
        }

        [TestMethod]
        public void NaturalLanguageCalculation()
        {
            Assert.AreEqual("January 6, 2000", NaturalLanguageCalculator.Calculate("Jan 1 2000 + 5 days"));
            Assert.AreEqual("February 20, 2000", NaturalLanguageCalculator.Calculate("Jan 1 2000 + 50 days"));
        }
    }
}
