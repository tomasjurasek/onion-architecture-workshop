using Review.Domain.Entities;

namespace Review.Infrastructure.WriteModel.Database.Mappings
{
    internal static class DomainMapping
    {
        public static IList<Like> ToDomain(this ICollection<Entities.Like> likes)
        {
            return likes.Select(s => new Like(new User(s.User.UserName), s.CreatedAt)).ToArray();
        }

        public static IList<Dislike> ToDomain(this ICollection<Entities.Dislike> dislikes)
        {
            return dislikes.Select(s => new Dislike(new User(s.User.UserName), s.CreatedAt)).ToArray();
        }

        public static User ToDomain(this Entities.User user)
        {
            return new User(user.UserName);
        }

        public static Product ToDomain(this Entities.Product product)
        {
            return new Product(product.Code);
        }

        public static Domain.Entities.Review ToDomain(this Entities.Review review)
        {
            return new Domain.Entities.Review(review.ReviewId,
                review.Product.ToDomain(),
                review.User.ToDomain(),
                review.Description,
                review.Likes.ToDomain(),
                review.Dislikes.ToDomain(),
                review.CreatedAt,
                true);
        }
    }
}
