namespace N5_Challenge.Configuration.Options
{
    public class KafkaOptions
    {
        public static string Section = "Application:Kafka";
        public string Topic { get; set; }
        public string Server { get; set; }
    }
}
