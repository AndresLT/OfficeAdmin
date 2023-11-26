namespace OfficeAdmin_API.Models.Request
{
    public class ModifyUserRequest
    {
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public bool? Active { get; set; }

    }
}
