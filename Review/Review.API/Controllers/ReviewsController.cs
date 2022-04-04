using Microsoft.AspNetCore.Mvc;
using Review.API.DTO;
using Review.API.Mapper;
using Review.Application.Queries;
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
        private readonly IGetReviewsQuery _getReviewsQuery;

        public ReviewsController(ILogger<ReviewsController> logger, IReviewService reviewService,
            ILoggedUserProvider loggedUserProvider,
            IGetReviewsQuery getReviewsQuery)
        {
            _logger = logger;
            _reviewService = reviewService;
            _loggedUserProvider = loggedUserProvider;
            _getReviewsQuery = getReviewsQuery;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewRequest model)
        {
            var result = await _reviewService.Store(model.ProductId, _loggedUserProvider.UserId, model.Description);
            if (result == Application.Enums.ReviewOperationResult.Success)
            {
                return Ok();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };

        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> List(Guid productId) // TODO Paging, ...
        {
            var result = await _getReviewsQuery.Get(productId);
            return Ok(result.Select(s => s.Map()));
        }

        [HttpPut("{reviewId}/like")]
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

        [HttpPut("{reviewId}/dislike")]
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