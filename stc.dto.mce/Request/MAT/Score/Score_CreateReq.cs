using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request.MAT.Score
{
    public class Score_CreateReq
    {
        public int ExamTestID { get; set; }
        public int MemberID { get; set; }
        public float Score { get; set; }
        public bool IsActived { get; set; }
    }
}
