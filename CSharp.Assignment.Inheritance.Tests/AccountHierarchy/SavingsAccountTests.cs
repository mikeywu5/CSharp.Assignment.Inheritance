using System;
using NUnit.Framework;
using CSharp.Assignments.Tests.Library;
using System.Reflection;

namespace CSharp.Assignments.Classes.AccountHierarchy.Tests
{
    public class SavingsAccountTests
    {
        [Test]
        public void Constructor()
        {
            var assert = new TypeAssert<SavingsAccount>();
            assert.Extends<Account>();
            assert.Constructor(
                BindingFlags.Instance |
                BindingFlags.Public,
                new Param<decimal>("initialBalance"),
                new Param<decimal>("transactionFee")
            );

            assert.NonConstructor(
                BindingFlags.Instance |
                BindingFlags.Public,
                new Param<decimal>("initialBalance")
            );
            // new SavingsAccount(11.5m, -0.05m);
            assert.Catch<ArgumentOutOfRangeException>(() => assert.New(11.5m, -0.05m));
        }

        [Test]
        public void Credit()
        {
            var assert = new TypeAssert<SavingsAccount>();
            assert.Method<bool>(
                "Credit",
                BindingFlags.Instance |
                BindingFlags.Public,
                new Param<decimal>("amount")
            ).Override();

            decimal rate = 0.035m;
            dynamic o;
            // new SavingsAccount(1000m, rate);
            o = assert.New(1000m, rate);
            Assert.IsTrue(o.Credit(500m));
            Assert.AreEqual(1500m, o.Balance);
            Assert.IsTrue(o.Credit(2m));
            Assert.AreEqual(1502m, o.Balance);
        }

        [Test]
        public void Debit()
        {
            var assert = new TypeAssert<SavingsAccount>();
            assert.Method<bool>(
                "Debit",
                BindingFlags.Instance |
                BindingFlags.Public,
                new Param<decimal>("amount")
            ).Override();

            decimal rate = 0.035m;
            dynamic o;
            // new SavingsAccount(1000m, rate);
            o = assert.New(1000m, rate);
            Assert.IsTrue(o.Debit(491m));
            Assert.AreEqual(509m, o.Balance);
            Assert.IsFalse(o.Debit(509.01m));
            Assert.AreEqual(509m, o.Balance);
            Assert.IsTrue(o.Debit(509m));
            Assert.AreEqual(0m, o.Balance);
        }

        [Test]
        public void CalculateInterest()
        {
            var assert = new TypeAssert<SavingsAccount>();
            assert.Method<decimal>(
                "CalculateInterest",
                BindingFlags.Instance |
                BindingFlags.Public
            ).DeclaredIn<SavingsAccount>();

            dynamic o;
            decimal rate = 0.035m;
            o = assert.New(1234m, rate);
            Assert.AreEqual(43.19m, o.CalculateInterest());
        }
    }
}
