using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommonLib.Tests
{
    public class ValitationTests
    {
        [Fact]
        public void IsDateTime_Test()
        {
            Assert.True(Validation.IsDateTime("2018-12-12"));
            Assert.True(Validation.IsDateTime("2018/12/12"));
            Assert.True(Validation.IsDateTime("2018/12/2"));
            Assert.True(Validation.IsDateTime("2018/12/02"));
            Assert.False(Validation.IsDateTime("2018-12-123"));
            Assert.False(Validation.IsDateTime("2018-12-32"));
            Assert.False(Validation.IsDateTime("20181213"));
        }

        [Fact]
        public void IsPhoneNo_Test()
        {
            Assert.True(Validation.IsPhoneNo("13800138000"));
            Assert.False(Validation.IsPhoneNo("138001380001"));
            Assert.False(Validation.IsPhoneNo("138001380"));
            Assert.False(Validation.IsPhoneNo("A1380013800"));
            Assert.False(Validation.IsPhoneNo("23423423423"));
            Assert.False(Validation.IsPhoneNo("1380013800@"));
            Assert.False(Validation.IsPhoneNo("1380013800o"));
            Assert.False(Validation.IsPhoneNo("1380013800o"));
        }
    }
}
