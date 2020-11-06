using System;
using NUnit.Framework;
using CSharp.Assignments.Tests.Library;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Assignment.Inheritance.TwoThreeCalc.Tests
{
    public class BaseCalcTests
    {
        [Test]
        public void TestBaseCalcClass()
        {
            var assert = new TypeAssert<BaseCalc>();
            assert.Constructor(
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).Protected();

            assert.Method<int>(
                "Calculate",
                BindingFlags.Public |
                 BindingFlags.DeclaredOnly |
                BindingFlags.Instance
            ).Virtual();
        }
    }
}
