using Microsoft.EntityFrameworkCore;
using Review.Domain.Contracts;
using Review.Infrastructure.WriteModel.Database;
using Review.Infrastructure.WriteModel.Database.Entities;
using Review.Infrastructure.WriteModel.Database.Mappings;

namespace Review.Infrastructure.WriteModel
{
    internal class ReviewRepository : IReviewRepository
    {
        private readonly IDbContextFactory<ReviewDbContext> _factory;

        public ReviewRepository(IDbContextFactory<ReviewDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<Domain.Entities.Review> GetAsync(Guid id)
        {
            using var context = _factory.CreateDbContext();

            var reviewDb = await context.Reviews
                    .Include(s => s.User)
                    .Include(s => s.Likes)
                    .ThenInclude(s => s.User)
                    .Include(s => s.Dislikes)
                    .ThenInclude(s => s.User)
                    .Include(s => s.Product)
                    .SingleAsync(s => s.ReviewId == id);

            return reviewDb.ToDomain();
        }

        public async Task UpsertAsync(Domain.Entities.Review review)
        {
            using var context = _factory.CreateDbContext();

            var reviewDb = await context.Reviews
                    .Include(s => s.User)
                    .Include(s => s.Likes)
                    .ThenInclude(s => s.User)
                    .Include(s => s.Dislikes)
                    .ThenInclude(s => s.User)
                    .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.ReviewId == review.Id) ?? review.ToDatabase();


            //LIKES
            var likesToAdd = review.Likes.Where(s => !reviewDb.Likes.Select(w => w.User.UserName).Contains(s.User.UserName));
            var likesToRemove = reviewDb.Likes.Where(s => !review.Likes.Select(w => w.User.UserName).Contains(s.User.UserName));
            foreach (var likeToAdd in likesToAdd)
            {
                reviewDb.Likes.Add(new Like
                {
                    CreatedAt = likeToAdd.CreatedAt,
                    User = reviewDb.User
                });
            }
            foreach (var likeToRemove in likesToRemove)
            {
                reviewDb.Likes.Remove(likeToRemove);
            }

            //DISLIKES
            var dislikesToAdd = review.Dislikes.Where(s => !reviewDb.Dislikes.Select(w => w.User.UserName).Contains(s.User.UserName));
            var dislikesToRemove = reviewDb.Dislikes.Where(s => !review.Dislikes.Select(w => w.User.UserName).Contains(s.User.UserName));
            foreach (var dislikeToAdd in dislikesToAdd)
            {
                reviewDb.Dislikes.Add(new Dislike
                {
                    CreatedAt = dislikeToAdd.CreatedAt,
                    User = reviewDb.User
                });
            }
            foreach (var dislikeToRemove in dislikesToRemove)
            {
                reviewDb.Dislikes.Remove(dislikeToRemove);
            }

            context.ReviewsRefresh.Add(new ReviewRefresh
            {
                ReviewId = reviewDb.ReviewId
            });

            context.Reviews.Attach(reviewDb);

            await context.SaveChangesAsync();

        }
    }
}
