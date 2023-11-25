using System;
using System.Collections.Generic;

namespace OfficeAdmin_API.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime LogDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
