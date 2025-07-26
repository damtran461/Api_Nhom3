namespace stc.dto.mce.Response
{
    public class CourseType_ReadAllRes
    {
        public int CourseTypeID { get; set; }
        public string CourseTypeCode { get; set; }
        public string CourseTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
    }
}
