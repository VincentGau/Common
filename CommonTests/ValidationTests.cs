using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
    [TestClass()]
    public class ValidationTests
    {
        [TestMethod()]
        public void IsDateTimeTest()
        {
            Assert.AreEqual(Validation.IsDateTime("2018-12-12"), true);
            Assert.AreEqual(Validation.IsDateTime("2018/12/12"), true);
            Assert.AreEqual(Validation.IsDateTime("2018/12/2"), true);
            Assert.AreEqual(Validation.IsDateTime("2018/12/02"), true);
            Assert.AreEqual(Validation.IsDateTime("2018-12-123"), false);
            Assert.AreEqual(Validation.IsDateTime("2018-12-32"), false);
            Assert.AreEqual(Validation.IsDateTime("20181213"), false);
        }

        [TestMethod()]
        public void IsPhoneNoTest()
        {
            Assert.AreEqual(Validation.IsPhoneNo("13800138000"), true);
            Assert.AreEqual(Validation.IsPhoneNo("138001380001"), false);
            Assert.AreEqual(Validation.IsPhoneNo("138001380"), false);
            Assert.AreEqual(Validation.IsPhoneNo("A1380013800"), false);
            Assert.AreEqual(Validation.IsPhoneNo("23423423423"), false);
            Assert.AreEqual(Validation.IsPhoneNo("1380013800@"), false);
            Assert.AreEqual(Validation.IsPhoneNo("1380013800o"), false);
        }
    }
}