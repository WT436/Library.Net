using System;
using System.Collections.Generic;
using System.Text;

namespace Office.ExcelNpoi.Dtos
{
    public class ExcelExportSheetConfig<T>
    {
        public string SheetName { get; set; }
        public List<T> SheetDatas { get; set; }
        public int RowStart { get; set; }
        public int ColumnStart { get; set; }
        public List<ExcelExportCellConfig> CellConfig { get; set; }
        public string[] HeaderLabel { get; set; }
    }
}
