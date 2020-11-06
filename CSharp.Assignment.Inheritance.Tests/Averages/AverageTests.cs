using CSharp.Assignments.Tests.Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CSharp.Assignment.Inheritance.Averages.Tests
{
    class AverageTests
    {
        [Test]
        public void TestAverageClass()
        {
            var average = new TypeAssert<Average>();
            average.Class();
            average.NonAbstract();
            average.Extends<Aggregator>();
            average.NonPropertyAndField<int[]>("Numbers",
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.GetField |
                BindingFlags.GetProperty);

            average.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int[]>("numbers"))?.Public();

            average.Method<int>(
                "GetValue",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty,
                new Param<int>("n")
            )?.Override();
        }

        [Test]
        public void TestAggregateData()
        {
            var average = new TypeAssert<Average>();
            Assert.Catch<ArgumentNullException>(() => average.New(null));
            int[] array = { 1, 4, 2, 7, 3, 7, 2, 2, 6, 3, -3, 2 };
            dynamic o = average.New(array);
            Assert.AreEqual(1, o.GetValue(1));
            Assert.AreEqual(4, o.GetValue(6));
            Assert.AreEqual(3, o.GetValue(30));
            Assert.Catch<DivideByZeroException>(() => o.GetValue(0));
            Assert.Catch<ArgumentOutOfRangeException>(() => o.GetValue(-1));
            Assert.AreEqual("1 4 2 7 3 7 2 2 6 3 -3 2", o.ToString());
        }
    }
}