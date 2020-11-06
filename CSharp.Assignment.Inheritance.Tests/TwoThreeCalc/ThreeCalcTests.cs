using System;
using NUnit.Framework;
using CSharp.Assignments.Tests.Library;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Assignment.Inheritance.TwoThreeCalc.Tests
{
    public class ThreeCalcTests
    {

        [Test]
        public void TestThreeCalcClass()
        {
            var assert = new TypeAssert<ThreeCalc>();
            assert.Class();
            assert.Extends<TwoCalc>();

            assert.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int>("a"),
                new Param<int>("b"),
                new Param<int>("c")
            );

            assert.NonField<int>(
                "_a",
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance
            );

            assert.NonField<int>(
                "_b",
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance
            );

            assert.Field<int>(
                "_c",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).Private().ReadOnly();

            assert.Method<int>(
                "Calculate",
                BindingFlags.Public |
                BindingFlags.Instance
            ).Override();

            assert.Method<string>(
                "ToString",
                BindingFlags.Public |
                BindingFlags.Instance
            ).Override();
        }

        [Test]
        public void TestThreeCalcData()
        {
            var assert = new TypeAssert<ThreeCalc>();
            dynamic calc;
            calc = assert.New(1, 2, 3);
            Assert.AreEqual(14, calc.Calculate());
            Assert.AreEqual("1, 2, 3", calc.ToString());

            calc = assert.New(1, 3, 5);
            Assert.AreEqual(22, calc.Calculate());
            Assert.AreEqual("1, 3, 5", calc.ToString());

            calc = assert.New(-10, 4, -100);
            Assert.AreEqual(-302, calc.Calculate());
            Assert.AreEqual("-10, 4, -100", calc.ToString());
        }
    }
}
