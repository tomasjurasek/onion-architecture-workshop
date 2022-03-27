using Review.Application.Enums;

namespace Review.Application.Services
{
    public interface IReviewService
    {
        Task<ReviewOperationResult> Store(Guid productId, Guid userId, string description);
        Task<ReviewOperationResult> Like(Guid reviewId, Guid userId);
        Task<ReviewOperationResult> Dislike(Guid reviewId, Guid userId);
    }
}
