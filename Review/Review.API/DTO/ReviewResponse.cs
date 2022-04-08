namespace Review.API.DTO
{
    public record ReviewResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
