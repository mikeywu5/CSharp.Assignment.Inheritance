using CSharp.Assignments.Tests.Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CSharp.Assignment.Inheritance.Averages.Tests
{
    class AggregatorTests
    {
        [Test]
        public void TestAggregatorClass()
        {
            var aggregator = new TypeAssert<Aggregator>();
            aggregator.Class();
            aggregator.NonAbstract();
            aggregator.PropertyOrField<int[]>("Numbers",
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.GetField |
                BindingFlags.GetProperty)?.Protected()?.ReadOnly();

            aggregator.Constructor(
                BindingFlags.NonPublic |
                BindingFlags.Instance,
                new Param<int[]>("numbers"))?.Protected();

            aggregator.Method<int>(
                "GetValue",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty,
                new Param<int>("n")
            )?.Virtual();

            aggregator.Method<string>(
                "ToString",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly
            ).Override();
        }



    }
}