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
    public class SingleFamilyTests
    {
        [Test]
        [Category("Home Class")]
        public void TestSingleFamily()
        {
            var assert = new TypeAssert<SingleFamily>();
            assert.Extends<Home>();

            assert.Property<string>(
               "Address",
               BindingFlags.Instance |
               BindingFlags.Public |
               BindingFlags.GetProperty
            ).DeclaredIn<Home>();

            assert.Property<int>(
                "YearBuilt",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            ).DeclaredIn<Home>();

            assert.Property<decimal>(
                "Price",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            ).DeclaredIn<Home>();

            assert.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<string>("address"),
                new Param<int>("yearBuilt"),
                new Param<decimal>("price")
            );

            assert.NonConstructor(
                BindingFlags.Public |
                BindingFlags.Instance
            );

            assert.Property<decimal>(
                "TotalCost",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            ).DeclaredIn<Home>().Override();

            assert.Method<decimal>(
                "GetRate",
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int>("numberOfPeriods")
            ).DeclaredIn<SingleFamily>().Override();

            assert.Method<string>(
                "ToString",
                BindingFlags.Public |
                BindingFlags.Instance
            ).DeclaredIn<SingleFamily>().Override();

        }

        [Test]
        public void TestSingleFamilyData()
        {
            var singleFamily = new TypeAssert<SingleFamily>();
            dynamic o;
            Catch<ArgumentOutOfRangeException>(() => singleFamily.New("One Main Street", 1799, 100_000m));
            Catch<ArgumentOutOfRangeException>(() => singleFamily.New("Two Main Street", 2000, 0m));
            o = singleFamily.New("Two Main Street", 1800, 100_000m);
            Assert.AreEqual(100_000m, o.Price);
            Assert.AreEqual(100_000m, o.TotalCost);
            Assert.AreEqual(1800, o.YearBuilt);
            Assert.AreEqual("Two Main Street 1800 $100,000.00", o.ToString());
        }
    }
}
