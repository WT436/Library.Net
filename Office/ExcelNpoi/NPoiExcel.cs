using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Office.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static NPOI.HSSF.Util.HSSFColor;

namespace Office.ExcelNpoi
{
    public class NPoiExcel
    {
        public void NPoiWriteExcel<T>(string outFile,
           List<ExcelExportSheetConfig<T>> sheets)
        {
            try
            {
                IWorkbook workbook = new XSSFWorkbook();

                ICellStyle headStyle = workbook.CreateCellStyle();
                headStyle.Alignment = HorizontalAlignment.Center;
                headStyle.BorderLeft = BorderStyle.Thin;
                headStyle.BorderTop = BorderStyle.Thin;
                headStyle.BorderRight = BorderStyle.Thin;
                headStyle.BorderBottom = BorderStyle.Thin;
                headStyle.FillForegroundColor = Grey25Percent.Index;
                headStyle.FillPattern = FillPattern.SolidForeground;

                for (int count = 0; count < sheets.Count; count++)
                {
                    var sheet = sheets[count];

                    ISheet workbookSheet = workbook.CreateSheet(sheet.SheetName);

                    IRow rowHead = workbookSheet.CreateRow(sheet.RowStart - 1);
                    for (int i = 0; i < sheet.HeaderLabel.Length; i++)
                    {
                        var label = sheet.HeaderLabel[i];
                        ICell cell = rowHead.CreateCell(i);
                        cell.SetCellValue(label);
                        cell.CellStyle = headStyle;
                    }

                    // Write data
                    for (int i = 0; i < sheet.SheetDatas.Count; i++)
                    {
                        var data = sheet.SheetDatas[i];

                        IRow row = workbookSheet.CreateRow(i + sheet.RowStart);
                        for (int j = 0; j < sheet.CellConfig.Count; j++)
                        {
                            ICell cell = row.CreateCell(j);

                            var cellConfig = sheet.CellConfig[j];

                            PropertyInfo pi = data.GetType().GetProperty(cellConfig.FieldName);
                            var valueOfField = pi.GetValue(data, null);
                            if (valueOfField != null)
                            {
                                var typeOfValue = pi.GetValue(data, null).GetType();
                                if (typeOfValue == typeof(short))
                                {
                                    short value = (short)(pi.GetValue(data, null));
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(int))
                                {
                                    int value = (int)(pi.GetValue(data, null));
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(long))
                                {
                                    long value = (long)(pi.GetValue(data, null));
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(double))
                                {
                                    double value = (double)(pi.GetValue(data, null));
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(decimal))
                                {
                                    decimal valueD = (decimal)(pi.GetValue(data, null));
                                    double value = (double)(valueD);
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(DateTime))
                                {
                                    DateTime value = (DateTime)(pi.GetValue(data, null));
                                    cell.SetCellValue(value);
                                }
                                else if (typeOfValue == typeof(bool))
                                {
                                    bool value = (bool)(pi.GetValue(data, null));
                                    cell.SetCellValue(value ? "Yes" : "No");
                                }
                                else
                                {
                                    string value = (string)(pi.GetValue(data, null));
                                    if (cellConfig.StringUpper && !string.IsNullOrEmpty(value))
                                    {
                                        value = value.ToUpper();
                                    }
                                    cell.SetCellValue(value);
                                }

                                if (!string.IsNullOrEmpty(cellConfig.FormatString))
                                {
                                    ICellStyle cellStyle = workbook.CreateCellStyle();
                                    cellStyle.DataFormat = workbook.CreateDataFormat().GetFormat(cellConfig.FormatString);
                                    cell.CellStyle = cellStyle;
                                }
                            }
                            else
                            {
                                cell.SetCellValue("");
                            }
                        }
                    }
                    for (int i = 0; i < sheet.CellConfig.Count; i++)
                    {
                        workbookSheet.AutoSizeColumn(i);
                    }
                }
                using (FileStream stream = new FileStream(outFile, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(stream);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetExcelColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modifier;

            while (dividend > 0)
            {
                modifier = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modifier).ToString() + columnName;
                dividend = (int)((dividend - modifier) / 26);
            }

            return columnName;
        }

        public DataTable NPoiReadExcel(string Path, int sheetIndex, bool isTitle)
        {
            //XSSFWorkbook wb;
            IWorkbook wb = null;

            //XSSFSheet sh;
            ISheet sh = null;
            String Sheet_name;

            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                if (Path.IndexOf(".xlsx") > 0)
                    wb = new XSSFWorkbook(fs);
                else if (Path.IndexOf(".xls") > 0)
                    wb = new HSSFWorkbook(fs);
            }

            DataTable DT = new DataTable();
            DT.Rows.Clear();
            DT.Columns.Clear();

            if (sheetIndex < 0)
                sheetIndex = 0;

            sh = wb.GetSheetAt(sheetIndex);
            Sheet_name = wb.GetSheetAt(sheetIndex).SheetName;//get  sheet name

            int rowStart = sh.FirstRowNum;
            int rowEnd = sh.LastRowNum;

            int columnsCellStart = 0;
            int columnsCellEnd = 0;
            try
            {
                columnsCellStart = sh.GetRow(rowStart).FirstCellNum;
                columnsCellEnd = sh.GetRow(rowStart).LastCellNum;
            }
            catch { }

            if (isTitle)
            {
                // add columns
                for (int readcolumnsCellStart = columnsCellStart; readcolumnsCellStart < columnsCellEnd; readcolumnsCellStart++)
                {
                    DT.Columns.Add(sh.GetRow(rowStart).GetCell(readcolumnsCellStart).ToString(), typeof(string));
                }
            }

            // Ghi data
            int i = 0;
            int j = 0;
            for(int row = rowStart+1; row <= rowEnd; row++)
            {
                DataRow dr = DT.NewRow();
                for (int cell = columnsCellStart; cell < columnsCellEnd; cell++)
                {
                    var cellData = sh.GetRow(row).GetCell(cell).ToString();
                    dr[j] = cellData;
                    j++;
                }
                DT.Rows.Add(dr);
                j = 0;
                i++;
            }
            return DT;
        }

        public List<T> NPoiReadExCel<T>(string fileName, string name) where T : new()
        {
            try
            {
                IWorkbook workbook;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                // kiem tra file
                if (fileName.IndexOf(".xlsx") > 0)
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0)
                    workbook = new HSSFWorkbook(fs);
                else
                {
                    throw new Exception("File khong hop le");
                }
                // đọc sheet đầu tiên
                ISheet sheet = workbook.GetSheetAt(0);

                var col = new List<T>();

                // kiểm tra dữ liệu đầu tiên trong shêet
                if (sheet != null)
                {
                    var property_infos = typeof(T).GetProperties();

                    // lấy dòng đàu và dòng cuối có dữ liệu ra
                    int LastRowNum = sheet.LastRowNum;
                    int FirstRowNum = sheet.FirstRowNum;

                    // xác định tên cột 
                    IRow curRow;
                    curRow = sheet.GetRow(FirstRowNum);
                    List<string> SavePropertyClass = new List<string>();
                    List<string> SaveTypeClass = new List<string>();
                    // Kiem tra tat ca cac truowng trong excel and class
                    for (int BegincurRow = curRow.FirstCellNum; BegincurRow < curRow.LastCellNum; BegincurRow++)
                    {
                        // get data title PropertyClass of excel
                        var cellValue = curRow.GetCell(BegincurRow).StringCellValue.Trim();
                        var CheckPropertyExcel = property_infos.Count(m => m.Name == cellValue);
                        if (CheckPropertyExcel == 0)
                        {
                            throw new Exception("Khong map du cac truong vao dto");
                        }
                        else
                        {
                            SaveTypeClass.Add(property_infos.Where(m => m.Name == cellValue).Select(m => m.PropertyType.Name).FirstOrDefault());
                            SavePropertyClass.Add(cellValue);
                        }
                    }

                    // bawt ddaau add du lieu
                    for (int RowDataBegin = FirstRowNum + 1; RowDataBegin <= LastRowNum; RowDataBegin++)
                    {
                        curRow = sheet.GetRow(RowDataBegin);
                        int LocationType = 0;
                        var obj = new T();
                        for (int BegincurRow = curRow.FirstCellNum; BegincurRow < curRow.LastCellNum; BegincurRow++)
                        {
                            // laays kieu du lieu cua class thong qua SavePropertyExcel
                            var NameProp = SavePropertyClass[LocationType];
                            var TypeProp = SaveTypeClass[LocationType];

                            // tao object T
                            var type = typeof(T);
                            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            var prop = props.FirstOrDefault(x => x.Name == NameProp); // timf teen truong

                            if (TypeProp == "Boolean")
                            {
                                var cellValue = curRow.GetCell(BegincurRow).ToString().Trim();
                                bool Trave = cellValue == "Nam" ? true : false;
                                prop.SetValue(obj, Trave, null);
                            }
                            //else if(TypeProp == "Int16")
                            //{
                            //    short cellValue = Convert.ToInt16(curRow.GetCell(BegincurRow).ToString());
                            //}
                            else if (TypeProp == "Int32")
                            {
                                int cellValue = Convert.ToInt32(curRow.GetCell(BegincurRow).ToString());
                                prop.SetValue(obj, cellValue, null);
                            }
                            else if (TypeProp == "Double")
                            {
                                Double cellValue = Convert.ToDouble(curRow.GetCell(BegincurRow).ToString());
                                prop.SetValue(obj, cellValue, null);
                            }
                            //else if (TypeProp == "Single")
                            //{
                            //    long cellValue = Convert.ToInt64(curRow.GetCell(BegincurRow).ToString());
                            //}
                            else if (TypeProp == "DateTime")
                            {
                                DateTime cellValue = Convert.ToDateTime(curRow.GetCell(BegincurRow).ToString());
                                prop.SetValue(obj, cellValue, null);
                            }
                            //else if (TypeProp == "Byte")
                            //{
                            //    Byte cellValue = Convert.ToByte(curRow.GetCell(BegincurRow).ToString());
                            //}
                            else
                            {
                                var cellValue = curRow.GetCell(BegincurRow).ToString().Trim();
                                prop.SetValue(obj, cellValue, null);
                            }

                            LocationType++;
                        }
                        col.Add(obj);
                    }

                }
                return col;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public void NPoiWriteExcelFile<T>(string outFile,
          List<ExcelExportSheetConfig<T>> sheets)
        {
            try
            {
                IWorkbook workbook = new XSSFWorkbook();

                ICellStyle headStyle = workbook.CreateCellStyle();
                headStyle.Alignment = HorizontalAlignment.Center;
                headStyle.BorderLeft = BorderStyle.Thin;
                headStyle.BorderTop = BorderStyle.Thin;
                headStyle.BorderRight = BorderStyle.Thin;
                headStyle.BorderBottom = BorderStyle.Thin;
                headStyle.FillForegroundColor = Grey25Percent.Index;
                headStyle.FillPattern = FillPattern.SolidForeground;

                for (int count = 0; count < sheets.Count; count++)
                {
                    var sheet = sheets[count];

                    ISheet workbookSheet = workbook.CreateSheet(sheet.SheetName);

                    IRow rowHead = workbookSheet.CreateRow(sheet.RowStart - 1);
                    for (int i = 0; i < sheet.HeaderLabel.Length; i++)
                    {
                        var label = sheet.HeaderLabel[i];
                        ICell cell = rowHead.CreateCell(i);
                        cell.SetCellValue(label);
                        cell.CellStyle = headStyle;
                    }

                    // Write data
                    for (int i = 0; i < sheet.SheetDatas.Count; i++)
                    {
                        var data = sheet.SheetDatas[i];

                        IRow row = workbookSheet.CreateRow(i + sheet.RowStart);
                        for (int j = 0; j < sheet.CellConfig.Count; j++)
                        {
                            ICell cell = row.CreateCell(j);

                            var cellConfig = sheet.CellConfig[j];

                            PropertyInfo pi = data.GetType().GetProperty(cellConfig.FieldName);
                            var valueOfField = pi.GetValue(data, null);
                            if (valueOfField != null)
                            {
                                var typeOfValue = pi.GetValue(data, null).GetType();
                                if (typeOfValue == typeof(short))
                                {
                                    short value = (short)(pi.GetValue(data, null));
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(int))
                                {
                                    int value = (int)(pi.GetValue(data, null));
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(long))
                                {
                                    long value = (long)(pi.GetValue(data, null));
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(double))
                                {
                                    double value = (double)(pi.GetValue(data, null));
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(decimal))
                                {
                                    decimal valueD = (decimal)(pi.GetValue(data, null));
                                    double value = (double)(valueD);
                                    if (cellConfig.ZeroToBlank && value == 0)
                                    {
                                        cell.SetCellValue("");
                                    }
                                    else
                                    {
                                        cell.SetCellValue(value);
                                    }
                                }
                                else if (typeOfValue == typeof(DateTime))
                                {
                                    DateTime value = (DateTime)(pi.GetValue(data, null));
                                    cell.SetCellValue(value);
                                }
                                else if (typeOfValue == typeof(bool))
                                {
                                    bool value = (bool)(pi.GetValue(data, null));
                                    cell.SetCellValue(value ? "Yes" : "No");
                                }
                                else
                                {
                                    string value = (string)(pi.GetValue(data, null));
                                    if (cellConfig.StringUpper && !string.IsNullOrEmpty(value))
                                    {
                                        value = value.ToUpper();
                                    }
                                    cell.SetCellValue(value);
                                }

                                if (!string.IsNullOrEmpty(cellConfig.FormatString))
                                {
                                    ICellStyle cellStyle = workbook.CreateCellStyle();
                                    cellStyle.DataFormat = workbook.CreateDataFormat().GetFormat(cellConfig.FormatString);
                                    cell.CellStyle = cellStyle;
                                }
                            }
                            else
                            {
                                cell.SetCellValue("");
                            }
                        }
                    }
                    for (int i = 0; i < sheet.CellConfig.Count; i++)
                    {
                        workbookSheet.AutoSizeColumn(i);
                    }
                }
                using (FileStream stream = new FileStream(outFile, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(stream);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteExcelFile<T>(
            string outFile,
            List<ExcelExportSheetConfig<T>> sheets)
        {
            var template = new FileInfo(outFile);
            byte[] templateBytes;
            using (var templateStream = new MemoryStream())
            {
                using (SpreadsheetDocument spreadDocument = SpreadsheetDocument.Open(outFile, true))
                {
                    WorkbookPart workbookPart = spreadDocument.WorkbookPart;
                    for (int count = 0; count < sheets.Count; count++)
                    {
                        var sheet = sheets[count];
                        Sheet sheetProcess = workbookPart.Workbook.Descendants<Sheet>()
                            .FirstOrDefault(s => s.Name.Equals(sheet.SheetName));
                        Worksheet workSheetProcess = (workbookPart.GetPartById(sheetProcess.Id.Value) as WorksheetPart).Worksheet;
                        SheetData sheetProcessData = workSheetProcess.GetFirstChild<SheetData>();

                        // Write data
                        for (int i = 0; i < sheet.SheetDatas.Count; i++)
                        {
                            var data = sheet.SheetDatas[i];
                            Row dataRow = new Row
                            {
                                RowIndex = (uint)(i + sheet.RowStart)
                            };

                            for (int j = 0; j < sheet.CellConfig.Count; j++)
                            {
                                var cellConfig = sheet.CellConfig[j];
                                string cellValue = GetObjectFieldValue(
                                    data,
                                    cellConfig.FieldName,
                                    cellConfig.FormatString,
                                    cellConfig.ZeroToBlank,
                                    cellConfig.StringUpper,
                                    false);
                                Cell newCell = CreateTextCell(sheet.ColumnStart + j + 1,
                                    Convert.ToInt32((uint)(i + sheet.RowStart)),
                                    cellValue, cellConfig.FormatString);
                                dataRow.AppendChild(newCell);
                            }
                            sheetProcessData.AppendChild(dataRow);
                        }
                        workSheetProcess.Save();
                    }

                    workbookPart.Workbook.Save();
                    spreadDocument.Close();
                }

                templateBytes = File.ReadAllBytes(template.FullName);
                templateStream.Write(templateBytes, 0, templateBytes.Length);
                templateStream.Position = 0;

                templateStream.ToArray();
                templateStream.Flush();
            }
        }
    }
}
