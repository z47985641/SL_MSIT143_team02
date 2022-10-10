using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class Admin
    {
        public Admin()
        {
            AdminReferences = new HashSet<AdminReference>();
        }

        public int AdminId { get; set; }
        public int MemberId { get; set; }
        public string AdminInfo { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<AdminReference> AdminReferences { get; set; }
    }
}
