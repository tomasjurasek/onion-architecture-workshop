namespace Review.Domain.Contracts
{
    internal interface IReviewRepositories
    {
        Task<Entities.Review> GetAsync(Guid id);
        Task UpsertAsync(Entities.Review review);
    }
}
