using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string excelFilePath = "";
        DataTable gData = null;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*xls*)|*.xls*"; //设置要选择的文件的类型
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                excelFilePath = fileDialog.FileName;//获得文件的全路径
            }
            else
            {
                MessageBox.Show("Excel 文件读取失败 ");
            }
        }
   
        // 处理数据
        private void deal_Click(object sender, EventArgs e)
        {
            if (excelFilePath == "")
            {
                MessageBox.Show("请导入文件");
                return;
            }
            if (choiceColumn.Text == "")
            {
                MessageBox.Show("请输入列数");
                return;
            }
            int nColumn = int.Parse(choiceColumn.Text);
            Regex reg = new Regex(@"产品数量\s*:\s*([\d\.]+)");
            gData = readExcel(excelFilePath);
            gData.Columns.Add("总量");
            int nColumns = gData.Columns.Count;

            for (int i = 0; i < gData.Rows.Count; i++)
            {
                var rowData = gData.Rows[i];
                object columnData = rowData[nColumn];
                MatchCollection matcheMap = reg.Matches((string)columnData);    // 匹配所有
                Match[] marr = matcheMap.OfType<Match>().ToArray();

                int nSum = 0;
                for (int j = 0; j < marr.Length; j++)
                {
                    Match matche = marr[j];
                    nSum = nSum + int.Parse(matche.Groups[1].Value);
                }
                gData.Rows[i][nColumns - 1] = nSum;
            }
            MessageBox.Show("处理完成");
        }

        //保存处理好的数据
        private void save_Click(object sender, EventArgs e)
        {
            updateExcel(excelFilePath, gData);
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Title = "导出为Excel文件";
            ////设置文件类型 
            //sfd.Filter = "所有文件(*xls*)|*.xls*"; //设置要选择的文件后缀
            ////设置默认文件类型显示顺序 
            //sfd.FilterIndex = 1;

            ////保存对话框是否记忆上次打开的目录 
            //sfd.RestoreDirectory = true;
            ////设置默认的文件名
            //sfd.FileName = "YourFileName";// in wpf is  sfd.FileName = "YourFileName";
            ////点了保存按钮进入 
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    string localFilePath = sfd.FileName.ToString(); //获得文件路径 
            //    string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
            //    SaveToExcel(localFilePath, gData);
            //}
        }

        //读取表格内容
        public DataTable readExcel(string excelFilePath)
        {
            //通过select语句读取excel表的内容，存放到dataset中，方便操作
            try
            {
                //创建连接，引用协议
                string strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFilePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
                // string strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFilePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";//HDR=YES,第一行为表头，否则为FALSE
                OleDbConnection oleDbConnect = new OleDbConnection(strConnect);

                //打开连接，并执行sql语句，需要最后关闭
                oleDbConnect.Open();
                string sql = "SELECT * FROM [Sheet1$]";
                OleDbDataAdapter oleDaExcel = new OleDbDataAdapter(sql, oleDbConnect);
                DataSet oleDsExcel = new DataSet();
                oleDaExcel.Fill(oleDsExcel, "Sheet1");
                oleDbConnect.Close();
                return oleDsExcel.Tables["Sheet1"];
            }
            catch (Exception e)
            {
                MessageBox.Show("数据绑定Excel失败，失败原因：" + e.Message);
                return null;
            }
        }

        //读取表格内容
        public void updateExcel(string excelFilePath, DataTable dt)
        {
            //通过select语句读取excel表的内容，存放到dataset中，方便操作
            try
            {


                string strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFilePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
                OleDbConnection oleDbConnect = new OleDbConnection(strConnect);

                //打开连接，并执行sql语句，需要最后关闭
                oleDbConnect.Open();
                string sql = "SELECT * FROM [Sheet1$]";
                OleDbDataAdapter oleDaExcel = new OleDbDataAdapter(sql, oleDbConnect);
               // DataSet oleDsExcel = new DataSet();
              //  oleDaExcel.Fill(oleDsExcel, "Sheet1");
                
                dt.AcceptChanges();

                oleDbConnect.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("数据绑定Excel失败，失败原因：" + e.Message);
            }
        }
        /// <summary>
        /// 将数据表保存到Excel表格中
        /// </summary>
        /// <param name="addr">Excel表格存放地址（程序运行目录后面的部分）</param>
        /// <param name="dt">要输出的DataTable</param>
        public static void SaveToExcel(string addr, System.Data.DataTable dt)
        {
            //0.注意：
            // * Excel中形如Cells[x][y]的写法，前面的数字是列，后面的数字是行!
            // * Excel中的行、列都是从1开始的，而不是0
            //1.制作一个新的Excel文档实例
            Excel::Application xlsApp = new Excel::Application();
            xlsApp.Workbooks.Add(true);
            /* 示例输入：需要注意Excel里数组以1为起始（而不是0）
              * for (int i = 1; i < 10; i++)
              * {
              *   for (int j = 1; j < 10; j++)
              *   {
              *     xlsApp.Cells[i][j] = "-"; 
              *   }
              * }
              */
            //2.设置Excel分页卡标题
            xlsApp.ActiveSheet.Name = dt.TableName;
            //3.合并第一行的单元格
            /*
            string temp = "";
            if (dt.Columns.Count < 26)
            {
                temp = ((char)('A' + dt.Columns.Count)).ToString();
            }
            else if (dt.Columns.Count <= 26 + 26 * 26)
            {
                temp = ((char)('A' + (dt.Columns.Count - 26) / 26)).ToString()
                  + ((char)('A' + (dt.Columns.Count - 26) % 26)).ToString();
            }
            else throw new Exception("列数过多");
            Excel::Range range = xlsApp.get_Range("A1", temp + "1");
            range.ClearContents(); //清空要合并的区域
            range.MergeCells = true; //合并单元格
            //4.填写第一行：表名，对应DataTable的TableName
            xlsApp.Cells[1][1] = dt.TableName;
            xlsApp.Cells[1][1].Font.Name = "黑体";
            xlsApp.Cells[1][1].Font.Size = 25;
            xlsApp.Cells[1][1].Font.Bold = true;
            xlsApp.Cells[1][1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//居中
            xlsApp.Rows[1].RowHeight = 60; //第一行行高为60（单位：磅）
            //5.合并第二行单元格，用于书写表格生成日期
            range = xlsApp.get_Range("A2", temp + "2");
            range.ClearContents(); //清空要合并的区域
            range.MergeCells = true; //合并单元格
            */
            //6.填写第二行：生成时间
            xlsApp.Cells[1][2] = "报表生成于：" + DateTime.Now.ToString();
            xlsApp.Cells[1][2].Font.Name = "宋体";
            xlsApp.Cells[1][2].Font.Size = 15;
            //xlsApp.Cells[1][2].HorizontalAlignment = 4;//右对齐
            xlsApp.Cells[1][2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//居中
            xlsApp.Rows[2].RowHeight = 30; //第一行行高为60（单位：磅）

            string temp = "";
            //7.填写各列的标题行
            xlsApp.Cells[1][3] = "序号";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                xlsApp.Cells[i + 2][3] = dt.Columns[i].ColumnName;
            }
            xlsApp.Rows[3].Font.Name = "宋体";
            xlsApp.Rows[3].Font.Size = 15;
            xlsApp.Rows[3].Font.Bold = true;
            xlsApp.Rows[3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//居中
            //设置颜色
            //Excel::Range range = xlsApp.get_Range("A3", temp + "3");
            //range.Interior.ColorIndex = 33;
            //range.NumberFormatLocal = "@";//文本格式


            //range = xlsApp.get_Range("A4", temp + (dt.Rows.Count + 3).ToString());
            //range.Interior.ColorIndex = 37;
            //range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            //range.NumberFormatLocal = "@";//文本格式

            //8.填写DataTable中的数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                xlsApp.Cells[1][i + 4] = i.ToString();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    xlsApp.Cells[j + 2][i + 4] = dt.Rows[i][j].ToString();
                }
            }

            //9.描绘边框
            //range = xlsApp.get_Range("A1", temp + (dt.Rows.Count + 3).ToString());
            //range.Borders.LineStyle = 1;
            //range.Borders.Weight = 3;
            //10.打开制作完毕的表格
            //xlsApp.Visible = true;
            //11.保存表格到根目录下指定名称的文件中
            //xlsApp.ActiveWorkbook.SaveAs(Application.StartupPath + "/" + addr);
            xlsApp.ActiveWorkbook.SaveAs(addr);
            xlsApp.Quit();
            xlsApp = null;
            GC.Collect();
        }

    }
}
