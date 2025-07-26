using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Request
{
    public class Class_UpdateReq
    {
        public int ClassID { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public int SubId { get; set; }
        public int QuantityStudent { get; set; }
        public bool IsActived { get; set; }
    }
}
