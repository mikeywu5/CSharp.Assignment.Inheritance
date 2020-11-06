// Exercise 12.10 Solution: ShapeTest.cs
// Application tests the Shape hierarchy.
using System;
using CSharp.Assignments.Tests.Library;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace CSharp.Assignments.Inheritance.ShapeHierarchy.Tests
{
    public class CubeTests
    {

        [Test]
        public void TestCubeClass()
        {
            var assert = new TypeAssert<Cube>();
            assert.Extends<ThreeDimensionalShape>();
            assert.NonAbstract();
            assert.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int>("x"),
                new Param<int>("y"),
                new Param<int>("side")
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

            assert.Property<double>(
                "Volume",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            ).Override();

            assert.Property<int>(
                "Side",
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
            ).DeclaredIn<ThreeDimensionalShape>();

            assert.Field<int>(
                "Dimension2",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).DeclaredIn<ThreeDimensionalShape>();

            assert.Field<int>(
                "Dimension3",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).DeclaredIn<ThreeDimensionalShape>();
        }

        [Test]
        public void TestCubeData()
        {
            var assert = new TypeAssert<Cube>();
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

            var dimension3 = assert.Field<int>(
                "Dimension3",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            // var obj = new Sphere(1, 2 ,3);
            dynamic obj = assert.New(3, 5, 2);
            Assert.AreEqual("Cube", obj.Name);
            Assert.AreEqual(3, obj.X);
            Assert.AreEqual(5, obj.Y);
            Assert.AreEqual(2, obj.Side);
            Assert.AreEqual(2, dimension1.GetValue(obj));
            Assert.AreEqual(2, dimension2.GetValue(obj));
            Assert.AreEqual(2, dimension3.GetValue(obj));
            Assert.AreEqual(24, obj.Area, 1E-10);
            Assert.AreEqual(8, obj.Volume, 1E-10);
            Assert.AreEqual("(3, 5) side: 2", obj.ToString());

            obj.Side = 7;
            Assert.AreEqual(7, dimension1.GetValue(obj));
            Assert.AreEqual(7, dimension2.GetValue(obj));
            Assert.AreEqual(294, obj.Area, 1E-10);
            Assert.AreEqual(343, obj.Volume, 1E-10);
            Assert.AreEqual("(3, 5) side: 7", obj.ToString());
        }
    }
}
