using Review.API.DTO;

namespace Review.API.Mapper
{
    public static class ReviewMapper
    {
        public static ReviewResponse Map(this Application.DTO.Review dto) => new()
        {
            Description = dto.Description,
            Dislikes = dto.Dislikes,
            Id = dto.Id,
            Likes = dto.Likes
        };
    }
}
