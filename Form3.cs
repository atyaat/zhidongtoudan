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
using Microsoft.VisualBasic;
using System.Data.OleDb;

namespace datagridview
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        MySqlConnection conn;   //数据库链接对象
        MySqlCommand comm;      //SQL语句执行对象
        DataTable dt;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 打開投單界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContextMenuStrip form = new ContextMenuStrip();
            form.Show();
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            textBoxshuax.Text = "1";
            textBoxshuax.Visible = false;
            button2.BackColor = Color.Aquamarine;

            textTime.Text = DateTime.Now.Date.ToString("yyyy年M月d日");
            //textTime.Text = DateTime.Now.Date.ToString("MM/dd");  //显示日期
            //DateTime.Now.Date.ToString("HH:mm");  //显示具体时间

            string connectionStr = "server = 10.148.208.25; port = 3306; database = tjdemo; user = smtdsm; password = smtdsm; Sslmode = none;";
            // 数据可IP地址          端口         数据库名称            用户名         密码   
            conn = new MySqlConnection(connectionStr);          //创建数据库连接对象
            conn.Open();

            string strSQL = "SELECT 序號,線別,工單號碼,需求時間,需求數量,已完成數量 FROM smt_td2  WHERE 日期 LIKE '" + textTime.Text.Trim() + "' AND 狀態 LIKE '1' ORDER BY `線別` ASC";
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
            DataSet ds = new DataSet();
            da.Fill(ds, "smt_td2"); //参数1：dataset对象，参数2：表名，
            dt = ds.Tables["smt_td2"];
            dataGridView1.DataSource = dt.DefaultView;

            strSQL = "SELECT 線別,工單號碼,需求時間,需求數量 FROM smt_td2  WHERE 日期 LIKE '" + textTime.Text.Trim() + "' AND 狀態 LIKE '0' ORDER BY `線別` ASC";
            da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
            ds = new DataSet();
            da.Fill(ds, "smt_td2"); //参数1：dataset对象，参数2：表名，
            dt = ds.Tables["smt_td2"];
            dataGridView2.DataSource = dt.DefaultView;

            conn.Close();
        }
        //跳转Form4
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Form5 frm = new Form5();
            this.Hide();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        //发送工单
        
        /// <summary>
        ///  if 机种为空
        ///     带出机种和工单总数
        ///  else if 全部填满
        ///     将工单写入数据库
        ///     
        /// </summary>
        string strSQL;
    
        private void butt_Click(object sender, EventArgs e)
        {
            string a = textTime.Text.Trim();         //日期
            string b = comboBoxLine.Text.Trim();     //线别
            string c = textBox3.Text.Trim();         //機種
            string d = textBoxPn.Text.Trim();        //工单号码
            string ee = textTime2.Text.Trim();       // 需求时间
            string f = textBoxNum.Text.Trim();       //需求数量
            string g = textBoxnum2.Text.Trim();      //工單總數
            string i = textBoxSMT.Text.Trim();         //SMT工單總數

            if (c == "" || ee == "")
            {
                Httppost_getModel model_get = new Httppost_getModel();
                string[] h = model_get.get_Model(textBoxPn.Text.Trim());    //参数：工单号码

                textBoxnum2.Text = h[0];
                textBox3.Text = h[1];


            }
            else if (a != "" && b != "" && c != "" && d != "" && ee != "" && f != "" && g != "" && i != "")
            {
                string eee = "";
                //var index = ee.IndexOf(":");
                //if (index == 2)
                //{
                //    eee = ee.Substring(0, 2) + ":" + ee.Substring(ee.Length - 2, 2); //将时间输入框的前两位和后两位组合
                //}
                //else if (index == 1)
                //{
                //    eee = ee.Substring(0, 1) + ":" + ee.Substring(ee.Length - 2, 2); //将时间输入框的前两位和后两位组合
                //}
              
                eee = ee.Substring(0, 2) + ":" + ee.Substring(ee.Length - 2, 2); //将时间输入框的前两位和后两位组合

                var index = ee.IndexOf(":");
                if (index == 1)
                {
                    eee = "0" + ee.Substring(0, 1) + ":" + ee.Substring(2, 2);
                }

                richTextBox1.Font = new Font("宋体", 12, FontStyle.Regular);

                strSQL = "SELECT * FROM smt_td2 WHERE(線別) LIKE('" + b + "') AND 日期 LIKE '" + a + "' AND 需求時間 LIKE '" + eee + "'";
                MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
                DataSet ds = new DataSet();
                da.Fill(ds, "smt_td2");                  //参数1：dataset对象，参数2：表名
                if (ds.Tables[0].Rows.Count > 0)         //如果查询到数据库有一样的数据行，显示发生失败
                {
                    richTextBox1.Text = a + "\n" + b + "\n" + c + "\n" + d + "\n" + ee + "\n" + f + "\n" + "發現重複的需求工單\n" + "發送失敗";
                }
                else                                     // 没有相同的数据行，就执行，添加数据
                {
                    strSQL = @"INSERT INTO smt_td2  
                                        (日期,線別,機種,工單號碼,工單總數,SMT工單號碼,需求時間,需求數量,狀態)
                                     VALUES
                                         ('" + a + "' ,'" + b + "','" + c + "','" + d + "' ,'" + g + "','" + i + "','" + eee + "','" + f + "','1' )";
                    if (conn.State == System.Data.ConnectionState.Closed)  //如果数据库链接没有打开
                        conn.Open();
                    comm = new MySqlCommand(strSQL, conn);
                    comm.ExecuteNonQuery();

                    richTextBox1.Text = a + "\n" + b + "\n" + c + "\n" + d + "\n" + ee + "\n" + f + "\n" + "工單發送成功";   //显示工单发送成功
                    conn.Close();
                }
                // 清除文本
                
                textTime2.Clear();
                textBoxNum.Clear();
               
            }
            else
            {
                richTextBox1.Text =  "工單信息不完整\n" + "發送失敗";
            }
           

        }
        //双击后显示工单号码对应的机种号
        private void textBox3_DoubleClick(object sender, EventArgs e)
        {
            //Httppost_getModel model_get = new Httppost_getModel();
            //textBox3.Text = model_get.get_Model(textBoxPn.Text.Trim());    //参数：工单号码
        }

        private void textBoxPn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)   //如果输入的不是数字，显示提示
            {
                e.Handled = true;
            }
            else
                richTextBox1.Text = "請出入數字！";
        }
        //定时刷新显示，当天状态为1和0的数据行的数量
        private void timer1_Tick(object sender, EventArgs e)
        {
            strSQL = "SELECT * FROM smt_td2 WHERE(日期) LIKE('"+ textTime.Text.Trim() +  "') AND 狀態 LIKE '1'";
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
            DataSet ds = new DataSet();
            da.Fill(ds, "smt_td2");
            textBox1.Text = ds.Tables[0].Rows.Count.ToString();

            strSQL = "SELECT * FROM smt_td2 WHERE(日期) LIKE('" + textTime.Text.Trim() + "') AND 狀態 LIKE '0'";
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
            ds = new DataSet();
            da.Fill(ds, "smt_td2");
            textBox2.Text = ds.Tables[0].Rows.Count.ToString();

            if (textBoxshuax.Text == "1")
            {
                string strSQL = "SELECT 序號,線別,工單號碼,需求時間,需求數量,已完成數量 FROM smt_td2  WHERE 日期 LIKE '" + textTime.Text.Trim() + "' AND 狀態 LIKE '1' ORDER BY `線別` ASC";
                da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
                ds = new DataSet();
                da.Fill(ds, "smt_td2"); //参数1：dataset对象，参数2：表名，
                dt = ds.Tables["smt_td2"];
                dataGridView1.DataSource = dt.DefaultView;

                strSQL = "SELECT 線別,工單號碼,需求時間,需求數量 FROM smt_td2  WHERE 日期 LIKE '" + textTime.Text.Trim() + "' AND 狀態 LIKE '0' ORDER BY `線別` ASC";
                da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
                ds = new DataSet();
                da.Fill(ds, "smt_td2"); //参数1：dataset对象，参数2：表名，
                dt = ds.Tables["smt_td2"];
                dataGridView2.DataSource = dt.DefaultView;
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form6 frm = new Form6();
            this.Hide();
            frm.Show();
        }

        // 刪除數據   在要刪除的行前面標記D
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
        // 修改數據 在將要修改的行前面標記U
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            password frm = new password();
            frm.ShowDialog();
            if (password.a == 1)
            {

                string strSQL = "";
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    string a = dataGridView1.Rows[i].Cells["線別"].Value.ToString();
                    string b = dataGridView1.Rows[i].Cells["工單號碼"].Value.ToString();
                    string c = dataGridView1.Rows[i].Cells["需求時間"].Value.ToString();
                    string d = dataGridView1.Rows[i].Cells["需求數量"].Value.ToString();

                    if (dataGridView1.Rows[i].HeaderCell.Value == null)
                    {
                        strSQL = "";
                    }
                    else if (dataGridView1.Rows[i].HeaderCell.Value.ToString() == "D")  // 行首标记为 D 将此行删除
                    {
                        strSQL = "delete from smt_td2 WHERE 序號 = '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                    }
                    else       // 行首标记为 U 修改此行         
                    {
                        strSQL = "UPDATE smt_td2 SET 線別 = '" + a + "',工單號碼 = '" + b + "',需求時間 = '" + c + "',需求數量 = '" + d + "'  WHERE 序號 = '" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                    }
                    if (strSQL != "") // 执行SQL语句
                    {
                        if (conn.State == System.Data.ConnectionState.Closed)
                            conn.Open();
                        MySqlCommand comm = new MySqlCommand(strSQL, conn);
                        comm.ExecuteNonQuery();
                    }


                }
                //修改完后刷新显示
                strSQL = "SELECT 序號,線別,工單號碼,需求時間,需求數量,已完成數量 FROM smt_td2  WHERE 日期 LIKE '" + textTime.Text.Trim() + "' AND 狀態 LIKE '1' ORDER BY `需求時間` ASC";
                MySqlDataAdapter da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
                DataSet ds = new DataSet();
                da.Fill(ds, "smt_td2"); //参数1：dataset对象，参数2：表名
                dt = ds.Tables["smt_td2"];
                dataGridView1.DataSource = dt.DefaultView;
                textBoxshuax.Text = "1";
                button2.BackColor = Color.Aquamarine;
            }
           
            
        }
        //修改
        private void button4_Click(object sender, EventArgs e)
        {
            textBoxshuax.Text = "0";
            button2.BackColor = Color.Orange;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPn_TextChanged(object sender, EventArgs e)
        {

        }
      
        private void button3_Click_1(object sender, EventArgs e)
        {
            password frm = new password();
            frm.ShowDialog();
            if (password.a == 1)
            {
                richTextBox1.Text = "成功";
            }
        }
        public static int a = 0;
        public static void ThreadProc()
        {
            Application.Run(new Form1());
        }
        private void button3_Click_2(object sender, EventArgs e)
        {
            a = 1;

            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            thread.SetApartmentState(System.Threading.ApartmentState.STA); //重点
            thread.Start();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public static DataTable ReadExcelToTable(string path)//excel存放的路径
        {
            try
            {

                //连接字符串
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意
                //string connstring = Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; //Office 07以下版本 
                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串                    //string sql = string.Format("SELECT * FROM [{0}] WHERE [日期] is not null", firstSheetName); //查询字符串
                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    return set.Tables[0];
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        DataTable aa;
        private void buttonexcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file_name = openFileDialog1.FileName;
                ExcelOptions excelOptions = new ExcelOptions();
                aa = excelOptions.ThreadReadExcel(file_name);
                //aa = ReadExcelToTable(file_name);
                //dataGridView1.DataSource = aa;

                //for (int i = 0; i < dataGridView1.RowCount; i++) //行的循环
                //{
                //    for (int j = 5; j < dataGridView1.ColumnCount; j += 2) // 列的循环
                //    {
                //        string cell = dataGridView1[j, i].Value.ToString();
                //        if (cell != null)
                //            dataGridView1[j, i].Style.BackColor = Color.Yellow;

                //    }
                //}
                //for (int i = 0; i < aa.Tables[0].Rows.Count; i++)
                //{
                //    string d = ds.Tables[0].Rows[i][0].ToString();
                //}
                //    string a = ds.Tables[0].Rows[i][0].ToString();
                //aa.Rows.RemoveAt(0);
                string bbb = aa.Rows[0][1].ToString();   // bbb 表0行1列的数据（不包括列名）
                string bbbB = aa.Columns.Count.ToString();          // 获取列数
                string bbbbb = aa.Rows[1]["工單號碼"].ToString();   //获取1行 列名为“工单号码”的数据
                 
                int a = Convert.ToInt32(bbbB);
                int c = Convert.ToInt32(aa.Rows.Count.ToString());  //行數
                for ( int i = 0 ; i < c; i++)
                {
                    textBoxPn.Text = aa.Rows[i]["工單號碼"].ToString();
                    comboBoxLine.Text = aa.Rows[i]["線別"].ToString();
                    butt_Click(null, null);
                    textBoxSMT.Text = aa.Rows[i]["SMT工單號碼"].ToString();
                    textTime2.Text = aa.Rows[i]["需求時間"].ToString();
                    textBoxNum.Text = aa.Rows[i]["需求數量"].ToString();
                    butt_Click(null , null);
                    try
                    {
                        if (aa.Rows[i]["需求時間2"].ToString() != "")
                        {
                            textBoxPn.Text = aa.Rows[i]["工單號碼"].ToString();
                            comboBoxLine.Text = aa.Rows[i]["線別"].ToString();
                            butt_Click(null, null);
                            textBoxSMT.Text = aa.Rows[i]["SMT工單號碼"].ToString();
                            textTime2.Text = aa.Rows[i]["需求時間2"].ToString();
                            textBoxNum.Text = aa.Rows[i]["需求數量2"].ToString();
                            butt_Click(null, null);
                        }
                    }
                    catch
                    { }
                   
                    try
                    {
                        if (aa.Rows[i]["需求時間3"].ToString() != "")
                        {
                            textBoxPn.Text = aa.Rows[i]["工單號碼"].ToString();
                            comboBoxLine.Text = aa.Rows[i]["線別"].ToString();
                            butt_Click(null, null);
                            textBoxSMT.Text = aa.Rows[i]["SMT工單號碼"].ToString();
                            textTime2.Text = aa.Rows[i]["需求時間3"].ToString();
                            textBoxNum.Text = aa.Rows[i]["需求數量3"].ToString();
                            butt_Click(null, null);
                        }
                    }
                    catch
                    { }
                
                    try
                    {
                        if (aa.Rows[i]["需求時間4"].ToString() != "")
                        {
                            textBoxPn.Text = aa.Rows[i]["工單號碼"].ToString();
                            comboBoxLine.Text = aa.Rows[i]["線別"].ToString();
                            butt_Click(null, null);
                            textBoxSMT.Text = aa.Rows[i]["SMT工單號碼"].ToString();
                            textTime2.Text = aa.Rows[i]["需求時間4"].ToString();
                            textBoxNum.Text = aa.Rows[i]["需求數量4"].ToString();
                            butt_Click(null, null);
                        }
                    }
                    catch
                    { }
                
                    try
                    {
                        if (aa.Rows[i]["需求時間5"].ToString() != "")
                        {
                            textBoxPn.Text = aa.Rows[i]["工單號碼"].ToString();
                            comboBoxLine.Text = aa.Rows[i]["線別"].ToString();
                            butt_Click(null, null);
                            textBoxSMT.Text = aa.Rows[i]["SMT工單號碼"].ToString();
                            textTime2.Text = aa.Rows[i]["需求時間5"].ToString();
                            textBoxNum.Text = aa.Rows[i]["需求數量5"].ToString();
                            butt_Click(null, null);
                        }
                    }
                    catch
                    { }
                 
                    try
                    {
                        if (aa.Rows[i]["需求時間6"].ToString() != "")
                        {
                            textBoxPn.Text = aa.Rows[i]["工單號碼"].ToString();
                            comboBoxLine.Text = aa.Rows[i]["線別"].ToString();
                            butt_Click(null, null);
                            textBoxSMT.Text = aa.Rows[i]["SMT工單號碼"].ToString();
                            textTime2.Text = aa.Rows[i]["需求時間6"].ToString();
                            textBoxNum.Text = aa.Rows[i]["需求數量6"].ToString();
                            butt_Click(null, null);
                        }
                    }
                    catch
                    { }

                   

                }


                MessageBox.Show("導入成功");

                
            }
        }

        private void textTime2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
