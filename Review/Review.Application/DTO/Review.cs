namespace Review.Application.DTO
{
    public record Review (Guid Id, Guid UserId, string Description, int Likes, int Dislikes); // TODO Validation
}
