﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response.MAT.Member
{
    public  class Member_ReadAllRes
    {
        public int MemberID { get; set; }
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birtdate { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public bool IsActived { get; set; }
        public string Gender { get;set; }

    }
}
