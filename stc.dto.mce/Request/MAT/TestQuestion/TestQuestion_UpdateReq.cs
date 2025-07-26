using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request
{
    public class TestQuestion_UpdateReq
    {
        public int TestQuestionID { get; set; }
        public int QuestionID { get; set; }
        public int TestId { get; set; }
        public bool IsActived { get; set; }

    }
}
