using Microsoft.AspNetCore.Mvc;
using Review.Application.Services;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {

        private readonly ILogger<ReviewsController> _logger;
        private readonly IReviewService _reviewService;
        private readonly ILoggedUserProvider _loggedUserProvider;

        public ReviewsController(ILogger<ReviewsController> logger, IReviewService reviewService, ILoggedUserProvider loggedUserProvider)
        {
            _logger = logger;
            _reviewService = reviewService;
            _loggedUserProvider = loggedUserProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpPut("{reviewId/like}")]
        public async Task<IActionResult> Like(Guid reviewId)
        {
            var result = await _reviewService.Like(reviewId, _loggedUserProvider.UserId);
            if (result == Application.Enums.ReviewOperationResult.Success)
            {
                return Ok();
            }
            else if (result == Application.Enums.ReviewOperationResult.AlreadyLiked)
            {
                return BadRequest();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };
        }

        [HttpPut("{reviewId/dislike}")]
        public async Task<IActionResult> Dislike(Guid reviewId)
        {
            var result = await _reviewService.Dislike(reviewId, _loggedUserProvider.UserId);
            if (result == Application.Enums.ReviewOperationResult.Success)
            {
                return Ok();
            }
            else if (result == Application.Enums.ReviewOperationResult.AlreadyDisliked)
            {
                return BadRequest();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}