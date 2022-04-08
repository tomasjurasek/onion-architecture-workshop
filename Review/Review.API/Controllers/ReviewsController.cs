using Microsoft.AspNetCore.Mvc;
using Review.API.DTO;
using Review.Application.Services;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {

        private readonly ILogger<ReviewsController> _logger;
        private readonly IReviewFacade _reviewService;
        private readonly ILoggedUserProvider _loggedUserProvider;

        public ReviewsController(ILogger<ReviewsController> logger, IReviewFacade reviewService, ILoggedUserProvider loggedUserProvider)
        {
            _logger = logger;
            _reviewService = reviewService;
            _loggedUserProvider = loggedUserProvider;
        }

        [HttpPost]
        public async Task<IActionResult> StoreAsync([FromBody] StoreReviewRequest model)
        {
            var result = await _reviewService.StoreAsync(model.ProductId, _loggedUserProvider.UserId, model.Description);
            if (result == Application.Enums.ReviewOperationResult.Success)
            {
                return Ok();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteAsync(Guid reviewId)
        {
            var result = await _reviewService.DeleteAsync(reviewId, _loggedUserProvider.UserId);
            if (result == Application.Enums.ReviewOperationResult.Success)
            {
                return Ok();
            }
            else if (result == Application.Enums.ReviewOperationResult.Error)
            {
                return BadRequest();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };
        }


        [HttpPut("{reviewId/like}")]
        public async Task<IActionResult> LikeAsync(Guid reviewId)
        {
            var result = await _reviewService.LikeAsync(reviewId, _loggedUserProvider.UserId);
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
        public async Task<IActionResult> DislikeAsync(Guid reviewId)
        {
            var result = await _reviewService.DislikeAsync(reviewId, _loggedUserProvider.UserId);
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