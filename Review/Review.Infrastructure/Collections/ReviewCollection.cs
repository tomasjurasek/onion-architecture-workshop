﻿using Review.Domain.Services;

namespace Review.Infrastructure.Collections
{
    internal class ReviewCollection : IReviewCollection
    {
        public Task<Domain.Models.Review> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpsertAsync(Domain.Models.Review review)
        {
            throw new NotImplementedException();
        }
    }
}
