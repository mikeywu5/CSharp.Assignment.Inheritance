// Exercise 12.10 Solution: ShapeTest.cs
// Application tests the Shape hierarchy.
using System;
using CSharp.Assignments.Tests.Library;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace CSharp.Assignments.Inheritance.ShapeHierarchy.Tests
{
    public class CircleTests
    {
        [Test]
        public void TestCircleClass()
        {
            var assert = new TypeAssert<Circle>();
            assert.Extends<TwoDimensionalShape>();
            assert.NonAbstract();
            assert.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int>("x"),
                new Param<int>("y"),
                new Param<int>("radius")
            );
            assert.ConstructorCount(1);

            assert.Property<string>(
                "Name",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            ).Override();

            assert.Property<double>(
                "Area",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            ).Override();

            assert.Property<int>(
                "Radius",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty |
                BindingFlags.SetProperty
            );

            assert.Method<string>(
                "ToString",
                BindingFlags.Public |
                BindingFlags.Instance
            ).Override();
            
            assert.Field<int>(
                "Dimension1",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).DeclaredIn<TwoDimensionalShape>();

            assert.Field<int>(
                "Dimension2",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).DeclaredIn<TwoDimensionalShape>();

        }

        [Test]
        public void TestCircleData()
        {
            var assert = new TypeAssert<Circle>();

            var dimension1 = assert.Field<int>(
                "Dimension1",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            var dimension2 = assert.Field<int>(
                "Dimension2",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            // var obj = new Circle(1, 2 ,3);
            dynamic obj = assert.New(1, 2, 3);
            Assert.AreEqual("Circle", obj.Name);
            Assert.AreEqual(1, obj.X);
            Assert.AreEqual(2, obj.Y);
            Assert.AreEqual(3, obj.Radius);
            Assert.AreEqual(3, dimension1.GetValue(obj));
            Assert.AreEqual(3, dimension2.GetValue(obj));
            Assert.AreEqual(28.274333882308138, obj.Area, 1E-10);
            Assert.AreEqual("(1, 2) radius: 3", obj.ToString());

            obj.Radius = 7;
            Assert.AreEqual(7, dimension1.GetValue(obj));
            Assert.AreEqual(7, dimension2.GetValue(obj));
            Assert.AreEqual(153.93804002589985, obj.Area, 1E-10);
            Assert.AreEqual("(1, 2) radius: 7", obj.ToString());
        }
    }
}
