using System;
using NUnit.Framework;
using CSharp.Assignments.Tests.Library;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Assignment.Inheritance.TwoThreeCalc.Tests
{
    public class TwoCalcTests
    {
        [Test]
        public void TestTwoCalcClass()
        {
            var assert = new TypeAssert<TwoCalc>();
            assert.Class();
            assert.Extends<BaseCalc>();
            assert.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int>("a"),
                new Param<int>("b")
            );

            assert.Field<int>(
                "_a",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).Private().ReadOnly();

            assert.Field<int>(
                "_b",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).Private().ReadOnly();

            assert.Method<int>(
                "Calculate",
                BindingFlags.Public |
                BindingFlags.Instance
            ).Override().Virtual();

            assert.Method<string>(
                "ToString",
                BindingFlags.Public |
                BindingFlags.Instance
            ).Override();
        }

        [Test]
        public void TestTwoCalcData()
        {
            var assert = new TypeAssert<TwoCalc>();
            dynamic calc;
            calc = assert.New(1, 2);
            Assert.AreEqual(5, calc.Calculate());
            Assert.AreEqual("1, 2", calc.ToString());
            calc = assert.New(-10, 4);
            Assert.AreEqual(-2, calc.Calculate());
            Assert.AreEqual("-10, 4", calc.ToString());
        }
    }
}
