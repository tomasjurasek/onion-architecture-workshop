using Microsoft.AspNetCore.Mvc;
using Review.API.DTO;
using Review.Application.Contracts;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("products/{code}")]
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

        [HttpPost("reviews")]
        public async Task<IActionResult> InsertAsync(string code, [FromBody] ReviewInsertRequest model)
        {
            var result = await _reviewService.InsertAsync(code, _loggedUserProvider.UserName, model.Description);
            if (result == ReviewOperationResult.Success)
            {
                return Ok();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };
        }

        [HttpDelete("reviews/{reviewId}")]
        public async Task<IActionResult> DeleteAsync(Guid reviewId)
        {
            var result = await _reviewService.DeleteAsync(reviewId, _loggedUserProvider.UserName);
            if (result == ReviewOperationResult.Success)
            {
                return Ok();
            }
            else if (result == ReviewOperationResult.Error)
            {
                return BadRequest();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };
        }


        [HttpPut("reviews/{reviewId}/like")]
        public async Task<IActionResult> LikeAsync(Guid reviewId)
        {
            var result = await _reviewService.LikeAsync(reviewId, _loggedUserProvider.UserName);
            if (result == ReviewOperationResult.Success)
            {
                return Ok();
            }
            else if (result == ReviewOperationResult.AlreadyLiked)
            {
                return BadRequest();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };
        }

        [HttpPut("reviews/{reviewId}/dislike")]
        public async Task<IActionResult> DislikeAsync(Guid reviewId)
        {
            var result = await _reviewService.DislikeAsync(reviewId, _loggedUserProvider.UserName);
            if (result == ReviewOperationResult.Success)
            {
                return Ok();
            }
            else if (result == ReviewOperationResult.AlreadyDisliked)
            {
                return BadRequest();
            }

            return new ContentResult { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}