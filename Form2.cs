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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //string connectionStr = "server = 10.148.208.25; port = 3306; database = tjdemo; user = smtdsm; password = smtdsm; Sslmode = none;";
        MySqlConnection conn;
        DataTable dt;
        private void buttFend_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text.Trim();

            if (text == null)
            {
                MessageBox.Show("請輸入查詢日期");
            }
            //判断  是否有连接
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            
            //拼接T-SQL语句
            string strSQL = "SELECT * FROM smt_td WHERE 日期 LIKE '%" + textBox1.Text.Trim() + "%'";
            //创建comm对象
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
            DataSet ds = new DataSet();
            da.Fill(ds, "smt_td"); //参数1：dataset对象，参数2：表名，自定义的名字，不需要和查询的语句一致

            //绑定数据到dataGridView

            //方法一
            //dataGridView1.DataSource = ds;
            //dataGridView1.DataMember = "students";

            ////方法二
            //dataGridView1.DataSource = ds.Tables["students"];
            if (text != null)
            {
                //方法三
                dt = ds.Tables["smt_td"];
                dataGridView1.DataSource = dt.DefaultView;
            }
            //方法三
            //dt = ds.Tables["smt_td"];
            //dataGridView1.DataSource = dt.DefaultView;

            conn.Close();
        }
        DataTable a;
       
        private void ChooseFile1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file_name = openFileDialog1.FileName;
                ExcelOptions excelOptions = new ExcelOptions();
                a = excelOptions.ThreadReadExcel(file_name);
                dataGridView1.DataSource = a;

                //for (int i = 0; i < dataGridView1.RowCount; i++) //行的循环
                //{
                //    for (int j = 5; j < dataGridView1.ColumnCount; j += 2) // 列的循环
                //    {
                //        string cell = dataGridView1[j, i].Value.ToString();
                //        if (cell != null)
                //            dataGridView1[j, i].Style.BackColor = Color.Yellow;

                //    }
                //}
                MessageBox.Show("打開成功");
            }
           
           
        }
        //-------------添加行---------------------------
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (dt == null && a == null)
            {
                //没有绑定数据的时候添加行
                dataGridView1.Rows.Add();
                int rc = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[rc].HeaderCell.Value = "A";
            }
            else if (dt != null )
            {
                //绑定数据以后添加行
                DataRow dr = dt.NewRow();  //创建一个和当前绑定表格具有相同架构的行
                int index = dataGridView1.RowCount == 0 ? 0 : dataGridView1.CurrentRow.Index + 1; //三维运算
                dt.Rows.InsertAt(dr, index);                         //把空行插入到指定位置
                dataGridView1.Rows[index].HeaderCell.Value = "A";  // 在行头显示A
            }
            else if (a != null && dt == null)
            {
                DataRow dr = a.NewRow();  //创建一个和当前绑定表格具有相同架构的行
                int index = dataGridView1.RowCount == 0 ? 0 : dataGridView1.CurrentRow.Index + 1; //三维运算
                a.Rows.InsertAt(dr, index);                         //把空行插入到指定位置
                dataGridView1.Rows[index].HeaderCell.Value = "A";  // 在行头显示A
            }
        }

        private void changeTime1_Click(object sender, EventArgs e)
        {

        }

        //------------修改行---------------------
        string strOlder;
        string strNew;
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            strOlder = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            strNew = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (strOlder != strNew)
            {
                if (dataGridView1.Rows[e.RowIndex].HeaderCell.Value == null)
                    dataGridView1.Rows[e.RowIndex].HeaderCell.Value = "U";
                else if (dataGridView1.Rows[e.RowIndex].HeaderCell.Value.ToString() == "D")
                    dataGridView1.Rows[e.RowIndex].HeaderCell.Value = "U";
            }
        }
        //----------------删除行----------------

        
        private void buttDel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (dataGridView1.Rows[row.Index].HeaderCell.Value == null)
                {
                    dataGridView1.Rows[row.Index].HeaderCell.Value = "D";
                }
                else if (dataGridView1.Rows[row.Index].HeaderCell.Value.ToString() == "A")
                {
                    dataGridView1.Rows.Remove(row);  // 参数：选中的行
                }
                else if (dataGridView1.Rows[row.Index].HeaderCell.Value.ToString() == "U")
                {
                    dataGridView1.Rows[row.Index].HeaderCell.Value = "D";
                }
            }
        }
        //-------------确定投单
        int flog = 0;

        private void send2_Click(object sender, EventArgs e)
        {
            flog += 1;
            string strSQL = "";
            //string aaa = dataGridView1.Rows[0].Cells["日期"].Value.ToString();
            //strSQL = "SELECT * FROM smt_td WHERE 日期 = '"+ aaa + "'";
            DialogResult dr = MessageBox.Show("此處需要輸入密碼確認", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (flog == 1 && dr == DialogResult.Yes)
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string a = dataGridView1.Rows[i].Cells["日期"].Value.ToString();
                    string b = dataGridView1.Rows[i].Cells["線別"].Value.ToString();
                    string c = dataGridView1.Rows[i].Cells["機種"].Value.ToString();
                    string d = dataGridView1.Rows[i].Cells["工單號碼"].Value.ToString();
                    string ee = dataGridView1.Rows[i].Cells["工單余數"].Value.ToString();
                    string f = dataGridView1.Rows[i].Cells["需求時間"].Value.ToString();
                    string g = dataGridView1.Rows[i].Cells["需求數量"].Value.ToString();
                    string h = dataGridView1.Rows[i].Cells["需求時間2"].Value.ToString();
                    string ii = dataGridView1.Rows[i].Cells["需求數量2"].Value.ToString();
                    string j = dataGridView1.Rows[i].Cells["需求時間3"].Value.ToString();
                    string k = dataGridView1.Rows[i].Cells["需求數量3"].Value.ToString();
                    string l = dataGridView1.Rows[i].Cells["需求時間4"].Value.ToString();
                    string m = dataGridView1.Rows[i].Cells["需求數量4"].Value.ToString();
                    string n = dataGridView1.Rows[i].Cells["需求時間5"].Value.ToString();
                    string o = dataGridView1.Rows[i].Cells["需求數量5"].Value.ToString();
                    string p = dataGridView1.Rows[i].Cells["備註"].Value.ToString();
                    string q = dataGridView1.Rows[i].Cells["狀態"].Value.ToString();

                    //string strSQL = "";
                    strSQL = @"INSERT INTO smt_td  
                                        (日期,線別,機種,工單號碼,工單余數,需求時間,需求數量,需求時間2,需求數量2,需求時間3,需求數量3,需求時間4,需求數量4,需求時間5,需求數量5,備註,狀態)
                                     VALUES
                                         ('" + a + "' ,'" + b + "','" + c + "','" + d + "' ,'" + ee + "' ,'" + f + "','" + g + "','" + h + "','" + ii + "','" + j + "','" + k + "','" + l + "' ,'" + m + "','" + n + "','" + o + "','" + p + "','" + q + "')";


                    if (strSQL != "")
                    {
                        if (conn.State == System.Data.ConnectionState.Closed)
                            conn.Open();
                        MySqlCommand comm = new MySqlCommand(strSQL, conn);
                        comm.ExecuteNonQuery();
                    }

                }
                MessageBox.Show("投單成功");
            }
            else if (dr == DialogResult.No)
            {
                MessageBox.Show("投單失敗");
            }
            else
            {
                MessageBox.Show("請勿重複投單");
            }
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ////连接数据库 using
            ////新建连接的对象字符串
            //MySqlConnectionStringBuilder scsb = new MySqlConnectionStringBuilder();
            //scsb.Server = "localhost";
            //scsb.UserID = "root";
            //scsb.Password = "dim123";
            //scsb.Database = "demo";

            //// 实例化 MySqlConnection 对象           创建连接
            //conn = new MySqlConnection(scsb.ToString()); //参数是连接数据库的字符

            string connectionStr = "server = 10.148.208.25; port = 3306; database = tjdemo; user = smtdsm; password = smtdsm; Sslmode = none;";
                                     // 数据可IP地址          端口         数据库名称            用户名         密码   
             conn = new MySqlConnection(connectionStr);          //创建数据库连接对象
            conn.Open();
        }
        // ------------------------ 保存修改------------------------------------
        private void buttStay_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("此處需要輸入密碼確認", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)  //循环每一行，将数据都
                {
                    string strSQL = "";

                    string a = dataGridView1.Rows[i].Cells["日期"].Value.ToString();
                    string b = dataGridView1.Rows[i].Cells["線別"].Value.ToString();
                    string c = dataGridView1.Rows[i].Cells["機種"].Value.ToString();
                    string d = dataGridView1.Rows[i].Cells["工單號碼"].Value.ToString();
                    string ee = dataGridView1.Rows[i].Cells["工單余數"].Value.ToString();
                    string f = dataGridView1.Rows[i].Cells["需求時間"].Value.ToString();
                    string g = dataGridView1.Rows[i].Cells["需求數量"].Value.ToString();
                    string h = dataGridView1.Rows[i].Cells["需求時間2"].Value.ToString();
                    string ii = dataGridView1.Rows[i].Cells["需求數量2"].Value.ToString();
                    string j = dataGridView1.Rows[i].Cells["需求時間3"].Value.ToString();
                    string k = dataGridView1.Rows[i].Cells["需求數量3"].Value.ToString();
                    string l = dataGridView1.Rows[i].Cells["需求時間4"].Value.ToString();
                    string m = dataGridView1.Rows[i].Cells["需求數量4"].Value.ToString();
                    string n = dataGridView1.Rows[i].Cells["需求時間5"].Value.ToString();
                    string o = dataGridView1.Rows[i].Cells["需求數量5"].Value.ToString();
                    string p = dataGridView1.Rows[i].Cells["備註"].Value.ToString();
                    string q = dataGridView1.Rows[i].Cells["狀態"].Value.ToString();

                    if (dataGridView1.Rows[i].HeaderCell.Value == null)
                    {
                        strSQL = "";
                    }
                    else if (dataGridView1.Rows[i].HeaderCell.Value.ToString() == "A")       // Add
                    {
                        //// 读取dataGridView1单元格的数据
                        //string stu_no = dataGridView1.Rows[i].Cells["stu_no"].Value.ToString(); //方法一
                        //stu_no = dataGridView1["stu_no", i].Value.ToString();//方法二
                        ////strSQL = "insert into ......";

                        strSQL = @"INSERT INTO smt_td  
                                        (日期,線別,機種,工單號碼,工單余數,需求時間,需求數量,需求時間2,需求數量2,需求時間3,需求數量3,需求時間4,需求數量4,需求時間5,需求數量5,備註,狀態)
                                     VALUES
                                         ('" + a + "' ,'" + b + "','" + c + "','" + d + "' ,'" + ee + "' ,'" + f + "','" + g + "','" + h + "','" + ii + "','" + j + "','" + k + "','" + l + "' ,'" + m + "','" + n + "','" + o + "','" + p + "','" + q + "')";


                    }
                    else if (dataGridView1.Rows[i].HeaderCell.Value.ToString() == "D")
                    {
                        strSQL = "delete from smt_td WHERE 序号 = '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                    }
                    else       //修改
                    {
                        strSQL = "UPDATE smt_td SET 日期 =' " + a + " ',線別 = ' " + b + "',機種 = ' " + c + "',工單號碼 = ' " + d + "',工單余數 = ' " + ee + "',需求時間 = ' " + f + "',需求數量 = ' " + g + "',需求時間2 = ' " + h + "',需求數量2 = ' " + ii + "',需求時間3 = ' " + j + " ',需求數量3 = ' " + k + "',需求時間4 = ' " + l + "',需求數量4 = ' " + m + "',需求時間5 = ' " + n + "',需求數量5 = ' " + o + "',備註 = ' " + p + "', 狀態 = ' " + q + "'  WHERE 序号 = '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                    }

                    if (strSQL != "")
                    {
                        if (conn.State == System.Data.ConnectionState.Closed)
                            conn.Open();
                        MySqlCommand comm = new MySqlCommand(strSQL, conn);
                        comm.ExecuteNonQuery();
                    }
                }
            }
            //else if (dr == DialogResult.No)
            //{ 
            
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 查询工单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }
    }
}
