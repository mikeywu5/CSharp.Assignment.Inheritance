using System;
using CSharp.Assignments.Tests.Library;
using NUnit.Framework;
using System.Reflection;
namespace CSharp.Assignments.Classes.AccountHierarchy.Tests
{
    public class AccountTests
    {
        [Test]
        public void Constructor()
        {
#if !DEBUG
            Assert.Multiple(() =>
            {
#endif
            var assert = new TypeAssert<Account>();
            assert.Constructor(
               BindingFlags.Instance |
               BindingFlags.Public,
               new Param<decimal>("balance")
           );
            dynamic account;
            // new Account(1000.5m);
            account = assert.New(1000.5m);
            Assert.AreEqual(1000.5m, account.Balance);
            assert.Catch<ArgumentOutOfRangeException>(() => account = assert.New(-0.5m));
#if !DEBUG
            });
#endif
        }
        [Test]
        public void Balance()
        {
#if !DEBUG
            Assert.Multiple(() =>
            {
#endif
            var assert = new TypeAssert<Account>();
            var p = assert.Property<decimal>(
                "Balance",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.GetProperty |
                BindingFlags.SetProperty
            ).NonVirtual();

            dynamic account;
            account = assert.New(0m);
            Assert.AreEqual(0m, account.Balance);
            account.Balance = 123.45m;
            Assert.AreEqual(123.45m, account.Balance);
            assert.Catch<ArgumentOutOfRangeException>(() => account.Balance = -0.05m);
            Assert.AreEqual(123.45m, account.Balance);
#if !DEBUG
            });
#endif
        }

        [Test]
        public void Credit()
        {
#if !DEBUG
            Assert.Multiple(() =>
            {
#endif
            var assert = new TypeAssert<Account>();
            var p = assert.Method<bool>(
                "Credit",
                BindingFlags.Instance |
                BindingFlags.Public,
                new Param<decimal>("amount")
            ).Virtual();

            dynamic account;
            account = assert.New(100m);
            Assert.AreEqual(100m, account.Balance);
            bool credit = account.Credit(123.45m);
            Assert.AreEqual(223.45m, account.Balance);
            Assert.IsTrue(credit);
            assert.Catch<ArgumentOutOfRangeException>(() => account.Credit(-0.45m));
            Assert.AreEqual(223.45m, account.Balance);
#if !DEBUG
            });
#endif
        }

        [Test]
        public void Debit()
        {
#if !DEBUG
            Assert.Multiple(() =>
            {
#endif
            var assert = new TypeAssert<Account>();
            var p = assert.Method<bool>(
                "Debit",
                BindingFlags.Instance |
                BindingFlags.Public,
                new Param<decimal>("amount")
            ).Virtual();

            dynamic account;
            account = assert.New(100m);
            Assert.AreEqual(100m, account.Balance);
            bool debit = account.Debit(23.45m);
            Assert.AreEqual(76.55m, account.Balance);
            Assert.IsTrue(debit);
            debit = account.Debit(76.56m);
            Assert.IsFalse(debit);
            Assert.AreEqual(76.55m, account.Balance);
            assert.Catch<ArgumentOutOfRangeException>(() => account.Debit(-0.01m));
            Assert.AreEqual(76.55m, account.Balance);
#if !DEBUG
            });
#endif
        }
    }
}
