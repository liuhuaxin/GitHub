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
using ZFile;

namespace 电话号码统计工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string filePath;
        private Dictionary<string, int> numberCountDic = new Dictionary<string, int>();
        private Dictionary<string, string> nameDic = new Dictionary<string, string>();
        private Dictionary<string, string> departmentDic = new Dictionary<string, string>();

        #region UI
        private void btnCount_Click(object sender, EventArgs e)
        {
            ClearData();
            InitAddressBook();
            BeginCount();
        }

        private void txtPath_DragEnter(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            this.filePath = path;
            labelPath.Text = path;
        }
        #endregion UI

        #region 通讯录
        // 初始化通讯录
        private void InitAddressBook()
        {
            string path = Application.StartupPath + "/通讯录.txt";

            if (!File.Exists(path))
            {
                MessageBox.Show("没有找到 通讯录.txt 不自动填写通讯录");
                return;
            }

            List<string> lineList = ZFileUtil.ReadTextFile(path);

            foreach (string line in lineList)
            {
                InitOneAddress(line);
            }
        }

        // 初始化一行通讯录
        private void InitOneAddress(string line)
        {
            if (string.IsNullOrEmpty(line))
                return;

            line = line.Replace("：", ":");
            line = line.Replace(" ", "");

            string[] array = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            string department = array[0];
            string name = array[1];
            string phone = array[2];

            if (nameDic.ContainsKey(phone))
            {
                MessageBox.Show("通讯录的手机号重复:" + phone);
                return;
            }

            nameDic.Add(phone, name);
            departmentDic.Add(phone, department);
        }
        #endregion 通讯录

        #region 主要逻辑
        // 初始化数据
        private void ClearData()
        {
            numberCountDic.Clear();
            nameDic.Clear();
            departmentDic.Clear();
        }

        // 开始统计
        private void BeginCount()
        {
            String[] files = Directory.GetFiles(this.filePath, "*", SearchOption.AllDirectories);
            List<string> formatFiles = FilterPath(files);

            foreach (string path in formatFiles)
            {
                CountOneLine(path);
            }

            labelResult.ForeColor = Color.Red;
            labelResult.Text = "正在统计文件。。。。。。。。。。。。";
            ShowResult();

            labelResult.Text = "正在输出到Excel。。。。。。。。。。。。";
            WriteToExcel();
            MessageBox.Show("处理完毕");

            labelResult.Text = "本次统计文件数量:" + files.Length + " 其中带有电话号码的数量:" + formatFiles.Count;
            labelResult.ForeColor = Color.Blue;
        }
        
        // 过滤没有电话号码的文件
        private List<string> FilterPath(string[] files)
        { 
            List<string> list = new List<string>();

            foreach (string file in files)
            {
                if (HasPhoneNumber(file))
                {
                    list.Add(file);
                }
            }

            return list;
        }

        // 判断是否包含电话号码
        private bool HasPhoneNumber(string path)
        {
            string[] array = path.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string each in array)
            {
                if (each.Length == 11)
                    return true;
            }

            return false;
        }

        // 对一行进行统计
        private void CountOneLine(string line)
        {
            string number = AnalyzeNumber(line);

            if (!numberCountDic.ContainsKey(number))
                numberCountDic.Add(number, 0);

            numberCountDic[number]++;
        }

        // 分析出电话号码
        private string AnalyzeNumber(string line)
        {
            string[] array = line.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string each in array)
            {
                if (each.Length == 11)
                    return each;
            }

            return string.Empty;
        }

        // 显示结果
        private void ShowResult()
        {
            foreach (KeyValuePair<string, int> each in numberCountDic)
            {
                string lineResult = each.Key + " " + each.Value + "次\r\n";
                txtResult.Text += lineResult;
            }
        }

        // 写入到Excel
        private void WriteToExcel()
        {
            DataTable dt = new DataTable("first");

            dt.Columns.Add("姓名", System.Type.GetType("System.String"));
            dt.Columns.Add("部门", System.Type.GetType("System.String"));
            dt.Columns.Add("手机号", System.Type.GetType("System.String"));
            dt.Columns.Add("总计通话次数", System.Type.GetType("System.String"));

            List<KeyValuePair<string, int>> list = SortDictionary(numberCountDic);

            foreach (KeyValuePair<string, int> each in list)
            {
                DataRow dr = dt.NewRow();
                dr["姓名"] = TryGetName(each.Key);
                dr["部门"] = TryGetDepartment(each.Key);
                dr["手机号"] = each.Key;
                dr["总计通话次数"] = each.Value.ToString();
                dt.Rows.Add(dr);
            }

            ToExcel2.SaveToExcel("result.xlsx", dt);
        }

        private List<KeyValuePair<string, int>> SortDictionary(Dictionary<string, int> dic)
        {
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(dic);

            list.Sort(delegate(KeyValuePair<string, int> s1, KeyValuePair<string, int> s2)
            {
                string start = "1380288";
                if (s1.Key.StartsWith(start) && s2.Key.StartsWith(start))
                    return 0;

                if (s1.Key.StartsWith(start) && !s2.Key.StartsWith(start))
                    return -1;

                if (!s1.Key.StartsWith(start) && s2.Key.StartsWith(start))
                    return 1;

                string super1 = "13989907026";

                if (s1.Key.Equals(super1) && s2.Key.Equals(super1))
                    return 0;

                if (s1.Key.Equals(super1) && !s2.Key.Equals(super1))
                    return -1;

                if (!s1.Key.Equals(super1) && s2.Key.Equals(super1))
                    return 1;

                string super2 = "13922220150";

                if (s1.Key.Equals(super2) && s2.Key.Equals(super2))
                    return 0;

                if (s1.Key.Equals(super2) && !s2.Key.Equals(super2))
                    return -1;

                if (!s1.Key.Equals(super2) && s2.Key.Equals(super2))
                    return 1;

                return 0;
            });

            return list;
        }

        // 尝试获取姓名
        private string TryGetName(string phone)
        {
            if (nameDic.ContainsKey(phone))
                return nameDic[phone];

            return string.Empty;
        }

        // 尝试获取部门
        private string TryGetDepartment(string phone)
        {
            if (departmentDic.ContainsKey(phone))
                return departmentDic[phone];

            return string.Empty;
        }
        #endregion 主要逻辑
    }
}
