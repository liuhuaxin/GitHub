using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel;
using System.Threading;

namespace TestPlusA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string path;
        private Dictionary<string, List<int>> dic;
        private List<string> removeList = new List<string>();

        // 拖动文件
        private void txtOld_DragEnter_1(object sender, DragEventArgs e)
        {
            path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            dic = ReadAllData();
            label3.Text = path;
        }

        // 读取数字到List
        //private List<int> ReadNumber(string path, int lie)// 0为第一列
        //{
        //    List<int> intList = new List<int>();
        //    List<DataRow> list = ZZExcelReader.ReadExcel(path);

        //    foreach (DataRow each in list)
        //    {
        //        string ss = ZZExcelReader.GetString(each, lie);
        //        if (ss.Contains("."))
        //            continue;

        //        int number = ZZExcelReader.GetInt(each, lie);
        //        intList.Add(number);
        //    }

        //    return intList;
        //}

        // 网上找的算法
        private bool BackTrace(int[] a, bool[] flag, int target, string name)
        {
            int sum = 0;//初始化和
            int index = 0;//初始化索引
            while (index >= 0)//从第一个开始找,循环找,与for相比循环更多次
            {
                if (flag[index] == false)//如果不在组合中,则尝试把它加入
                {
                    sum += a[index];
                    flag[index] = true;

                    if (sum == target)//如果加入后组合数与目标数一致,则说明找到组合,如果组合数大于目标数,则将刚才的元素从组合中剔除,如果小于,则什么也不做.同时继续检验下一元素.
                        return true;
                    else if (sum > target)
                    {
                        sum -= a[index];
                        flag[index] = false;
                    }
                    index++;//继续检验下一元素
                }

                //如果索引到了最后,还没有找到合适的组合,那么将回溯.一般来说会出现*100011或*1000的情况,即此时flag中的元素应该在某个1之后有若干个0或01组合【先0后1】, [I个数] 1 [J个0][K个1]这样的情况,回溯到1的位置,将其变为0,然后继续往下循环检验.从后面回溯的时候,将1变为0,遇0不变.如果回溯到首位,则说明没有合适的组合存在.

                if (index >= a.Length)
                {
                    while (flag[index - 1] == true)//如果在组合中，则退出，并往前回溯
                    {
                        flag[index - 1] = false;
                        index--;
                        sum -= a[index];
                        if (index < 1)//此时index最大为0，但下次循环将出界，已经回溯到最开始了
                            return false;
                    }

                    while (flag[index - 1] == false)//如果不在组合中，往前回溯
                    {
                        index--;
                        if (index < 1)
                            return false;//此时index最大为0，但下次循环将出界，已经回溯到最开始了
                    }

                    flag[index - 1] = false;
                    sum -= a[index - 1];
                }
            }
            return false;
        }

        private void Run()
        {
            int target = int.Parse(textBoxTarget.Text);
            string str = string.Empty;

            foreach (var each in dic)
            {
                try
                {
                    string name = each.Key;
                    // 被移除的商家跳过
                    if (removeList.Contains(name))
                        continue;

                    textBoxResult.Text = "正在搜索:" + name;
                    List<int> intList = each.Value;

                    int[] numbers = intList.ToArray();
                    bool[] flags = new bool[numbers.Length];

                    //textBoxError.Text = "正在:" + name;
                    bool result = BackTrace(numbers, flags, target, name);
                    if (result)
                    {
                        for (int i = 0; i <= numbers.Length - 1; i++)
                        {
                            if (flags[i])
                            {
                                str += numbers[i].ToString() + "\r\n";
                            }
                        }

                        // 保存移除商家
                        str += "找到结果在(" + name + ")";
                        removeList.Add(name);
                        RefreshRemove();
                        break;
                    }
                    else
                    {
                        //str = "没有找到组合";
                    }
                }
                catch (Exception ee)
                {
                    break;
                }
            }

            if (string.IsNullOrEmpty(str)) str = "没有找到组合";
            textBoxResult.Text = str;
            btnFind.Enabled = true;
        }

        // 执行查找
        private void btnFind_Click(object sender, EventArgs e)
        {
            // 判断输入
            if (string.IsNullOrEmpty(textBoxTarget.Text))
            {
                textBoxResult.Text = "请输入要查找的数字";
                return;
            }

            Control.CheckForIllegalCrossThreadCalls = false;
            //textBoxResult.Text = "正在查找中...";
            btnFind.Enabled = false;

            Thread thread = new Thread(Run);
            thread.Start();
        }

        // 转换按钮
        private void btnTransform_Click(object sender, EventArgs e)
        {
            //Dictionary<string, List<int>> dic = ReadAllData();
            //WriteToExcel(dic);
        }

        // 读取所有数据
        private Dictionary<string, List<int>> ReadAllData()
        {
            // key:公司1 value:List<int>
            Dictionary<string, List<int>> dic = new Dictionary<string, List<int>>();

            List<DataRow> rowList = ZZExcelReader.ReadExcel(path);

            foreach (DataRow row in rowList)
            {
                string name = ZZExcelReader.GetString(row, 0);
                string ss = ZZExcelReader.GetString(row, 1);
                if (ss.Contains("."))
                    continue;

                int number = ZZExcelReader.GetInt(row, 1);
                if (number <= 0)
                    continue;

                // 如果没有这个公司的key，就先添加一个
                if (!dic.ContainsKey(name))
                    dic.Add(name, new List<int>());

                List<int> list = dic[name];
                list.Add(number);
            }

            return dic;
        }

        // 写入到Excel
        private void WriteToExcel(Dictionary<string, List<int>> dic)
        {
            DataTable dt = new DataTable("first");

            foreach (string name in dic.Keys)
            {
                dt.Columns.Add(name, System.Type.GetType("System.String"));
            }

            int maxLine = GetMaxLine(dic);
            for (int i = 0; i < maxLine; ++i)
            {
                DataRow dr = dt.NewRow();

                foreach (var each in dic)
                { 
                    string name = each.Key;
                    List<int> list = each.Value;

                    if (list.Count <= i)
                        continue;

                    dr[name] = list[i].ToString();
                }
                
                dt.Rows.Add(dr);
            }

            ToExcel2.SaveToExcel("result.xlsx", dt);
        }

        // 获取最大行数
        private int GetMaxLine(Dictionary<string, List<int>> dic)
        {
            int maxLength = 0;
            foreach (List<int> list in dic.Values)
            {
                if (list.Count > maxLength) maxLength = list.Count;
            }
            return maxLength;
        }

        // 获取目录路径
        private string GetPrePath(string path)
        {
            int index = path.LastIndexOf("\\");
            return path.Substring(0, index);
        }

        // 清空
        private void btnClearRemove_Click(object sender, EventArgs e)
        {
            removeList.Clear();
            RefreshRemove();
        }

        // 刷新被移除的商家
        private void RefreshRemove()
        {
            string str = string.Empty;
            foreach (string each in removeList)
            {
                str += each + "\r\n";
            }

            textBoxRemove.Text = str;
        }
    }
}
