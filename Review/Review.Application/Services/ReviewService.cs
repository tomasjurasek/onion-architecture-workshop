using Microsoft.Extensions.Logging;
using Review.Application.Enums;
using Review.Domain;
using Review.Domain.Services;

namespace Review.Application.Services
{
    internal class ReviewService : IReviewService
    {
        private readonly IReviewCollection _reviewCollection;
        private readonly ILogger<ReviewService> _logger;
        private readonly IMetric _metric;

        public ReviewService(IReviewCollection reviewCollection, ILogger<ReviewService> logger, IMetric metric)
        {
            _reviewCollection = reviewCollection;
            _logger = logger;
            _metric = metric;
        }

        public async Task<ReviewOperationResult> Dislike(Guid reviewId, Guid userId)
        {
            try
            {
                var review = await _reviewCollection.GetAsync(reviewId);
                var result = review.Dislike(userId);
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

        public async Task<ReviewOperationResult> Like(Guid reviewId, Guid userId)
        {
            try
            {
                var review = await _reviewCollection.GetAsync(reviewId);
                var result = review.Like(userId);
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

        public async Task<ReviewOperationResult> Store(Guid productId, Guid userId, string description)
        {
            try
            {
                var review = Domain.Models.Review.Create(productId, userId, description);

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
