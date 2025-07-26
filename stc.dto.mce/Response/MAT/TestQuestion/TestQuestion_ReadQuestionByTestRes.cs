using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response
{
    public class TestQuestion_ReadQuestionByTestRes
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public bool IsActived { get; set; }
    }
}
