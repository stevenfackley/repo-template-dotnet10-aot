using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace {{PROJECT_NAME}}.Storage;

public static class StorageExtensions
{
    /// <summary>
    /// Registers <see cref="IAmazonS3"/> when S3_* env vars are set (Cloudflare R2 / MinIO).
    /// No-op otherwise; features that require storage should check <see cref="S3Options.IsConfigured"/>.
    /// </summary>
    public static IServiceCollection AddS3Storage(this IServiceCollection services, IConfiguration config)
    {
        var opts = new S3Options
        {
            Endpoint = config["S3_ENDPOINT"],
            Region = config["S3_REGION"] ?? "auto",
            AccessKeyId = config["S3_ACCESS_KEY_ID"],
            SecretAccessKey = config["S3_SECRET_ACCESS_KEY"],
            Bucket = config["S3_BUCKET"],
            ForcePathStyle = config["S3_FORCE_PATH_STYLE"] == "true",
            PublicBaseUrl = config["S3_PUBLIC_BASE_URL"],
        };
        services.AddSingleton(opts);

        if (!opts.IsConfigured) return services;

        services.AddSingleton<IAmazonS3>(_ =>
        {
            var clientConfig = new AmazonS3Config
            {
                ServiceURL = opts.Endpoint,
                ForcePathStyle = opts.ForcePathStyle,
                AuthenticationRegion = opts.Region,
            };
            var creds = new BasicAWSCredentials(opts.AccessKeyId, opts.SecretAccessKey);
            return new AmazonS3Client(creds, clientConfig);
        });

        return services;
    }
}
