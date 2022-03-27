namespace Review.Domain.Services
{
    internal interface IReviewCollection
    {
        Task<Models.Review> GetAsync(Guid id);
        Task UpsertAsync(Models.Review review);
    }
}
