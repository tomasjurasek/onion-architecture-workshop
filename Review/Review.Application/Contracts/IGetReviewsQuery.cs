namespace Review.Application.Conracts
{
    internal interface IGetReviewsQuery
    {
        Task<ICollection<DTO.Review>> Get(Guid productId);
    }
}
