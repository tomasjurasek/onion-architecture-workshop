namespace Review.Application.Queries
{
    public interface IGetReviewsQuery
    {
        Task<ICollection<DTO.Review>> Get(Guid productId);
    }
}
