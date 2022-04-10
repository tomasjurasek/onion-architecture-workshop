using Microsoft.Extensions.Logging;
using Review.Application.Contracts;
using Review.Application.Queries;
using Review.Domain;
using Review.Domain.Services;

namespace Review.Application.Services
{
    internal class ReviewFacade : IReviewFacade
    {
        private readonly IReviewRepositories _reviewCollection;
        private readonly ILogger<ReviewFacade> _logger;
        private readonly IMetric _metric;
        private readonly IGetReviewsQuery _getReviewsQuery;

        public ReviewFacade(IReviewRepositories reviewCollection,
            ILogger<ReviewFacade> logger,
            IMetric metric,
            IGetReviewsQuery getReviewsQuery)
        {
            _reviewCollection = reviewCollection;
            _logger = logger;
            _metric = metric;
            _getReviewsQuery = getReviewsQuery;
        }

        public Task<ICollection<Contracts.DTO.Review>> GetAsync(Guid productId) // TODO paging, logging,...
        {
            return _getReviewsQuery.Get(productId);
        }

        public async Task<ReviewOperationResult> DeleteAsync(Guid reviewId, Guid userId)
        {
            try
            {
                var review = await _reviewCollection.GetAsync(reviewId);
                var result = review.Delete(userId);
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

        public async Task<ReviewOperationResult> DislikeAsync(Guid reviewId, Guid userId)
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

        public async Task<ReviewOperationResult> LikeAsync(Guid reviewId, Guid userId)
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

        public async Task<ReviewOperationResult> StoreAsync(Guid productId, Guid userId, string description)
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
