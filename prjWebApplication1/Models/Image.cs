using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class Image
    {
        public Image()
        {
            ImageReferences = new HashSet<ImageReference>();
        }

        public int ImageId { get; set; }
        public byte[] Image1 { get; set; }
        public string ImageCaption { get; set; }

        public virtual ICollection<ImageReference> ImageReferences { get; set; }
    }
}
