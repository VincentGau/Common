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
            Assert.IsTrue(Validation.IsDateTime("2018-12-12"));
            Assert.IsTrue(Validation.IsDateTime("2018/12/12"));
            Assert.IsTrue(Validation.IsDateTime("2018/12/2"));
            Assert.IsTrue(Validation.IsDateTime("2018/12/02"));
            Assert.IsFalse(Validation.IsDateTime("2018-12-123"));
            Assert.IsFalse(Validation.IsDateTime("2018-12-32"));
            Assert.IsFalse(Validation.IsDateTime("20181213"));
        }

        [TestMethod()]
        public void IsPhoneNoTest()
        {
            Assert.IsTrue(Validation.IsPhoneNo("13800138000"));
            Assert.IsFalse(Validation.IsPhoneNo("138001380001"));
            Assert.IsFalse(Validation.IsPhoneNo("138001380"));
            Assert.IsFalse(Validation.IsPhoneNo("A1380013800"));
            Assert.IsFalse(Validation.IsPhoneNo("23423423423"));
            Assert.IsFalse(Validation.IsPhoneNo("1380013800@"));
            Assert.IsFalse(Validation.IsPhoneNo("1380013800o"));
            Assert.IsFalse(Validation.IsPhoneNo("1380013800o"));
        }

        [TestMethod()]
        public void IsChineseTest()
        {
            Assert.IsFalse(Validation.IsChinese("a"));
            Assert.IsTrue(Validation.IsChinese("啊"));
            Assert.IsTrue(Validation.IsChinese("，"));
            Assert.IsFalse(Validation.IsChinese(","));
        }
    }
}