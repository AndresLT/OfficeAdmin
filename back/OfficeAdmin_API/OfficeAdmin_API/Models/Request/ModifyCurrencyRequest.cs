namespace OfficeAdmin_API.Models.Request
{
    public class ModifyCurrencyRequest
    {
        public int id { get; set; }
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }
        public string Username { get; set; }
    }
}
