namespace Review.Domain.Contracts
{
    internal interface IReviewRepository
    {
        Task<Entities.Review> GetAsync(Guid id);
        Task UpsertAsync(Entities.Review review);
    }
}
