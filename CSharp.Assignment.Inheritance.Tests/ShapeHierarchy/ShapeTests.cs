// Exercise 12.10 Solution: ShapeTest.cs
// Application tests the Shape hierarchy.
using System;
using CSharp.Assignments.Tests.Library;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace CSharp.Assignments.Inheritance.ShapeHierarchy.Tests
{
    public class ShapeTests
    {
        [Test]
        public void TestShapeClass ()
        {
            var assert = new TypeAssert<Shape>();

            assert.Constructor(
                BindingFlags.Instance |
                BindingFlags.NonPublic,
                new Param<int>("x"),
                new Param<int>("y")
            ).Protected();


            assert.Property<int>(
                 "X",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.GetProperty
            );

            assert.Property<int>(
                "Y",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.GetProperty
            );

            assert.Property<string>(
                "Name",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.GetProperty
            );


            assert.Property<string>(
                "Name",
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty
            ).Virtual();

            assert.Method<string>(
                "ToString",
                BindingFlags.Public |
                BindingFlags.Instance
            ).Override();
        }


        /// <summary>
        /// Tests the polymorphism of the compute method.
        /// </summary>
        [Test]
        public void TestCompute()
        {
            var assert = new TypeAssert<Shape>();
            var compute = assert.Method(
                "Compute",
                BindingFlags.Static |
                BindingFlags.Public,
                new Param<Shape[]>("shapes")
            );

            Shape[] shapes = new Shape[4];
            // shapes[0] = new Circle(22, 88, 4);
            shapes[0] = (Shape)Activator.CreateInstance(typeof(Circle), 22, 88, 4);
            // shapes[1] = new Cube(79, 61, 8);
            shapes[1] = (Shape)Activator.CreateInstance(typeof(Cube), 79, 61, 8);
            // shapes[2] = new Sphere(8, 89, 2);
            shapes[2] = (Shape)Activator.CreateInstance(typeof(Sphere), 8, 89, 2);
            // shapes[3] = new Square(71, 96, 10);
            shapes[3] = (Shape)Activator.CreateInstance(typeof(Square), 71, 96, 10);

            // Shape.Compute(shapes);
            Action app = () => compute.Invoke(null, new[] { shapes });
            string actual = app.Run();
            actual.Assert(
                "Circle",
                "(22, 88) radius: 4",
                "50.26548246",
                "Cube",
                "(79, 61) side: 8",
                "384",
                "512",
                "Sphere",
                "(8, 89) radius: 2",
                "50.26548246",
                "33.51032164",
                "Square",
                "(71, 96) side: 10",
                "100"
            );
        }
    }
}
