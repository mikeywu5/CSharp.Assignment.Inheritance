using System;
using NUnit.Framework;
using CSharp.Assignments.Tests.Library;
using System.Reflection;

namespace CSharp.Assignments.Classes.AccountHierarchy.Tests
{
    public class CheckingAccountTests
    {
        [Test]
        public void Constructor()
        {
#if !DEBUG
        Assert.Multiple(() => {
#endif
            var assert = new TypeAssert<CheckingAccount>();
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
            // new CheckingAccount(11.5m, -0.05m);
            assert.Catch<ArgumentOutOfRangeException>(() => assert.New(11.5m, -0.05m));
#if !DEBUG
        });
#endif
        }

        [Test]
        public void Credit()
        {
#if !DEBUG
        Assert.Multiple(() => {
#endif
            var assert = new TypeAssert<CheckingAccount>();
            assert.Method<bool>(
                "Credit",
                BindingFlags.Instance |
                BindingFlags.Public,
                new Param<decimal>("amount")
            ).Override();

            decimal fee = 3.5m;
            dynamic o;
            // new CheckingAccount(1000m, fee);
            o = assert.New(1000m, fee);
            Assert.IsTrue(o.Credit(500m));
            Assert.AreEqual(1496.5m, o.Balance);
            Assert.IsFalse(o.Credit(2m));
            Assert.AreEqual(1496.5m, o.Balance);
#if !DEBUG
    });
#endif
        }

        [Test]
        public void Debit()
        {
#if !DEBUG
        Assert.Multiple(() => {
#endif
            var assert = new TypeAssert<CheckingAccount>();
            assert.Method<bool>(
                "Debit",
                BindingFlags.Instance |
                BindingFlags.Public,
                new Param<decimal>("amount")
            ).Override();

            decimal fee = 3.5m;
            dynamic o;
            // new CheckingAccount(1000m, fee);
            o = assert.New(1000m, fee);
            Assert.IsTrue(o.Debit(500m));
            Assert.AreEqual(496.5m, o.Balance);
            Assert.IsFalse(o.Debit(494.0m));
            Assert.AreEqual(496.5m, o.Balance);
#if !DEBUG
});
#endif
        }
    }
}
