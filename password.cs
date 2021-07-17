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
    public partial class password : Form
    {
        public password()
        {
            InitializeComponent();
        }
        public static int a = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            
            string password = textBox1.Text;
            if ("123".Equals(password))
            {
                a = 1;
                
               
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入正确的密码");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void password_Load(object sender, EventArgs e)
        {
            
        }
    }
}
