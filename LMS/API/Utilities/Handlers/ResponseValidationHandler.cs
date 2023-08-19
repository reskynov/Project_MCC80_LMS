namespace API.Utilities.Handlers
{
    public class ResponseValidationHandler<TEntity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
    }
}
