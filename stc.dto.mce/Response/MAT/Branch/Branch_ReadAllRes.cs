using System;

namespace stc.dto.mce.Response
{
    public class Branch_ReadAllRes
    {
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SubPhone_1 { get; set; }
        public string SubPhone_2 { get; set; }
        public string FTP { get; set; }
        public string TaxNo { get; set; }
        public bool IsCenter { get; set; }
        public bool IsAlow2Print { get; set; }
        public string AddressContact { get; set; }
        public string Map { get; set; }
        public int? LanguageID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
    }
}
