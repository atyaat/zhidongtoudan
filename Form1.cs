using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace datagridview
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection conn;
        DataTable dt;
        string connectionStr = "server = 10.148.208.25; port = 3306; database = tjdemo; user = smtdsm; password = smtdsm; Sslmode = none;";
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        //保存单击事件
        private void button2_Click(object sender, EventArgs e)
        {
           
               
         }
       

       
      
        private void 发送工单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }
        //查询
        private void button3_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connectionStr);          //创建数据库连接对象
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            string strSQL = "";
            //拼接T-SQL语句
            if (textBox2.Text.Trim() == "")
            {
                strSQL = "SELECT * FROM smt_td2 WHERE 日期 LIKE '%" + dateTimePicker1.Text + "%'ORDER BY '線別' DESC";
            }
            else
            {
                strSQL = "SELECT * FROM smt_td2 WHERE 工單號碼 LIKE '%" + textBox2.Text.Trim() + "%'ORDER BY '線別' ASC";
            }
             
            //创建comm对象
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
            DataSet ds = new DataSet();
            da.Fill(ds, "smt_td2"); //参数1：dataset对象，参数2：表名，自定义的名字，不需要和查询的语句一致

            //绑定数据到dataGridView

            //方法一
            //dataGridView1.DataSource = ds;
            //dataGridView1.DataMember = "students";

            ////方法二
            //dataGridView1.DataSource = ds.Tables["students"];
           
            //方法三
            dt = ds.Tables["smt_td2"];
            dataGridView1.DataSource = dt.DefaultView;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }
        private void buttonDel_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 发送工单ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.Show();
        }

        private void buttKeep_Click(object sender, EventArgs e)
        {

        }
        public static void ThreadProc3()
        {
            Application.Run(new Form3());
        }
        public static void ThreadProc5()
        {
            Application.Run(new Form5());
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
           

            if (Form3.a == 1)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc3));
                thread.SetApartmentState(System.Threading.ApartmentState.STA); //重点
                thread.Start();
                this.Close();
            }
            else
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc5));
                thread.SetApartmentState(System.Threading.ApartmentState.STA); //重点
                thread.Start();
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connectionStr);          //创建数据库连接对象
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            //拼接T-SQL语句
            string strSQL = "SELECT * FROM smt_td2 ORDER BY `日期` DESC";  //日期從大到小排序
            //创建comm对象
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
            DataSet ds = new DataSet();
            da.Fill(ds, "smt_td2"); //参数1：dataset对象，参数2：表名
            dt = ds.Tables["smt_td2"];
            dataGridView1.DataSource = dt.DefaultView;
            //string b = dateTimePicker1.Text.Trim();
            dateTimePicker1.Text = DateTime.Now.ToString();
        }
        ExportToExcel myexport = new ExportToExcel();
        private void button2_Click_1(object sender, EventArgs e)
        {
            myexport.DataGridviewShowToExcel(dataGridView1, true);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
