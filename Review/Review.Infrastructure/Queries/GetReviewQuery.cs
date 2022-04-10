using Review.Application.Queries;

namespace Review.Infrastructure.Queries
{
    internal class GetReviewQUery : IGetReviewsQuery
    {
        public Task<ICollection<Application.Contracts.DTO.Review>> Get(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
