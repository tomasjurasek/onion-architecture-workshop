using Review.Application.Enums;

namespace Review.Application.Services
{
    public interface IReviewFacade
    {
        Task<ICollection<DTO.Review>> GetAsync(Guid productId);
        Task<ReviewOperationResult> StoreAsync(Guid productId, Guid userId, string description);
        Task<ReviewOperationResult> LikeAsync(Guid reviewId, Guid userId);
        Task<ReviewOperationResult> DislikeAsync(Guid reviewId, Guid userId);
        Task<ReviewOperationResult> DeleteAsync(Guid reviewId, Guid userId);
    }
}
