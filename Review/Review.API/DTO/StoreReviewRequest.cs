using System.ComponentModel.DataAnnotations;

namespace Review.API.DTO
{
    public record StoreReviewRequest
    {
        [Required]
        public Guid ProductId { get; init; }

        [Required]
        public string Description { get; init; }
    }
}
