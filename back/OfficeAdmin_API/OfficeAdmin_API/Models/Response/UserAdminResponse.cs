namespace OfficeAdmin_API.Models.Response
{
    public class UserAdminResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public bool? Active { get; set; }
    }
}
