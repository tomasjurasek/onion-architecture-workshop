using Review.Domain.Services;

namespace Review.Infrastructure.Collections
{
    internal class ReviewCollection : IReviewCollection
    {
        public ReviewCollection(ReviewRefresher reviewRefresher)
        {
            Task.Factory.StartNew(async () => await reviewRefresher.StartAsync(), TaskCreationOptions.LongRunning);
        }

        public Task<Domain.Models.Review> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpsertAsync(Domain.Models.Review review)
        {
            throw new NotImplementedException();
        }
    }
}
