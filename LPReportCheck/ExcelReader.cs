using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReportChecker
{
    class ExcelReader
    {
        Excel.Application xlApp;

        public ExcelReader(string filename)
        {
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(filename);
            SetPage(1);
        }

        public Excel.Range xlRange { get; set; }

        public Excel.Workbook xlWorkbook { get; set; }

        public Excel._Worksheet xlWorksheet { get; set; }

        public void SetPage(int sheetNo)

        {
            xlWorksheet = xlWorkbook.Sheets[sheetNo];
            xlRange = xlWorksheet.UsedRange;
        }

        public string GetCell(int row, int col)
        {
            if (xlRange.Cells[row, col] != null && xlRange.Cells[row, col].Value2 != null)
            {
                return xlRange.Cells[row, col].Value2.ToString();
            }
            else
            {
                return "";
            }
        }

        ~ExcelReader()
        {
            //You have to release references to these objects manually otherwise file access writes to the workbook will persist.
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}