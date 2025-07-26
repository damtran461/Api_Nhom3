using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request.MAT.ClassTest
{
    public class ClassTest_CreateReq
    {
        public int ClassID {  get; set; }
        public int ExamID { get; set; }
        public bool IsActive { get; set; }
    }
}
