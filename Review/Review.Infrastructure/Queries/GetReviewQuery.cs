using Review.Application.Conracts;

namespace Review.Infrastructure.Queries
{
    internal class GetReviewQUery : IGetReviewsQuery
    {
        public Task<ICollection<Application.DTO.Review>> Get(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
