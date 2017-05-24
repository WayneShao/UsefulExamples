﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using OfficeOpenXml;
using System.Runtime.Serialization.Formatters.Binary;

namespace ReadingFromExcel
{
    class Program
    {
        static void ParseToExcel(string path, byte[] bytes)
        {
            System.IO.File.WriteAllBytes(path, bytes);
        }
        static byte[] GetActiveWorkbook(Microsoft.Office.Interop.Excel.Application app)
        {
            string path = Path.GetTempFileName();
            try
            {
                app.ActiveWorkbook.SaveCopyAs(path);
                return File.ReadAllBytes(path);
            }
            finally
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
        static void Main(string[] args)
        {
            Microsoft.Office.Interop.Excel.Application xlApp0 = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook0 = xlApp0.Workbooks.Open(@"C:\Users\Dell\Desktop\user.xlsx");

            byte[] array = GetActiveWorkbook(xlApp0);
            






            string path = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory) + @"\newexceldocument1.xlsx";

            System.IO.File.WriteAllBytes(path, array);





            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            for (int i = 1; i <= xlWorksheet.Rows.Count; i++)
            {
                if (xlRange.Cells[i, 1].Value2 != null && xlRange.Cells[i, 1] != null)
                {
                    for (int j = 1; j <= xlWorksheet.Columns.Count; j++)
                    {
                        //new line
                        if (j == 1)
                            Console.Write("\r\n");

                        //write the value to the console
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                        else
                            break;
                    }
                }
                else
                    break;
            }
        }
    }
}
