using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Lib.FileProcessing.OnlyExcel.NPoiExcel.Dtos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Office.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using static NPOI.HSSF.Util.HSSFColor;

namespace Office.Npoi
{ 
    class NPoi
    {
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

        public void WriteWordFile<T1, T2>(
            string outFile,
            WordReportConfig<T1, T2> configData
            )
        {
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outFile, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    var listFieldNames = typeof(T1).GetProperties().Select(f => f.Name).ToList();
                    foreach (var fieldname in listFieldNames)
                    {
                        string formatString = "";
                        if (configData.FieldFormats != null)
                        {
                            foreach (var item in configData.FieldFormats)
                            {
                                if (item.Fields.Contains(fieldname))
                                {
                                    formatString = item.FormatString;
                                    break;
                                }
                            }
                        }

                        string textValue = GetObjectFieldValue(
                            configData.Data,
                            fieldname,
                            formatString,
                            configData.FieldsZeroToBlank.Contains(fieldname),
                            configData.FieldsStringToUpper.Contains(fieldname),
                            true);

                        docText = WordReportBinding(docText, wordDoc, fieldname, textValue);
                    }

                    if (configData.TablesDraw != null)
                    {
                        foreach (var table in configData.TablesDraw)
                        {
                            docText = WordReportBinding(docText, wordDoc, table.TemplateKey, CreateXmlTable(table));
                        }
                    }

                    foreach (KeyValuePair<string, string> item in configData.MapKeysToFields)
                    {
                        string formatString = "";
                        if (configData.FieldFormats != null)
                        {
                            foreach (var format in configData.FieldFormats)
                            {
                                if (format.Fields.Contains(item.Value))
                                {
                                    formatString = format.FormatString;
                                    break;
                                }
                            }
                        }

                        string textValue = GetObjectFieldValue(
                            configData.Data,
                            item.Value,
                            formatString,
                            configData.FieldsZeroToBlank.Contains(item.Value),
                            configData.FieldsStringToUpper.Contains(item.Value),
                            true);

                        docText = WordReportBinding(docText, wordDoc, item.Key, textValue);
                    }

                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }

                    wordDoc.Save();
                    wordDoc.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cell CreateTextCell(int columnIndex, int rowIndex, string cellValue, string format = "")
        {
            Cell cell = new Cell
            {
                CellReference = GetExcelColumnName(columnIndex) + rowIndex,
                DataType = CellValues.InlineString
            };
            InlineString inlineString = new InlineString();
            Text t = new Text
            {
                Text = cellValue == null ? "" : cellValue.ToString()
            };
            inlineString.AppendChild(t);
            cell.AppendChild(inlineString);

            /*
            int resInt;
            double resDouble;
            decimal resDecimal;
            DateTime resDate;
            long resLong;
            
            if (int.TryParse(cellValue, out resInt))
            {
                CellValue v = new CellValue();
                v.Text = resInt.ToString();

                cell.AppendChild(v);
            }
            else if (decimal.TryParse(cellValue, out resDecimal))
            {
                CellValue v = new CellValue();
                v.Text = resDecimal.ToString();
                cell.AppendChild(v);
            }
            else if (long.TryParse(cellValue, out resLong))
            {
                CellValue v = new CellValue();
                v.Text = resLong.ToString();
                cell.AppendChild(v);
            }
            else if (double.TryParse(cellValue, out resDouble))
            {
                CellValue v = new CellValue();
                v.Text = resDouble.ToString();
                cell.AppendChild(v);
            }
            else if (DateTime.TryParse(cellValue, out resDate))
            {
                cell.DataType = CellValues.InlineString;
                InlineString inlineString = new InlineString();
                Text t = new Text();

                if (string.IsNullOrEmpty(format))
                {
                    t.Text = resDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    t.Text = resDate.ToString(format);
                }

                inlineString.AppendChild(t);
                cell.AppendChild(inlineString);
            }
            else
            {
                cell.DataType = CellValues.InlineString;
                InlineString inlineString = new InlineString();
                Text t = new Text();

                t.Text = cellValue == null ? "" : cellValue.ToString();
                inlineString.AppendChild(t);
                cell.AppendChild(inlineString);
            }
            */
            return cell;
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

        public void WriteLog(Exception ex, string pObjErr, string logFile)
        {
            string confLogDir = ConfigurationManager.AppSettings["JobLogFolder"] + "\\";

            string sWebsitePath = confLogDir;
            string path = sWebsitePath + logFile;
            if (!CheckIfFileIsBeingUsed(path))
            {
                FileInfo oFileInfo = new FileInfo(path);
                DirectoryInfo oDirInfo = new DirectoryInfo(oFileInfo.DirectoryName);

                if (!oDirInfo.Exists)
                {
                    //oDirInfo.Create();
                }

                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write);
                using (StreamWriter w = new StreamWriter(fs))
                {
                    if (null == ex)
                    {
                        w.WriteLine(
                            "--------------------------------------------------------------------------------------");
                        w.WriteLine("<Log Entry> : {0} {1}", DateTime.Now.ToLongTimeString(),
                                    DateTime.Now.ToLongDateString());
                        w.WriteLine("<Object> : " + pObjErr);
                        w.WriteLine(
                            "--------------------------------------------------------------------------------------");
                        w.Flush();
                    }
                    else if (!(ex is ThreadAbortException))
                    {
                        w.WriteLine(
                            "--------------------------------------------------------------------------------------");
                        w.WriteLine("<Log Entry> : {0} {1}", DateTime.Now.ToLongTimeString(),
                                    DateTime.Now.ToLongDateString());
                        w.WriteLine("<Message> : " + ex.Message);
                        w.WriteLine("<StackTrace> : " + ex.StackTrace);
                        w.WriteLine("<Source> : " + ex.Source);
                        w.WriteLine("<TargetSite> : " + ex.TargetSite.ToString());
                        w.WriteLine("<Object> : " + pObjErr);
                        w.WriteLine(
                            "--------------------------------------------------------------------------------------");
                        w.Flush();
                    }
                    w.Close();
                }
                fs.Close();
            }
        }

        public bool CheckIfFileIsBeingUsed(string fileName)
        {
            try
            {
                FileStream fs;
                fs = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                fs.Close();
            }
            catch
            {
                return true;
            }
            return false;
        }

        public string CreateXmlTable<T>(WordReportTableConfig<T> tableConfig)
        {
            string tableContent = "";
            using (StreamReader file = new StreamReader(tableConfig.TableFilePath))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    tableContent += ln;
                }
                file.Close();
            }

            string tableRowContent = "";
            using (StreamReader file = new StreamReader(tableConfig.TableRowFilePath))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    tableRowContent += ln;
                }
                file.Close();
            }

            string rowRenders = "";
            if (tableConfig.Datas != null)
            {
                foreach (var row in tableConfig.Datas)
                {
                    string rowAdder = tableRowContent;
                    var listFieldNames = typeof(T).GetProperties().Select(f => f.Name).ToList();
                    foreach (var fieldname in listFieldNames)
                    {
                        string formatString = "";
                        if (tableConfig.FieldFormats != null)
                        {
                            foreach (var item in tableConfig.FieldFormats)
                            {
                                if (item.Fields.Contains(fieldname))
                                {
                                    formatString = item.FormatString;
                                    break;
                                }
                            }
                        }
                        string textValue = GetObjectFieldValue(
                            row,
                            fieldname,
                            formatString,
                            tableConfig.FieldsZeroToBlank.Contains(fieldname),
                            tableConfig.FieldsStringToUpper.Contains(fieldname),
                            true);

                        rowAdder = rowAdder.Replace("{" + fieldname + "}", textValue);
                    }

                    rowRenders += rowAdder;
                }
            }

            if (string.IsNullOrEmpty(rowRenders) && tableConfig.NoDataToBlank)
            {
                return "";
            }

            tableContent = tableContent.Replace("{TableRows}", rowRenders);
            return tableContent;
        }

        public string GetObjectFieldValue(
            object data,
            string fieldname,
            string formatString,
            bool zeroToBlank,
            bool stringUpper,
            bool isXml)
        {
            PropertyInfo pi = data.GetType().GetProperty(fieldname);
            var valueOfField = pi.GetValue(data, null);

            string textValue;
            if (valueOfField != null)
            {
                var typeOfValue = pi.GetValue(data, null).GetType();

                if (typeOfValue == typeof(short))
                {
                    short value = (short)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(int))
                {
                    int value = (int)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(long))
                {
                    long value = (long)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(double))
                {
                    double value = (double)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(decimal))
                {
                    decimal value = (decimal)(pi.GetValue(data, null));

                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString();
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }

                    if (zeroToBlank && value == 0)
                    {
                        textValue = "";
                    }
                }
                else if (typeOfValue == typeof(DateTime))
                {
                    DateTime value = (DateTime)(pi.GetValue(data, null));
                    if (string.IsNullOrEmpty(formatString))
                    {
                        textValue = value.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        textValue = value.ToString(formatString);
                    }
                }
                else if (typeOfValue == typeof(bool))
                {
                    bool value = (bool)(pi.GetValue(data, null));
                    textValue = value ? "1" : "0";
                }
                else
                {
                    string value = (string)(pi.GetValue(data, null));
                    if (stringUpper && !string.IsNullOrEmpty(value))
                    {
                        value = value.ToUpper();
                    }
                    textValue = isXml ? ForceXmlString(value) : value;
                }
            }
            else
            {
                textValue = "";
            }

            return textValue;
        }

        public string ForceXmlString(string inputStr)
        {
            return inputStr
                .Replace("&", "&#38;")
                .Replace("<", "&#60;")
                .Replace(">", "&#62;")
                .Replace("'", "&#39;")
                .Replace("\"", "&#34;")
                .Replace("\r\n", "<w:br/>");
        }

        public string WordReportBinding(
            string docText,
            WordprocessingDocument wordDoc,
            string templateKey,
            string textValue)
        {
            docText = docText.Replace("{" + templateKey + "}", textValue);
            foreach (var headerPart in wordDoc.MainDocumentPart.HeaderParts)
            {
                foreach (var currentText in headerPart.RootElement.Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>())
                {
                    currentText.Text = currentText.Text.Replace("{" + templateKey + "}", textValue);
                }
            }

            return docText;
        }


    }
}
