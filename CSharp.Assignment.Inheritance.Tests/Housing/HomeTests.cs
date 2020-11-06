using System;
using System.Reflection;
using CSharp.Assignments.Classes.Housing;
using CSharp.Assignments.Tests.Library;
using NUnit.Framework;
using static CSharp.Assignments.Tests.Library.TypeAssert;
using System.Text;
using System.Linq;

namespace CSharp.Assignment.Inheritance.Housing.Tests
{
    public class HomeTests
    {
        [Test]
        public void TestHomeClass()
        {
            var assert = new TypeAssert<Home>();

            assert.Constructor(
                BindingFlags.NonPublic |
                BindingFlags.Instance,
                new Param<string>("address"),
                new Param<int>("yearBuilt"),
                new Param<decimal>("price")
            ).Protected();

            assert.NonConstructor(
                BindingFlags.Public |
                BindingFlags.Instance
            );

            assert.Property<string>(
              "Address",
              BindingFlags.Instance |
              BindingFlags.Public |
              BindingFlags.GetProperty);

            assert.Property<int>(
                "YearBuilt",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            );

            assert.Property<decimal>(
                "Price",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            );

            assert.Property<decimal>(
                "TotalCost",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            ).Virtual();

            assert.Method<decimal>(
                "GetRate",
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int>("numberOfPeriods")
            ).Virtual();

            assert.Method<string>(
                "ToString",
                BindingFlags.Public |
                BindingFlags.Instance
            ).Override();
        }
    }
}
