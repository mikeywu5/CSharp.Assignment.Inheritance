using System;
using NUnit.Framework;
using CSharp.Assignments.Tests.Library;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Assignment.Inheritance.TwoThreeCalc.Tests
{
    public class ListCalcTests
    {

        [Test]
        public void TestListCalcClass()
        {
            var assert = new TypeAssert<ListCalc>();
            assert.Extends<BaseCalc>();
            assert.Class();
            assert.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<List<BaseCalc>>("calculations")
            );

            assert.Constructor(
                BindingFlags.Public |
                BindingFlags.Instance,
                new Param<int[]>("values") { Params = true }
            );

            assert.Field<List<BaseCalc>>(
                "Calculations",
                BindingFlags.NonPublic |
                BindingFlags.Instance
            ).Protected().ReadOnly();

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
        public void TestListCalcData()
        {
            var assert = new TypeAssert<ListCalc>();
            dynamic o;
            assert.Catch<ArgumentNullException>(() => o = assert.New(null));
            assert.Catch<ArgumentException>(() => o = assert.New());
            assert.Catch<ArgumentException>(() => o = assert.New(0));
            assert.Catch<ArgumentException>(() => o = assert.New(1));
            assert.Catch<ArgumentException>(() => o = assert.New(1000));

            o = assert.New(1, 2);
            Assert.AreEqual(5, o.Calculate());
            Assert.AreEqual("1, 2", o.ToString());

            o = assert.New(1, 2, 3);
            Assert.AreEqual(14, o.Calculate());
            Assert.AreEqual("1, 2, 3", o.ToString());

            o = assert.New(1, 2, 3, 4);
            Assert.AreEqual(16, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4", o.ToString());

            o = assert.New(1, 2, 3, 4, 5);
            Assert.AreEqual(31, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6);
            Assert.AreEqual(46, o.Calculate());
            Assert.AreEqual("1, 2, 3; 4, 5, 6", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual(51, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8);
            Assert.AreEqual(75, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(96, o.Calculate());
            Assert.AreEqual("1, 2, 3; 4, 5, 6; 7, 8, 9", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.AreEqual(104, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
            Assert.AreEqual(137, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            Assert.AreEqual(164, o.Calculate());
            Assert.AreEqual("1, 2, 3; 4, 5, 6; 7, 8, 9; 10, 11, 12", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);
            Assert.AreEqual(175, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);
            Assert.AreEqual(217, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
            Assert.AreEqual(250, o.Calculate());
            Assert.AreEqual("1, 2, 3; 4, 5, 6; 7, 8, 9; 10, 11, 12; 13, 14, 15", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Assert.AreEqual(264, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17);
            Assert.AreEqual(315, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18);
            Assert.AreEqual(354, o.Calculate());
            Assert.AreEqual("1, 2, 3; 4, 5, 6; 7, 8, 9; 10, 11, 12; 13, 14, 15; 16, 17, 18", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19);
            Assert.AreEqual(371, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17; 18, 19", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20);
            Assert.AreEqual(431, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17; 18, 19, 20", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21);
            Assert.AreEqual(476, o.Calculate());
            Assert.AreEqual("1, 2, 3; 4, 5, 6; 7, 8, 9; 10, 11, 12; 13, 14, 15; 16, 17, 18; 19, 20, 21", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22);
            Assert.AreEqual(496, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17; 18, 19, 20; 21, 22", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23);
            Assert.AreEqual(565, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17; 18, 19, 20; 21, 22, 23", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24);
            Assert.AreEqual(616, o.Calculate());
            Assert.AreEqual("1, 2, 3; 4, 5, 6; 7, 8, 9; 10, 11, 12; 13, 14, 15; 16, 17, 18; 19, 20, 21; 22, 23, 24", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25);
            Assert.AreEqual(639, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17; 18, 19, 20; 21, 22, 23; 24, 25", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26);
            Assert.AreEqual(717, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17; 18, 19, 20; 21, 22, 23; 24, 25, 26", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27);
            Assert.AreEqual(774, o.Calculate());
            Assert.AreEqual("1, 2, 3; 4, 5, 6; 7, 8, 9; 10, 11, 12; 13, 14, 15; 16, 17, 18; 19, 20, 21; 22, 23, 24; 25, 26, 27", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28);
            Assert.AreEqual(800, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17; 18, 19, 20; 21, 22, 23; 24, 25, 26; 27, 28", o.ToString());

            o = assert.New(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29);
            Assert.AreEqual(887, o.Calculate());
            Assert.AreEqual("1, 2; 3, 4, 5; 6, 7, 8; 9, 10, 11; 12, 13, 14; 15, 16, 17; 18, 19, 20; 21, 22, 23; 24, 25, 26; 27, 28, 29", o.ToString());

            o = assert.New(3, 86, 36, 49, 71, 29, 95, 9, 31, 11, 93, 36, 46, 87, 50, 34, 72, 44, 83, 38, 17, 91, 56, 66, 99, 34, 25, 38, 99, 86, 95, 91, 84, 22, 34, 30, 27, 97, 47, 81, 84, 97, 10, 80, 33, 67, 70, 88, 77, 49, 52, 70, 15, 35, 18, 11, 31, 69, 50, 38, 4, 57, 10, 45, 16, 3, 52, 25, 54, 20, 7, 75, 97, 2, 54, 20, 82, 75, 33, 32, 63, 37, 60, 51, 35, 26, 76, 49, 4, 23, 99, 57, 47, 10, 77, 27, 87, 65, 65, 31);
            Assert.AreEqual(10105, o.Calculate());
            Assert.AreEqual("3, 86; 36, 49, 71; 29, 95, 9; 31, 11, 93; 36, 46, 87; 50, 34, 72; 44, 83, 38; 17, 91, 56; 66, 99, 34; 25, 38, 99; 86, 95, 91; 84, 22, 34; 30, 27, 97; 47, 81, 84; 97, 10, 80; 33, 67, 70; 88, 77, 49; 52, 70, 15; 35, 18, 11; 31, 69, 50; 38, 4, 57; 10, 45, 16; 3, 52, 25; 54, 20, 7; 75, 97, 2; 54, 20, 82; 75, 33, 32; 63, 37, 60; 51, 35, 26; 76, 49, 4; 23, 99, 57; 47, 10, 77; 27, 87, 65; 65, 31", o.ToString());

            /*
            var sb = new StringBuilder();
            var list = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(Utils.Random.Next(1, 100));
            }

            var a = list.ToArray();
            o = assert.New(a);
            sb.AppendLine($"o = assert.New({string.Join(", ", list.ToArray())});");
            sb.AppendLine($"Assert.AreEqual({o.Calculate()}, o.Calculate());");
            sb.AppendLine($"Assert.AreEqual(\"{o}\", o.ToString());");
            sb.AppendLine();
            Assert.Fail(sb.ToString());
            */
        }
    }
}
