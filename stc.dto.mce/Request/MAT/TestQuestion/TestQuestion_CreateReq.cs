using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request
{
    public class TestQuestion_CreateReq
    {
        public int QuestionID { get; set; }
        public int TestId { get; set; }
        public bool IsActived { get; set; }
    }
}
