namespace OfficeAdmin_API.Models.Request
{
    public class CreateCurrencyRequest
    {
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public string Username { get; set; }
    }
}
