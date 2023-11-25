namespace OfficeAdmin_API.Models.Response
{
    public class Response<T>
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
