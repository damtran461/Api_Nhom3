using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response.MAT.ClassMember
{
    public class ClassMember_ReadAllRes
    {
        public int Class_Member_ID { get; set; }
        public int ClassID { get; set; }
        public int MemberID { get; set; }
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string ClassCode { get; set; }
        public bool IsQualified { get; set; }
        public bool IsActived { get; set; }
    }
}
