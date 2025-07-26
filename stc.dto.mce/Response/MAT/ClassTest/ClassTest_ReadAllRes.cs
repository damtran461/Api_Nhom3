using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response.MAT.Class_Test
{
    public class ClassTest_ReadAllRes
    {
        public int ClassTestID { get;set; }
        public int ClassID { get;set; }
        public int ExamID { get;set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string ClassName { get; set; }   
        public string TestName { get; set; }
        public string ClassCode { get; set; }
        public string TestCode { get; set; }    
        public List<MemberInfo> Members { get; set; } = new List<MemberInfo>();
    }
    public class MemberInfo
    {
        public int MemberID { get; set; }
        public string FullName { get; set; }
        public string MemberCode { get; set; }
        public bool IsQualified { get; set; }
    }
}
