namespace OfficeAdmin_API.Models.Request
{
    public class CreateOfficeRequest
    {
        public int Code { get; set; }
        public string Identification { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? Currency { get; set; }
        public string Username { get; set; }
    }
}
