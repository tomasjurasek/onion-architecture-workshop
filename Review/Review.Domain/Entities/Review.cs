using Dawn;

namespace Review.Domain.Entities;

internal class Review
{
    public Review(
        Guid id,
        Product product,
        User user,
        string description,
        IList<Like> likes,
        IList<Dislike> dislikes,
        DateTime createdAt,
        bool isActive)
    {
        Id = Guard.Argument(id).NotDefault();
        Product = Guard.Argument(product).NotNull();
        User = Guard.Argument(user).NotNull();
        Description = Guard.Argument(description).NotNull().NotEmpty();
        _likes = Guard.Argument(likes).NotNull().Value;
        _dislikes = Guard.Argument(dislikes).NotNull().Value;
        IsActive = isActive;
        CreatedAt = createdAt;
    }


    public Guid Id { get; }
    public Product Product { get; }
    public User User { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; }
    public IEnumerable<Like> Likes => _likes.AsEnumerable();
    public IEnumerable<Dislike> Dislikes => _dislikes.AsEnumerable();

    private IList<Like> _likes;
    private IList<Dislike> _dislikes;
    public bool IsActive { get; private set; }


    public Result Like(User user)
    {
        if (Likes.Any(s => s.User.Equals(user)))
        {
            return Result.AlreadyLiked;
        }

        var dislike = _dislikes.FirstOrDefault(s => s.User.Equals(user));
        if (dislike is not null)
        {
            _dislikes.Remove(dislike);
        }

        _likes.Add(new Like(user, DateTime.UtcNow));

        return Result.Sucess;
    }

    public Result Dislike(User user)
    {
        if (Dislikes.Any(s => s.User.Equals(user)))
        {
            return Result.AlredyDisliked;
        }

        var like = _likes.FirstOrDefault(s => s.User.Equals(user));
        if (like is not null)
        {
            _likes.Remove(like);
        }

        _dislikes.Add(new Dislike(user, DateTime.UtcNow));

        return Result.Sucess;
    }

    public Result Delete(User user)
    {
        if (!User.Equals(user))
        {
            return Result.UserCannotDelete;
        }

        IsActive = false;
        return Result.Sucess;
    }
}

