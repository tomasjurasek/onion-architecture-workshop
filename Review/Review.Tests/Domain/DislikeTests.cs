using Review.Domain.Entities;
using System;
using Xunit;

namespace Review.Tests.Domain
{
    public class DislikeTests
    {
        [Fact]
        public void Create_When_InputAreNotValid_Should_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Dislike(null, new DateTime(2021, 10, 1)));
        }
    }
}
