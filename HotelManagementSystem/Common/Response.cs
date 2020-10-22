namespace Common {
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ExceptionError { get; set; }
        public string StackTrace { get; set; }
    }
}
