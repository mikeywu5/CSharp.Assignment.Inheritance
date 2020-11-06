// Exercise 12.10 Solution: ShapeTest.cs
// Application tests the Shape hierarchy.
using System;
using CSharp.Assignments.Tests.Library;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace CSharp.Assignments.Inheritance.ShapeHierarchy.Tests
{
    public class SphereTests
    {
        [Test]
        public void TestSphereClass()
        {
            var assert = new TypeAssert<Sphere>();
            assert.Extends<ThreeDimensionalShape>();
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

            assert.Property<double>(
                "Volume",
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
        public void TestSphereData()
        {
            var assert = new TypeAssert<Sphere>();
            // var obj = new Sphere(1, 2 ,3);

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

            dynamic obj = assert.New(2, 1, 4);
            Assert.AreEqual("Sphere", obj.Name);
            Assert.AreEqual(2, obj.X);
            Assert.AreEqual(1, obj.Y);
            Assert.AreEqual(4, obj.Radius);
            Assert.AreEqual(4, dimension1.GetValue(obj));
            Assert.AreEqual(4, dimension2.GetValue(obj));
            Assert.AreEqual(4, dimension3.GetValue(obj));
            Assert.AreEqual(201.06192982974676, obj.Area, 1E-10);
            Assert.AreEqual(268.08257310632899, obj.Volume, 1E-10);
            Assert.AreEqual("(2, 1) radius: 4", obj.ToString());

            obj.Radius = 7;
            Assert.AreEqual(7, dimension1.GetValue(obj));
            Assert.AreEqual(7, dimension2.GetValue(obj));
            Assert.AreEqual(615.75216010359941, obj.Area, 1E-10);
            Assert.AreEqual(1436.7550402417321, obj.Volume, 1E-10);
            Assert.AreEqual("(2, 1) radius: 7", obj.ToString());
        }
    }
}
