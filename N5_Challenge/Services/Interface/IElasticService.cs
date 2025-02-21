namespace N5_Challenge.Services.Interface
{
    public interface IElasticService
    {
        Task IndexDocumentAsync<T>(T document) where T : class;
    }
}
