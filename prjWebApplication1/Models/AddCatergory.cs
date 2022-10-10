using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class AddCatergory
    {
        public AddCatergory()
        {
            Adds = new HashSet<Add>();
        }

        public int AddCatergoryId { get; set; }
        public string AddCatergoryName { get; set; }

        public virtual ICollection<Add> Adds { get; set; }
    }
}
