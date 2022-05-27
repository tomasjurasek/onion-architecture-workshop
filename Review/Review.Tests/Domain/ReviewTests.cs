using Review.Domain;
using Review.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Review.Tests.Domain
{
    public class ReviewTests
    {
        private Product _product;
        private User _user;
        private List<Like> _likes;
        private List<Dislike> _dislikes;
        private string _description;
        private DateTime _createdAt;
        private bool _isActive;
        private Guid _id;
        private Review.Domain.Entities.Review _review;

        public ReviewTests()
        {
            _product = new Product("CODE");
            _user = new User("USERNAME");
            _likes = new List<Like>();
            _dislikes = new List<Dislike>();
            _description = "DESCRIPTION";
            _createdAt = new DateTime(2021, 10, 1);
            _isActive = true;
            _id = Guid.NewGuid();

            _review = new Review.Domain.Entities.Review(
                _id,
                _product,
                _user,
                _description,
                _likes,
                _dislikes,
                _createdAt,
                _isActive);
        }

        [Fact]
        public void Create_When_IdIsNotValid_Should_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Review.Domain.Entities.Review(
                Guid.Empty,
                _product,
                _user,
                _description,
                _likes,
                _dislikes,
                _createdAt,
                _isActive));
        }

        [Fact]
        public void Create_When_ProductIsNull_Should_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Review.Domain.Entities.Review(
                Guid.Empty,
                null,
                _user,
                _description,
                _likes,
                _dislikes,
                _createdAt,
                _isActive));
        }

        [Fact]
        public void Create_When_UserIsNull_Should_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Review.Domain.Entities.Review(
                Guid.Empty,
                _product,
                null,
                _description,
                _likes,
                _dislikes,
                _createdAt,
                _isActive));
        }

        [Fact]
        public void Create_When_DescriptionIsNull_Should_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Review.Domain.Entities.Review(
                Guid.Empty,
                _product,
                _user,
                null,
                _likes,
                _dislikes,
                _createdAt,
                _isActive));
        }

        [Fact]
        public void Create_When_DescriptionIsEmpty_Should_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Review.Domain.Entities.Review(
                Guid.Empty,
                _product,
                _user,
                "",
                _likes,
                _dislikes,
                _createdAt,
                _isActive));
        }

        [Fact]
        public void Create_When_LikesAreNull_Should_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Review.Domain.Entities.Review(
                Guid.Empty,
                _product,
                _user,
                _description,
                null,
                _dislikes,
                _createdAt,
                _isActive));
        }

        [Fact]
        public void Create_When_DislikesAreNull_Should_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            new Review.Domain.Entities.Review(
                Guid.Empty,
                _product,
                _user,
                _description,
                _likes,
                null,
                _createdAt,
                _isActive));
        }

        [Fact]
        public void Like_When_UserAlreadyLiked_Should_ReturnsAlreadyLikedResultWithoutNewLike()
        {
            _review.Like(_user);

            var result = _review.Like(_user);

            Assert.Equal(Result.AlreadyLiked, result);
            Assert.Single(_review.Likes);
            Assert.Empty(_review.Dislikes);
        }

        [Fact]
        public void Like_When_UserAlreadyDisliked_Should_ReturnsSuccessResultWithNewLike()
        {
            _review.Dislike(_user);

            var result = _review.Like(_user);

            Assert.Equal(Result.Sucess, result);
            Assert.Single(_review.Likes);
            Assert.Empty(_review.Dislikes);
        }

        [Fact]
        public void Like_When_ReviewIsNew_Should_ReturnsSuccessResult()
        {
            var result = _review.Like(_user);

            Assert.Equal(Result.Sucess, result);
            Assert.Single(_review.Likes);
            Assert.Empty(_review.Dislikes);
        }


        [Fact]
        public void Dislike_When_UserAlreadyLiked_Should_ReturnsAlreadyLikedResult()
        {
            _review.Dislike(_user);

            var result = _review.Dislike(_user);

            Assert.Equal(Result.AlredyDisliked, result);
            Assert.Single(_review.Dislikes);
            Assert.Empty(_review.Likes);
        }

        [Fact]
        public void Dislike_When_UserAlreadyLiked_Should_ReturnsSuccessResultWithNewDislike()
        {
            _review.Like(_user);

            var result = _review.Dislike(_user);

            Assert.Equal(Result.Sucess, result);
            Assert.Single(_review.Dislikes);
            Assert.Empty(_review.Likes);
        }

        [Fact]
        public void Dislike_When_ReviewIsNew_Should_ReturnsSuccessResult()
        {
            var result = _review.Dislike(_user);

            Assert.Equal(Result.Sucess, result);
            Assert.Single(_review.Dislikes);
            Assert.Empty(_review.Likes);
        }

        [Fact]
        public void Delete_When_UserOwnsReview_Should_ReturnsSuccessResultAndIsNotActive()
        {
            var result = _review.Delete(_user);

            Assert.Equal(Result.Sucess, result);
            Assert.False(_review.IsActive);
        }

        [Fact]
        public void Delete_When_UserDoesNotOwnReview_Should_ReturnsUserCannotDeleteResultAndIsActive()
        {
            var result = _review.Delete(new User("NEWUSER"));

            Assert.Equal(Result.UserCannotDelete, result);
            Assert.True(_review.IsActive);
        }
    }
}
