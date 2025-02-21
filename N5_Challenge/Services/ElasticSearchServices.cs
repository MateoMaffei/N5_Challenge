using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using N5_Challenge.Configuration.Options;
using N5_Challenge.Exeptions;
using N5_Challenge.Services.Interface;

namespace N5_Challenge.Services
{
    public class ElasticService : IElasticService
    {
        private readonly ElasticsearchClient _elasticClient;
        private readonly ILogger<IElasticService> _logger;
        private readonly ElasticSearchOptions _elasticSearchOptions;

        public ElasticService(ElasticSearchOptions elasticSearchOptions, ILogger<IElasticService> logger)
        {
            _elasticSearchOptions = elasticSearchOptions;
            _logger = logger;

            if (!string.IsNullOrEmpty(_elasticSearchOptions.Uri) && 
                !string.IsNullOrEmpty(_elasticSearchOptions.Username) && 
                !string.IsNullOrEmpty(_elasticSearchOptions.Password))
            {
                var settings = new ElasticsearchClientSettings(new Uri(_elasticSearchOptions.Uri))
                    .Authentication(new BasicAuthentication(_elasticSearchOptions.Username, _elasticSearchOptions.Password))
                    .RequestTimeout(TimeSpan.FromSeconds(30));
                _elasticClient = new ElasticsearchClient(settings);
            }
        }

        public async Task IndexDocumentAsync<T>(T document) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(_elasticSearchOptions.IndexName)) throw new Exception("[IndexNameError].[ElasticSearch]");
                var response = await _elasticClient.IndexAsync(document, idx => idx.Index(_elasticSearchOptions.IndexName));
                if (!response.IsValidResponse)
                {
                    throw new UncontroledException($"Error al indexar el documento: {response.ElasticsearchServerError?.Error?.Reason}");
                }
                _logger.LogInformation("Documento indexado correctamente.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
