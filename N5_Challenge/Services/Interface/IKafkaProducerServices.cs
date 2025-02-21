using static N5_Challenge.Helper.Enums;

namespace N5_Challenge.Services.Interface
{
    public interface IKafkaProducerServices
    {
        Task EnviarEventoAsync(TipoOperacionesKafka mensaje);
    }
}
