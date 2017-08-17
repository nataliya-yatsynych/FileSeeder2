using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eCrtSeederNS;

namespace ecertSeeder.Tests
{
    [TestClass]
    public class SIN_Test
    {
        [TestMethod]
        public void InValidSIN()
        {

            string InvalidSIN = "111333444";
            Assert.IsFalse(InvalidSIN.LuhnCheck());
        }
        [TestMethod]
        public void ValidSIN()
        {

            string ValidSIN = "430665687";
            Assert.IsTrue(ValidSIN.LuhnCheck());
        }
    }
}
