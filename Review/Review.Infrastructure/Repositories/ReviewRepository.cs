using Review.Domain.Contracts;

namespace Review.Infrastructure.Repositories
{
    internal class ReviewRepository : IReviewRepositories
    {
        public Task<Domain.Entities.Review> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpsertAsync(Domain.Entities.Review review)
        {
            throw new NotImplementedException();
        }
    }
}
