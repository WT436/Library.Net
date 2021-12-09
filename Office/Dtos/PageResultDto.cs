using System.Collections.Generic;

namespace Office.Dtos
{
    public class PageResultDto<T>
    {
        public List<T> items { get; set; }

        public int totalCount { get; set; }
    }
}
