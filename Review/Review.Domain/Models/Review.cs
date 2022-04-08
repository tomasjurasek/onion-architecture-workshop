namespace Review.Domain.Models
{
    internal class Review
    {
        public static Review Create(Guid id, Guid productId, Guid userId, string descrption, IList<Like> likes, IList<Dislike> dislikes, DateTime createdAt) => new(id, productId, userId, descrption, likes, dislikes, createdAt);
        public static Review Create(Guid productId, Guid userId, string descrption) => new(Guid.NewGuid(), productId, userId, descrption, Array.Empty<Like>(), Array.Empty<Dislike>(), DateTime.UtcNow);

        public Guid Id { get; }
        public Guid ProductId { get; }
        public Guid UserId { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }
        public IEnumerable<Like> Likes => _likes.AsEnumerable();
        public IEnumerable<Dislike> Dislikes => _dislikes.AsEnumerable();

        private IList<Like> _likes;
        private IList<Dislike> _dislikes;
        public bool IsActive { get; private set; }


        public Review(Guid id, Guid productId, Guid userId, string description, IList<Like> likes, IList<Dislike> dislikes, DateTime createdAt)
        {
            //TODO Validation
            Id = id;
            ProductId = productId;
            UserId = userId;
            Description = description;
            CreatedAt = createdAt;
            _likes = likes;
            _dislikes = dislikes;
            IsActive = true; // TODO input
        }


        public Result Like(Guid userId)
        {
            if (Likes.Any(s => s.UserId.Equals(userId)))
            {
                return Result.AlreadyLiked;
            }

            var dislike = _dislikes.FirstOrDefault(s => s.UserId.Equals(userId));
            if (dislike is not null)
            {
                _dislikes.Remove(dislike);
            }

            _likes.Add(new Like(userId, DateTime.UtcNow));

            return Result.Sucess;
        }

        public Result Dislike(Guid userId)
        {
            if (Dislikes.Any(s => s.UserId.Equals(userId)))
            {
                return Result.AlredyDisliked;
            }

            var like = _likes.FirstOrDefault(s => s.UserId.Equals(userId));
            if (like is not null)
            {
                _likes.Remove(like);
            }

            _dislikes.Add(new Dislike(userId, DateTime.UtcNow));

            return Result.Sucess;
        }

        public Result Delete(Guid userId)
        {
            if (!UserId.Equals(userId))
            {
                return Result.UserCannotDelete;
            }

            IsActive = false;
            return Result.Sucess;
        }
    }
}
