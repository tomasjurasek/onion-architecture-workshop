using Dawn;

namespace Review.Domain.Entities
{
    internal record Dislike
    {
        public Dislike(User user, DateTime createdAt)
        {
            User = Guard.Argument(user).NotNull();
            CreatedAt = createdAt;
        }

        public User User { get; }
        public DateTime CreatedAt { get; }
    }
}
