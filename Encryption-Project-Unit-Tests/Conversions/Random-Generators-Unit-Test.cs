using Encryption_Project_LIB.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Encryption_Project_Unit_Tests.Conversions
{
    public class Random_Generators_Unit_Test
    {

        [Fact]
        public void GENERATE_RANDOM_STRING_TEST()
        {
            var result = RandomGenerator.RandomString(10);
            Assert.NotNull(result);
        }

        [Fact]
        public void GENERATE_RANDOM_NUMBER()
        {
            var result = RandomGenerator.RandomNumber(1, 10);
#pragma warning disable xUnit2002 // Do not use null check on value type
            Assert.NotNull(result);
#pragma warning restore xUnit2002 // Do not use null check on value type
        }
    }
}
