namespace Review.Application.DTO
{
    public record Review
    {
        public Review(Guid id, Guid userId, string description, int likes, int dislikes)
        {
            // TODO validation
            Id = id;
            UserId = userId;
            Description = description;
            Likes = likes;
            Dislikes = dislikes;
        }

        public Guid Id { get; }
        public string Description { get; }
        public int Likes { get; }
        public int Dislikes { get; }
        public Guid UserId { get; }
    }
}
