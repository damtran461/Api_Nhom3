using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response.MAT.Answers
{
    public class Answers_ReadAllRes
    {
        public int AnswerID { get; set; }
        public string AnswerDropdownName { get; set; }
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string AnswerName { get; set; }
        public string AnswerCode { get; set; }
        public bool IsTrue { get; set; }
        public bool IsActive { get; set; }
    }
}
