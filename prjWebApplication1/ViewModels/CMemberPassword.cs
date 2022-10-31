using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.ViewModels
{
    public class CMemberPassword
    {
        private Member _member;
        public CMemberPassword()
        {
            _member = new Member();

        }
        public Member member
        {
            get { return _member; }
            set { _member = value; }
        }
        public int MemberId
        {
            get { return _member.MemberId; }
            set { _member.MemberId = value; }
        }
        public string MemberAccount
        {
            get { return _member.MemberAccount; }
            set { _member.MemberAccount = value; }
        }
        public string MemberPassword
        {
            get { return _member.MemberPassword; }
            set { _member.MemberPassword = value; }
        }
        
        public string MemberNewPassword
        {
            get;
            set;
        }
        public string MemberName
        {
            get { return _member.MemberName; }
            set { _member.MemberName = value; }
        }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime BirthDate
        {
            get { return _member.BirthDate; }
            set { _member.BirthDate = value; }
        }
        public string MemberPhone
        {
            get { return _member.MemberPhone; }
            set { _member.MemberPhone = value; }
        }
        public string MemberEmail
        {
            get { return _member.MemberEmail; }
            set { _member.MemberEmail = value; }
        }
        public string CityName
        {
            get { return _member.CityName; }
            set { _member.CityName = value; }
        }
        public byte[] MemberImage
        {
            get { return _member.MemberImage; }
            set { _member.MemberImage = value; }
        }
        public string Authority
        {
            get { return _member.Authority; }
            set { _member.Authority = value; }
        }
    }
}
