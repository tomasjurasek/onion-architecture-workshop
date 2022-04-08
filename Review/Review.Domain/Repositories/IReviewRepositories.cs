namespace Review.Domain.Services
{
    internal interface IReviewRepositories
    {
        Task<Models.Review> GetAsync(Guid id);
        Task UpsertAsync(Models.Review review);
    }
}
