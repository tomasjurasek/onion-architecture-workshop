namespace Review.Application.Contracts
{
    public interface IReviewFacade
    {
        Task<ReviewOperationResult> InsertAsync(string productCode, string userName, string description);
        Task<ReviewOperationResult> LikeAsync(Guid reviewId, string userName);
        Task<ReviewOperationResult> DislikeAsync(Guid reviewId, string userName);
        Task<ReviewOperationResult> DeleteAsync(Guid reviewId, string userName);
    }
}
