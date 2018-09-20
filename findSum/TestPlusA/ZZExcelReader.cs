using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;

public class ZZExcelReader
{
    /// <summary>
    /// 读取一个Excel成List
    /// </summary>
    public static List<DataRow> ReadExcel(string path)
    {
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        DataTable table = result.Tables[0];

        List<DataRow> rowList = GetRowList(table);
        return rowList;
    }

    private static List<DataRow> GetRowList(DataTable table)
    {
        List<DataRow> rowList = new List<DataRow>();
        DataRowCollection collection = table.Rows;

        foreach (DataRow each in collection)
        {
            if (each[0].ToString().Equals(""))
                continue;

            rowList.Add(each);
        }

        return rowList;
    }

    // get float
    public static float GetFloat(DataRow row, int index)
    {
        string str = GetString(row, index);
        if (string.IsNullOrEmpty(str)) return 0;
        return float.Parse(str);
    }

    public static float GetFloatMin(DataRow row, int index, char split)
    {
        string str = GetString(row, index);
        if (string.IsNullOrEmpty(str)) return 0;
        if (!str.Contains(split)) return float.Parse(str);
        return float.Parse(ZZUtil.SplitString(str, split)[0]);
    }

    public static float GetFloatMax(DataRow row, int index, char split)
    {
        string str = GetString(row, index);
        if (string.IsNullOrEmpty(str)) return 0;
        if (!str.Contains(split)) return float.Parse(str);
        return float.Parse(ZZUtil.SplitString(str, split)[1]);
    }

    // get int
    public static int GetInt(DataRow row, int index)
    {
        string str = GetString(row, index);
        if (string.IsNullOrEmpty(str)) return 0;
        return int.Parse(str);
    }

    public static int GetIntMin(DataRow row, int index, char split)
    {
        string str = GetString(row, index);
        if (string.IsNullOrEmpty(str)) return 0;
        if (!str.Contains(split)) return int.Parse(str);
        return int.Parse(ZZUtil.SplitString(str, split)[0]);
    }

    public static int GetintMax(DataRow row, int index, char split)
    {
        string str = GetString(row, index);
        if (string.IsNullOrEmpty(str)) return 0;
        if (!str.Contains(split)) return int.Parse(str);
        return int.Parse(ZZUtil.SplitString(str, split)[1]);
    }

    // get string
    public static string GetString(DataRow row, int index)
    {
        return row[index].ToString();
    }
}
