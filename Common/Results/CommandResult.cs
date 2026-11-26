namespace library_v3.Common.Results
{
    public record CommandResult(bool Success, string? ErrorMessage);

    public record CommandResult<T>(bool Success, T? Data, string? ErrorMessage);

}
