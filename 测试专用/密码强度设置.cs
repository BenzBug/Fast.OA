using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 测试专用
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            //var v =$("#text").val();
            //Console.WriteLine("请输入");
            string msg = txtStr.Text;
            bool b = Regex.IsMatch(msg, "^(?=.*[a-zA-Z])(?=.*[1-9])(?=.*[\\W]).{8,}$");
            if (!b)
            {
                label1.Text =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+ "必须是8位以上，且包含字母数字和特殊符号！";
            }
            else
            {
                label1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "密码符合要求";
            }
            //reg =/^ (?=.*[a - zA - Z])(?=.*[1 - 9])(?=.*[\W]).{ 6,}$/;
            //alert(reg.test(v));
        }
    }
}
