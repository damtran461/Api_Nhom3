using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response.MAT.Subject
{
    public class Subject_ReadAllRes
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public bool IsActived { get; set; }
    }
}
