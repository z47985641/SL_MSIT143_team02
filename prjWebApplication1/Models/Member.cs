using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class Member
    {
        public Member()
        {
            Admins = new HashSet<Admin>();
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string MemberAccount { get; set; }
        public string MemberPassword { get; set; }
        public string MemberName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string MemberPhone { get; set; }
        public string MemberEmail { get; set; }
        public string CityName { get; set; }
        public byte[] MemberImage { get; set; }
        public string Authority { get; set; }

       // public virtual City City { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
