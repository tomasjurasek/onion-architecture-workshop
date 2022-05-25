using Microsoft.Extensions.Logging;
using Review.Application.Contracts;
using Review.Domain;
using Review.Domain.Contracts;
using Review.Domain.Entities;

namespace Review.Application.Services
{
    internal class ReviewFacade : IReviewFacade
    {
        private readonly IReviewRepository _reviewCollection;
        private readonly ILogger<ReviewFacade> _logger;
        private readonly IMetricCollector _metric;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ReviewFacade(IReviewRepository reviewCollection,
            ILogger<ReviewFacade> logger,
            IMetricCollector metric,
            IDateTimeProvider dateTimeProvider)
        {
            _reviewCollection = reviewCollection;
            _logger = logger;
            _metric = metric;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ReviewOperationResult> DeleteAsync(Guid reviewId, string userName)
        {
            try
            {
                var review = await _reviewCollection.GetAsync(reviewId);
                var result = review.Delete(new User(userName));
                if (result == Result.Sucess)
                {
                    await _reviewCollection.UpsertAsync(review);
                    _metric.Track("Review:Deleted");
                    return ReviewOperationResult.Success;
                }
                else if (result == Result.UserCannotDelete)
                {
                    return ReviewOperationResult.Error;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null);

            }

            return ReviewOperationResult.Error;
        }

        public async Task<ReviewOperationResult> DislikeAsync(Guid reviewId, string userName)
        {
            try
            {
                var review = await _reviewCollection.GetAsync(reviewId);
                var result = review.Dislike(new User(userName));
                if (result == Result.Sucess)
                {
                    await _reviewCollection.UpsertAsync(review);
                    _metric.Track("Review:Disliked");
                    return ReviewOperationResult.Success;
                }
                else if (result == Result.AlredyDisliked)
                {
                    return ReviewOperationResult.AlreadyDisliked;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null);

            }

            return ReviewOperationResult.Error;
        }

        public async Task<ReviewOperationResult> LikeAsync(Guid reviewId, string userName)
        {
            try
            {
                var review = await _reviewCollection.GetAsync(reviewId);
                var result = review.Like(new User(userName));
                if (result == Result.Sucess)
                {
                    await _reviewCollection.UpsertAsync(review);
                    _metric.Track("Review:Liked");
                    return ReviewOperationResult.Success;
                }
                else if (result == Result.AlreadyLiked)
                {
                    return ReviewOperationResult.AlreadyDisliked;
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, null);
            }

            return ReviewOperationResult.Error;
        }

        public async Task<ReviewOperationResult> InsertAsync(string productCode, string userName, string description)
        {
            try
            {
                var review = new Domain.Entities.Review(
                    Guid.NewGuid(),
                    new Product(productCode),
                    new User(userName),
                    description,
                    Array.Empty<Like>(),
                    Array.Empty<Dislike>(),
                    _dateTimeProvider.Utc,
                    true);

                await _reviewCollection.UpsertAsync(review);
                _metric.Track("Review:Stored");
                return ReviewOperationResult.Success;
            }
            catch (Exception ex)
            {
                _metric.Track("Review:StoreFailed");
                _logger.LogError(ex, null);
                return ReviewOperationResult.Error;
            }
        }
    }
}
