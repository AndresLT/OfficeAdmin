using System;
using System.Collections.Generic;

namespace OfficeAdmin_API.Models
{
    public partial class User
    {
        public User()
        {
            Logs = new HashSet<Log>();
            OfficeCreateUserNavigations = new HashSet<Office>();
            OfficeModifyUserNavigations = new HashSet<Office>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public bool? Active { get; set; }
        public int Attemps { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Office> OfficeCreateUserNavigations { get; set; }
        public virtual ICollection<Office> OfficeModifyUserNavigations { get; set; }
    }
}
