using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request.MAT.Questions
{
    public class Question_UpdateReq
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionImage { get; set; }
        public int NumberOption { get; set; }
        public bool IsActive { get; set; }
        public int UpdateUserID { get; set; }
    }
}
