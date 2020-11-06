// Exercise 12.10 Solution: ShapeTest.cs
// Application tests the Shape hierarchy.
using System;
using CSharp.Assignments.Tests.Library;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace CSharp.Assignments.Inheritance.ShapeHierarchy.Tests
{
    public class ThreeDimensionalShapeTests
    {
        [Test]
        public void TestThreeDimensionalShapeClass()
        {
            var assert = new TypeAssert<ThreeDimensionalShape>();
            assert.Extends<Shape>();
            assert.Constructor(
                BindingFlags.NonPublic |
                BindingFlags.Instance,
                new Param<int>("x"),
                new Param<int>("y"),
                new Param<int>("dimension1"),
                new Param<int>("dimension2"),
                new Param<int>("dimension3")
            ).Protected();

            assert.Field<int>(
                "Dimension1",
                BindingFlags.Instance |
                BindingFlags.NonPublic
            ).Protected();

            assert.Field<int>(
                "Dimension2",
                BindingFlags.Instance |
                BindingFlags.NonPublic
            ).Protected();

            assert.Field<int>(
                "Dimension3",
                BindingFlags.Instance |
                BindingFlags.NonPublic
            ).Protected();

            assert.Property<double>(
                "Area",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.GetProperty
            ).Virtual();

            assert.Property<double>(
                "Volume",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.GetProperty
            ).Virtual();
        }
    }
}
