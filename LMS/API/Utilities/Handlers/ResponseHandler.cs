namespace API.Utilities.Handlers
{
    public class ResponseHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public TEntity? Data { get; set; }
    }
}
