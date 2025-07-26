using System.Collections.Generic;

namespace stc.dto.mce.Common
{
    public class ApiPagingResult<T>
    {
        public int total_record { get; set; }

        public int page_index { get; set; }

        public int page_size { get; set; }

        public IEnumerable<T> records { get; set; }
    }
}
