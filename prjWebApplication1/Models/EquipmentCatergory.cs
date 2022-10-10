using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class EquipmentCatergory
    {
        public EquipmentCatergory()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int EquipmentCatergoryId { get; set; }
        public string EquipmentCatergoryName { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
