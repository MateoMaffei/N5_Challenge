namespace N5_Challenge.Exeptions
{
    public class UncontroledException : Exception
    {
        public int StatusCode { get; set; }

        public UncontroledException(string message, int statusCode = 400) : base (message)
        {
            StatusCode = statusCode;
        }
    }
}
