using System.Collections.Generic;

namespace stc.dto.mce.Common
{
    public class ExcelTemplateAdditionalSheetInfo
    {
        public bool IsUseValidate { get; set; } = true;
        public string SheetName { get; set; }

        public List<string> Headers { get; set; }

        public string RankList { get; set; }

        public List<ExcelAdditionalSheetDataModel> Data { get; set; }
    }
}
