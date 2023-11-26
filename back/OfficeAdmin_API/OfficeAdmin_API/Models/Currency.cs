using System;
using System.Collections.Generic;

namespace OfficeAdmin_API.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Offices = new HashSet<Office>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Office> Offices { get; set; }
    }
}
