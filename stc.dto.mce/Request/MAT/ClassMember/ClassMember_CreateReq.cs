using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request
{
    public class ClassMember_CreateReq
    {
        public int ClassID { get; set; }
        public int MemberID { get; set; }
        public bool IsQualified { get; set; }
        public bool IsActived {  get; set; }
    }
}
