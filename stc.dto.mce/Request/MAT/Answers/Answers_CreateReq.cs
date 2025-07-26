using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request.MAT.Answers
{
    public class Answers_CreateReq
    {
        public int QuestionID { get; set; }
        public string AnswerName { get; set; }
        public string AnswerCode { get; set; }
        public bool IsTrue { get; set; }
        public bool IsActive { get; set; }
        public int UpdateUserID { get; set; }
    }
}
