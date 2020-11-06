using CSharp.Assignments.Tests.Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CSharp.Assignment.Inheritance.Averages.Tests
{
    class CountTests
    {
        [Test]
        public void TestCountClass()
        {
            var count = new TypeAssert<Count>();
            count.Class();
            count.NonAbstract();
            count.Extends<Aggregator>();
            count.NonPropertyAndField<int[]>("Numbers",
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.GetField |
                BindingFlags.GetProperty);

            count.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int[]>("numbers"))?.Public();

            count.Method<int>(
                "GetValue",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty,
                new Param<int>("n")
            )?.Override();
        }

        [Test]
        public void TestCountData()
        {
            var count = new TypeAssert<Count>();
            Assert.Catch<ArgumentNullException>(() => count.New(null));
            int[] array = { 1, 4, 2, 7, 3, 7, 2, 2, 6, 3, -3, 2 };
            dynamic o = count.New(array);
            Assert.AreEqual(0, o.GetValue(0));
            Assert.AreEqual(1, o.GetValue(1));
            Assert.AreEqual(4, o.GetValue(2));
            Assert.AreEqual(2, o.GetValue(3));
            Assert.AreEqual(1, o.GetValue(4));
            Assert.AreEqual(0, o.GetValue(5));
            Assert.AreEqual(1, o.GetValue(6));
            Assert.AreEqual(2, o.GetValue(7));
            Assert.AreEqual(0, o.GetValue(8));
            Assert.AreEqual("1 4 2 7 3 7 2 2 6 3 -3 2", o.ToString());
        }
    }
}