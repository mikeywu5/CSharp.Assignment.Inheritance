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
    public class HousingTests
    {
        [Test]
        public void TestCondoClass()
        {
            var assert = new TypeAssert<Condo>();
            assert.Extends<Home>();
            assert.NonAbstract();

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

            assert.Property<decimal>(
                "Fee",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty);

            assert.Property<string>(
                "UnitNumber",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            );

            assert.Property<bool>(
                "IsRental",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty |
                BindingFlags.SetProperty
            );

            assert.NonConstructor(
                BindingFlags.Public |
                BindingFlags.Instance
            );

            assert.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<string>("address"),
                new Param<int>("yearBuilt"),
                new Param<decimal>("price"),
                new Param<string>("unitNumber"),
                new Param<decimal>("fee"),
                new Param<bool>("isRental") { Default = false }
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
            ).Override().DeclaredIn<Condo>(); ;

            assert.Method<decimal>(
                "GetRate",
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int>("numberOfPeriods")
            ).Override().DeclaredIn<Condo>();

            assert.Method<decimal>(
                "GetMonthlyRate",
                BindingFlags.Public |
                BindingFlags.Instance
            );
        }

        [Test]
        public void TestCondoData()
        {
            var condo = new TypeAssert<Condo>();
            dynamic o;
            Catch<ArgumentOutOfRangeException>(() => condo.New("One Main Street", 1799, 100_000m, "A", 1m, false));
            Catch<ArgumentOutOfRangeException>(() => condo.New("Two Main Street", 2000, 0m, "A", 1m, true));
            Catch<ArgumentOutOfRangeException>(() => condo.New("Two Main Street", 2000, 100m, "A", -1m, false));
            Catch<ArgumentOutOfRangeException>(() => condo.New("Two Main Street", 2050, 100m, "A", 1m, false));
            o = condo.New("Two Main Street", 1800, 100_000m, "A", 2000m, false);
            Assert.AreEqual(2000m, o.Fee);
            Assert.AreEqual(100_000m, o.Price);
            Assert.AreEqual(102_000m, o.TotalCost);
            Assert.AreEqual(1800, o.YearBuilt);
            Assert.AreEqual("A", o.UnitNumber);
            Assert.IsFalse(o.IsRental);
            Catch<InvalidOperationException>(() => o.GetMonthlyRate());
            Assert.AreEqual("Two Main Street 1800 A $102,000.00", o.ToString());

            o.IsRental = true;
            Assert.AreEqual(2000m, o.Fee);
            Assert.AreEqual(100_000m, o.Price);
            Assert.AreEqual(102_000m, o.TotalCost);
            Assert.AreEqual(1800, o.YearBuilt);
            Assert.AreEqual("A", o.UnitNumber);
            Assert.IsTrue(o.IsRental);
            Assert.AreEqual(425, o.GetMonthlyRate());
            Assert.AreEqual("Two Main Street 1800 A $425.00", o.ToString());
        }
    }
}
