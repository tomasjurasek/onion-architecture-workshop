using Review.Domain.Entities;
using System;
using Xunit;

namespace Review.Tests.Domain
{
    public class ProductTests
    {

        [Theory]
        [InlineData("")]
        public void Create_When_InputAreNotValid_Should_ThrowsArgumentException(string code)
        {
            Assert.Throws<ArgumentException>(() => new Product(code));
        }

        [Theory]
        [InlineData(null)]
        public void Create_When_InputAreNotValid_Should_ThrowsArgumentNullException(string code)
        {
            Assert.Throws<ArgumentNullException>(() => new Product(code));
        }
    }
}
