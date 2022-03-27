namespace Review.Domain.Models
{
    internal record Review
    {
        public static Review Create(Guid id, Guid productId, Guid userId, string descrption, DateTime createdAt) => new(id, productId, userId, descrption, createdAt);
        public static Review Create(Guid productId, Guid userId, string descrption) => new(Guid.NewGuid(), productId, userId, descrption, DateTime.UtcNow);

        public Guid Id { get; }
        public Guid ProductId { get; }
        public Guid UserId { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }
        public ICollection<Like> Likes { get; private set; }
        public ICollection<Dislike> Dislikes { get; private set; }


        internal Review(Guid id, Guid productId, Guid userId, string description, DateTime createdAt)
        {
            Id = id;
            ProductId = productId;
            UserId = userId;
            Description = description;
            CreatedAt = createdAt;
            Likes = new List<Like>();
            Dislikes = new List<Dislike>();
        }


        internal Result Like(Guid userId)
        {
            if (Likes.Any(s => s.UserId.Equals(userId)))
            {
                return Result.AlreadyLiked;
            }

            Likes.Add(new Like(userId, DateTime.UtcNow));

            return Result.Sucess;
        }

        internal Result Dislike(Guid userId)
        {
            if (Dislikes.Any(s => s.UserId.Equals(userId)))
            {
                return Result.AlredyDisliked;
            }

            Dislikes.Add(new Dislike(userId, DateTime.UtcNow));

            return Result.Sucess;
        }
    }
}
