using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.dto.mce.Response
{
    public class Class_ReadAllRes
    {
        public int ClassID { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public int SubId { get; set; }
        public string SubjectName { get; set; }
        public int QuantityStudent { get; set; }
        public bool IsActived { get; set; }
    }
}
