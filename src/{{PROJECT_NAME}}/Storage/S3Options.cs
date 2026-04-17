namespace {{PROJECT_NAME}}.Storage;

public sealed class S3Options
{
    public const string SectionName = "S3";

    public string? Endpoint { get; init; }
    public string Region { get; init; } = "auto";
    public string? AccessKeyId { get; init; }
    public string? SecretAccessKey { get; init; }
    public string? Bucket { get; init; }
    public bool ForcePathStyle { get; init; } = true;
    public string? PublicBaseUrl { get; init; }

    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(Endpoint)
        && !string.IsNullOrWhiteSpace(AccessKeyId)
        && !string.IsNullOrWhiteSpace(SecretAccessKey)
        && !string.IsNullOrWhiteSpace(Bucket);
}
