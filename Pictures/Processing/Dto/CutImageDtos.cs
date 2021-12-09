using System;
using System.Collections.Generic;
using System.Text;

namespace Pictures.Processing.Dto
{
    public class CutImageDtos : ResizeImageDto
    {
        public List<ListPoints> listPoints { get; set; }
    }
}
