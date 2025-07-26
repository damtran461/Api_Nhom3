using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request.MAT.Subject
{
    public class Subject_CreateReq
    {
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public bool IsActived { get; set; }
    }
}
