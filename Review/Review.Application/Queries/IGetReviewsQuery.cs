namespace Review.Application.Queries
{
    internal interface IGetReviewsQuery
    {
        Task<ICollection<DTO.Review>> Get(Guid productId);
    }
}
