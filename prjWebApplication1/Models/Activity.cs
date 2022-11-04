using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class Activity
    {
        public Activity()
        {
            ActivityReferences = new HashSet<ActivityReference>();
        }

        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string ActivityInfo { get; set; }
        public int ActivityCapacity { get; set; }
        public string ActivityStatus { get; set; }
        public string ActivityLocation { get; set; }
        public decimal? ActivityPrice { get; set; }

        public virtual ICollection<ActivityReference> ActivityReferences { get; set; }
    }
}
