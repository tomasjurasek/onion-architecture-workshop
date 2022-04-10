namespace Review.API.DTO
{
    public record ReviewResponse
    {
        public Guid Id { get; init; }
        public string Description { get; init; }
        public int Likes { get; init; }
        public int Dislikes { get; init; }
    }
}
