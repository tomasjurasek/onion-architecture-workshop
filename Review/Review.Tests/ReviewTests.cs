using NUnit.Framework;

namespace Review.Tests
{
    public class ReviewTests
    {

        [Test]
        public void Create_When_DescriptionIsNullOrEmpty_Should_ThrowsException()
        {
            Assert.Pass();
        }

        [Test]
        public void Create_When_Valid_Should_CreatesReview()
        {
            Assert.Pass();
        }

        [Test]
        public void Like_When_UserAlreadyLiked_Should_ReturnsUserAlreadyLikedResult()
        {
            Assert.Pass();
        }

        [Test]
        public void Like_When_Valid_Should_UserLikedIt()
        {
            Assert.Pass();
        }

        [Test]
        public void Dislike_When_UserAlreadyDisliked_Should_ReturnsUserAlreadyDislikedResult()
        {
            Assert.Pass();
        }

        [Test]
        public void Dislike_When_Valid_Should_DislikedIt()
        {
            Assert.Pass();
        }
    }
}