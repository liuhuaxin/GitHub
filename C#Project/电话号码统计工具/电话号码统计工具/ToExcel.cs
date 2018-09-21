using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExcelTools
{
    /// <summary>
    /// 导出到EXCEL表
    /// </summary>
    private static void toExcel(string FileName, string SheetName, System.Data.DataTable dt)
    {
        Application myExcel = new Application();
        Worksheet worksheet = null;

        string curPath = Directory.GetParent(FileName).FullName;
        if (!Directory.Exists(curPath))
        {
            Directory.CreateDirectory(curPath);
        }

        if (File.Exists(FileName))
        {
            try
            {
                File.Delete(FileName);
            }
            catch
            {
                throw new Exception("文件操作出错，可能正在运行当前的EXCEL文件！");
            }
        }

        try
        {
            object missing = System.Reflection.Missing.Value;

            myExcel.Workbooks.Add(missing);
            worksheet = (Worksheet)myExcel.ActiveSheet;
            worksheet.Name = SheetName;
            myExcel.Visible = false;

            int iRows = dt.Rows.Count;
            int iCol = dt.Columns.Count;

            worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[iRows + 1, iCol]).NumberFormatLocal = "@";

            for (int i = 0; i < iCol; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ToString();
            }

            int size = iRows / 1000;
            int page = 0;
            string[,] rows = null;
            for (int i = 0; i < iRows; )
            {
                if (page < size)
                {
                    rows = new string[1000, iCol];
                    for (int count = 0; count < 1000; count++)
                    {
                        for (int j = 0; j < iCol; j++)
                        {
                            rows[count, j] = dt.DefaultView[i][j].ToString();
                        }
                        i++;
                    }
                    worksheet.get_Range(worksheet.Cells[page * 1000 + 2, 1], worksheet.Cells[page * 1000 + 1001, iCol]).Value2 = rows;
                    page++;
                }
                else
                {
                    int left = iRows % 1000;
                    rows = new string[left, iCol];
                    for (int count = 0; count < left; count++)
                    {
                        for (int j = 0; j < iCol; j++)
                        {
                            rows[count, j] = dt.DefaultView[i][j].ToString();
                        }
                        i++;
                    }
                    worksheet.get_Range(worksheet.Cells[iRows + 2 - left, 1], worksheet.Cells[iRows + 1, iCol]).Value2 = rows;
                }
            }

            worksheet.SaveAs(FileName, missing, missing, missing, missing, missing, missing, missing, missing, missing);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            myExcel.Workbooks.Close();
            myExcel.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcel);
            worksheet = null;
            myExcel = null;
            GC.Collect();
        }

    }

    /// <summary>
    /// 因为调用EXCEL后，结束时EXCEL进程并没有立即KILL，用这个方法调用可以KILL掉EXCEL
    /// </summary>
    public static void callToExcel(string FileName, string SheetName, System.Data.DataTable dt)
    {
        toExcel(FileName, SheetName, dt);
        GC.Collect();
    }
}
