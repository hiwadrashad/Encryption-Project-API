using Encryption_Project_LIB.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Encryption_Project_Unit_Tests.Conversions
{
    public class Converter_Unit_Test
    {
        [Fact]
        public void RETURN_JSON_FROM_ROOT_TEST()
        {
            Mock<IConverter> Converter = new Mock<IConverter>();
            List<Encryption_Project_LIB.DTOs.Root> RootList = new List<Encryption_Project_LIB.DTOs.Root>();
            Converter.Setup(a => a.ConvertJSON()).Returns(RootList);
            var result = Converter.Object.ConvertJSON();
            Assert.NotNull(result);
        }

        [Fact]
        public void CONVERT_STRING_TO_BYTE_ARRAY_TEST()
        {
            Mock<IConverter> Converter = new Mock<IConverter>();
            byte[] RETURNVALUE = new byte[] { 74, 65 ,73 ,74 };
            Converter.Setup(a => a.GetBytes("test")).Returns(RETURNVALUE);
            var result = Converter.Object.GetBytes("test");
            Assert.NotNull(result);
        }

        [Fact]
        public void CONVERT_BYTE_ARRAY_TO_STRING_TEST()
        {
            Mock<IConverter> Converter = new Mock<IConverter>();
            string INPUTRESULT = "test";
            byte[] RETURNRESULT = new byte[] { 74, 65 ,73 ,74 };
            Converter.Setup(a => a.GetString(RETURNRESULT)).Returns(INPUTRESULT);
            var result = Converter.Object.GetString(RETURNRESULT);
            Assert.NotNull(result);
        }
    }
}
