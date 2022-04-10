namespace Review.Application.Contracts
{
    public enum ReviewOperationResult
    {
        Success = 0,
        AlreadyLiked = 1,
        AlreadyDisliked = 2,
        Error = 3
    }
}
