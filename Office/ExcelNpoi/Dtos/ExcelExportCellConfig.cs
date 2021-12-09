using System;
using System.Collections.Generic;
using System.Text;

namespace Office.ExcelNpoi.Dtos
{
    public class ExcelExportCellConfig
    {
        public string FieldName { get; set; }
        public string FormatString { get; set; }
        public bool ZeroToBlank { get; set; } = false;
        public bool StringUpper { get; set; } = false;
    }
}
