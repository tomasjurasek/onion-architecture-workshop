using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Review.Application
{
    internal class ReviewUpdater : IReviewUpdater
    {
        public ReviewUpdater()
        {

        }

        public Task UpdateAsync()
        {
            return Task.CompletedTask;
        }
    }

    public interface IReviewUpdater
    {
        Task UpdateAsync();
    }
}
