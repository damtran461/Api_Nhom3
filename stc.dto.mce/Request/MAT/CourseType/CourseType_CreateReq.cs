namespace stc.dto.mce.Request
{
    public class CourseType_CreateReq
    {
        public string CourseTypeCode { get; set; }
        public string CourseTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
    }
}
