﻿namespace OfficeAdmin_API.Models.Request
{
    public class CreateUserRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
    }
}
