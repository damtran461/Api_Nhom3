using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response.MAT.Questions
{
    public class Questions_ReadAllRes
    {
        public int QuestionID { get; set; }
        public string QuestionDropdownName { get; set; }
        public string SubjectName { get; set; }
        public int SubjectsId { get; set; }
        public string QuestionTypeName { get; set; }
        public int QuestionTypeID { get; set; }
        public string QuestionCode { get; set; }
        public string QuestionText { get; set; }
        public string QuestionImage { get; set; }
        public int NumberOption { get; set; }
        public bool IsActive { get; set; }
    }
}
