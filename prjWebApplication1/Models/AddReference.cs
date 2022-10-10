using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class AddReference
    {
        public int AddId { get; set; }
        public int OrderId { get; set; }
        public int AddReferenceId { get; set; }

        public virtual Add Add { get; set; }
        public virtual Order Order { get; set; }
    }
}
