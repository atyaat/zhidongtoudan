using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datagridview
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void buttEsc_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //登入设置
        public static void ThreadProc()
        {
            Application.Run(new Form3());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string password = textBox2.Text;

            if ( "37807620".Equals(id)|| "42295433".Equals(id)||"37784501".Equals(id)|| "42182020".Equals(id) || "31939449".Equals(id) || "31572257".Equals(id))
            {
                id = "32385129";
            }

            if ("32385129".Equals(id) && "123".Equals(password))
            {
                if (radioButton1.Checked)
                {
                    MessageBox.Show("登入投单系统");
                    //Form3 frm = new Form3();

                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                    thread.SetApartmentState(System.Threading.ApartmentState.STA); //重点
                    thread.Start();
                    this.Close();

                    //frm.Show();
                }
                else if (radioButton2.Checked)
                {
                    MessageBox.Show("登入确认投单系统");
                    Form5 frm = new Form5();
                    this.Hide();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("请选择登入界面");
                }
            }
            else
            {
                MessageBox.Show("请输入正确的账号密码");
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
