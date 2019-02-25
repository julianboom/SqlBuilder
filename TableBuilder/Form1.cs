using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TableBuilderDateBase;

namespace TableBuilder
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool a = FourForms.Checked;
            bool b = CaseInfo.Checked;
            string tablename = "";
            if ((a&&!b)||(b&&!a))
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.ShowDialog();
                string path = ofd.FileName;
                textBox1.Text += "您选择的文件是" + path + "\r\n";

                sys_dicService dic = new sys_dicService();
                if (a)
                {
                    tablename = "FourForms";
                }
                else
                {
                    tablename = "CaseInfo";
                }
                List<sys_dic> list = dic.getsysdic(tablename);

                TableFactory tf = new TableFactory();
                try
                {
                    string showmsg = tf.sqlBuilder(path, tablename);
                    textBox1.Clear();
                    textBox1.AppendText(showmsg);
                }
                catch (Exception ex)
                {
                    
                    textBox1.Clear();
                    textBox1.Text = "";
                    textBox1.AppendText("遇到一些不可抗拒力，请联系管理员");
                }

            }
            else
            {
                MessageBox.Show("请选择您要制作的唯一表");

            }

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void FourForms_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CaseInfo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dbConnection_Click(object sender, EventArgs e)
        {
            
            {
                textBox1.Text = "数据库连接中...\r\n";
                bool judge = DateBase.testConnection();
                if (judge)
                {
                    textBox1.Text += "数据库连接成功!\r\n";
                }
                else
                {
                    textBox1.Text += "数据库连接失败，请联系管理员电话15150598302";
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
