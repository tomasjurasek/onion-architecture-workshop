using System.ComponentModel.DataAnnotations;

namespace Review.API.DTO
{
    public record ReviewRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
