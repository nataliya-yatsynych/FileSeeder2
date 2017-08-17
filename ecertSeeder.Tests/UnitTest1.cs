using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eCrtSeederNS;

namespace ecertSeeder.Tests
{
    [TestClass]
    public class SInTest
    {
        [TestMethod]
        public void TestalidSIN()
        {

            long test= ValidSIN.GenerateValidSIN();
        }
    }
}
