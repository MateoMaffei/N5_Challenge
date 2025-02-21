namespace N5_Challenge.Configuration.Options
{
    public class ElasticSearchOptions
    {
        public static string Section = "Application:ElasticSearch";
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IndexName { get; set; }
    }
}
