namespace Review.Application.Queries
{
    internal interface IGetReviewsQuery
    {
        Task<ICollection<Contracts.DTO.Review>> Get(Guid productId);
    }
}
