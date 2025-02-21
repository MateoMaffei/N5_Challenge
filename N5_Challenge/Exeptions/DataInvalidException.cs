namespace N5_Challenge.Exeptions
{
    public class ForbidenException : Exception
    {
        public int StatusCode { get; set; }
        public ForbidenException(string message, int statusCode = 403) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
