using System;
using ToyStore.Business.Logic;
using ToyStore.Repository.Models;
using Xunit;

namespace ToyStore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // arrange
            SellableLogic logic = new SellableLogic(new ToyRepository());
            var expected = 6;
            // act
            var i = logic.TestMethod(2);

            // assert
            Assert.Equal(expected, i);
        }
    }
}
