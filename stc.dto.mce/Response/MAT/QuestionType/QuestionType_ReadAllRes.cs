using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response.MAT.QuestionType
{
    public class QuestionType_ReadAllRes
    {
        public int QuestionTypeId { get; set; }
        public string QuestionTypeCode { get; set; }
        public string QuestionTypeName { get; set; }
        public bool IsActived { get; set; }
    }
}
