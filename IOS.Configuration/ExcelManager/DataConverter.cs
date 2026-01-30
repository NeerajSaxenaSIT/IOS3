using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.IO;


namespace IOS.Configuration.ExcelManager
{
    public class DataConverter
    {
        public DataSet ConvertXmlStringToDataSet(string xmlString)
        {
            DataSet ds = new DataSet();
            StringReader sr = new StringReader(xmlString);
            ds.ReadXml(sr);
            return ds;
        }
        public DataSet ConvertExcelToDataSet(string workBookPath)
        {
            DataSet sheets = new XLSDataProvider(workBookPath).GetAllData();
            sheets.Tables[0].TableName = "IOSConfiguration";
            for (int i = 1; i < sheets.Tables.Count - 1; i++)
            {
                if (sheets.Tables[i].Rows.Count > 0 && string.IsNullOrEmpty(Convert.ToString(sheets.Tables[i].Rows[0][0])))
                {
                    sheets.Tables.RemoveAt(i);
                }
            }
            if (sheets.Tables.Count > 1)
                sheets.Tables[1].TableName = "UserSetting";
            foreach (DataColumn col in sheets.Tables[0].Columns)
            {
                col.ColumnName = col.ColumnName.Replace(" ", "_");
            }
            return sheets;
            //sheets.Tables[0].WriteXml(path, XmlWriteMode.WriteSchema);
        }
        public void DatasetToExcel(DataSet ds, string workBookPath, bool IsOpenAlso)
        {
            Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = new Worksheet(); //(Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int sheetIndex = 0;
            for (int t = 0; t < ds.Tables.Count; t++)
            {
                if (t > 2)
                {
                    xlWorkBook.Sheets.Add(misValue, misValue, misValue, misValue);
                    sheetIndex = 1;
                }
                else
                {
                    sheetIndex++;
                }
                xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(sheetIndex);

                // Excel work sheet name can not be more than 31 char
                xlWorkSheet.Name = ds.Tables[t].TableName.Length > 31 ? ds.Tables[t].TableName.Substring(0, 30) : ds.Tables[t].TableName;
                for (int c = 1; c <= ds.Tables[t].Columns.Count; c++)
                {
                    xlWorkSheet.Cells[1, c] = ds.Tables[t].Columns[c - 1].ColumnName;

                }
                string data = string.Empty;
                for (int i = 0; i <= ds.Tables[t].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[t].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[t].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 2, j + 1] = data;
                        //((Range)xlWorkSheet.Cells[i + 2, j + 1]).EntireColumn.AutoFit();
                    }
                }
                xlWorkSheet.Columns.EntireColumn.AutoFit();
            }

            string savePath = workBookPath;
            DeleteExistingFile(savePath);
            xlWorkBook.SaveAs(savePath, (Path.GetExtension(savePath).ToLower() == ".xlsx") ? XlFileFormat.xlWorkbookDefault : XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            if (IsOpenAlso)
            {
                xlApp.Workbooks.Open(workBookPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                xlApp.Visible = true;
            }
            else
            {
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                System.GC.Collect();
            }
        }
        private static void DeleteExistingFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {

            }
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;

            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
