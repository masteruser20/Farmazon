using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Farmazon.Shared
{
    public class ProductProvider : IProductProvider
    {
        private readonly IRetryPolicy retryPolicy;
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private readonly AsyncRetryPolicy policy;

        public ProductProvider(IRetryPolicy retryPolicy,
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger logger)
        {
            retryPolicy = retryPolicy;
            httpClient = httpClient;
            configuration = configuration;
            logger = logger;
            
            policy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2),
                    (exception, _, _, _) =>
                    {
                        logger.LogWarning(exception, "Error");
                    });
        }

        public async Task<bool> ProductExistsAsync(Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                return await policy.ExecuteAsync(async () =>
                {
                    var response = await httpClient.GetAsync(
                        $"{configuration.GetSection("URL")}/api/v1/products/{productId}/exists",
                        cancellationToken);

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return false;
                    }

                    response.EnsureSuccessStatusCode();
                    return true;
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking if product {ProductId} exists", productId);
                throw new InvalidOperationException("Error checking if product exists", ex);
            }
        }

        public async Task<int> GetProductStockAsync(Guid productId, CancellationToken cancellationToken)
        {
            return 5;
        }

        public async Task<string> GetProductInfoAsync(Guid productId, CancellationToken cancellationToken)
        {
            return "";
        }
    }
}