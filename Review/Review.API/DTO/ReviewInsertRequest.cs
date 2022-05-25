using System.ComponentModel.DataAnnotations;

namespace Review.API.DTO
{
    public record ReviewInsertRequest
    {
        [Required]
        public string Description { get; init; }
    }
}
