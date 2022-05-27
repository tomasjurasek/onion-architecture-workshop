using Review.Domain.Entities;
using System;
using Xunit;

namespace Review.Tests.Domain
{
    public class UserTests
    {

        [Theory]
        [InlineData("")]
        public void Create_When_InputAreNotValid_Should_ThrowsArgumentException(string userName)
        {
            Assert.Throws<ArgumentException>(() => new User(userName));
        }

        [Theory]
        [InlineData(null)]
        public void Create_When_InputAreNotValid_Should_ThrowsArgumentNullException(string userName)
        {
            Assert.Throws<ArgumentNullException>(() => new User(userName));
        }
    }
}
