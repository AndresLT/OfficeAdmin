using System;
using System.Collections.Generic;

namespace OfficeAdmin_API.Models
{
    public partial class Office
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Identification { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? Currency { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? ModifyUser { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual User? CreateUserNavigation { get; set; }
        public virtual Currency? CurrencyNavigation { get; set; }
        public virtual User? ModifyUserNavigation { get; set; }
    }
}
