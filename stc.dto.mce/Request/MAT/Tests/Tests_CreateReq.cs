using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request
{
    public class Tests_CreateReq
    {
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int NumberQuestion { get; set; }
        public bool IsActived { get; set; }
    }
}
