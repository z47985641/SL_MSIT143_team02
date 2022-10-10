using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class Add
    {
        public Add()
        {
            AddReferences = new HashSet<AddReference>();
        }

        public int AddId { get; set; }
        public string AddName { get; set; }
        public decimal AddPrice { get; set; }
        public int AddCategoryId { get; set; }

        public virtual AddCatergory AddCategory { get; set; }
        public virtual ICollection<AddReference> AddReferences { get; set; }
    }
}
