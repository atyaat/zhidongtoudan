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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            //以下用于窗口自适应
            x = this.Width;
            y = this.Height;
            setTag(this);
        }
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        private float xx, yy; //用來接收窗口放大縮小的倍數
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    xx = newx;
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    yy = newy;
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = 1 + System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                   
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    richTextBox1.Font = new Font("宋体", 16 * newy, FontStyle.Regular);
                    richTextBox2.Font = new Font("宋体", 16 * newy, FontStyle.Regular);
                    richTextBox3.Font = new Font("宋体", 16 * newy, FontStyle.Regular);
                    richTextBox4.Font = new Font("宋体", 16 * newy, FontStyle.Regular);
                    richTextBox5.Font = new Font("宋体", 16 * newy, FontStyle.Regular);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                
                }
            }
        }
        private void Form5_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
        }


        MySqlConnection conn;

        private void Form5_Load(object sender, EventArgs e)
        {


            textTime.Text = DateTime.Now.Date.ToString("yyyy年M月d日"); //显示当天的日期    、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、
            string connectionStr = "server = 10.148.208.25; port = 3306; database = tjdemo; user = smtdsm; password = smtdsm; Sslmode = none;";
            // 数据可IP地址          端口         数据库名称            用户名         密码   
            conn = new MySqlConnection(connectionStr);          //创建数据库连接对象
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            richTextBox1.Font = new Font("宋体", 16, FontStyle.Regular);
            richTextBox2.Font = new Font("宋体", 16, FontStyle.Regular);
            richTextBox3.Font = new Font("宋体", 16, FontStyle.Regular);
            richTextBox4.Font = new Font("宋体", 16, FontStyle.Regular);
            richTextBox5.Font = new Font("宋体", 16, FontStyle.Regular);
            Show_text();

        } 

        // 顯示需求工單函數 
        private void Show_text()
        {

        
            DataSet ds;
            string Line = comboBoxLine.Text.Trim();
            string T = textTime.Text.Trim();
            string strSQL = "";

            int H = DateTime.Now.Hour;



            string strSQL_num = "SELECT 狀態,count(*) T FROM smt_td2 WHERE 日期 = '" + textTime.Text.Trim() + "'  GROUP BY 狀態";
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(strSQL_num, conn); //参数1：SQL语句；参数2：数据库连接对象
            DataSet dn = new DataSet();
            da.Fill(dn, "smt_td2");

            try
            {
                textBox1.Text = dn.Tables[0].Rows[1][1].ToString();
                textBox2.Text = dn.Tables[0].Rows[0][1].ToString();
            }
            catch
            {

            }
            

            if (7 < H && H < 20)
            {
                text_banc.Text = "白班";
                if (Line == "")   //如果线别的选项为空，显示所有的需求工单，并按照需求时间排序
                {
                    strSQL = "SELECT 線別,工單號碼,SMT工單號碼,工單總數,需求時間,需求數量,已完成數量 FROM smt_td2  WHERE 日期 = '"+T+ "' AND 需求時間 between '08:00 'AND '23:00' AND 狀態 = '1' ORDER BY `需求時間` ASC";
                }
                else  // 否则显示这条线别的需求工单
                {
                    strSQL = "SELECT 線別,工單號碼,SMT工單號碼,工單總數,需求時間,需求數量,已完成數量 FROM smt_td2  WHERE 日期 = '" + T + "'AND 線別 LIKE '" + Line + "' AND 需求時間 between '08:00 'AND '23:00' AND 狀態 = '1' ORDER BY `需求時間` ASC";
                   
                }
            }
            else 
            {
                text_banc.Text = "夜班";
                if (H < 8)
                {
                    textTime.Text = DateTime.Now.AddDays(-1).Date.ToString("yyyy年M月d日"); //显示当天的前一天   、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、

                }
               
                if (Line == "")   //如果线别的选项为空，显示所有的需求工单，并按照需求时间排序
                {
                    strSQL = "SELECT 線別,工單號碼,SMT工單號碼,工單總數,需求時間,需求數量,已完成數量 FROM smt_td2  WHERE 日期 = '"+T+"'AND 狀態 = '1' ORDER BY `需求時間` ASC";
                }
                else  // 否则显示这条线别的需求工单
                {
                    strSQL = "SELECT 線別,工單號碼,SMT工單號碼,工單總數,需求時間,需求數量,已完成數量 FROM smt_td2  WHERE 日期 = '" + T + "'AND 線別 LIKE '" + Line + "'AND 狀態 = '1' ORDER BY `需求時間` ASC";
                    
                }

               
            }
           
           
            da = new MySqlDataAdapter(strSQL, conn); //参数1：SQL语句；参数2：数据库连接对象
            ds = new DataSet();
            da.Fill(ds, "smt_td2");
            int hang = ds.Tables[0].Rows.Count;
            
            if (ds.Tables[0].Rows.Count == 0)   //如果没有找到需求的数据行，将5个显示都清除
            {
                richTextBox1.BackColor = Color.Aquamarine;
                richTextBox2.BackColor = Color.Aquamarine;
                richTextBox3.BackColor = Color.Aquamarine;
                richTextBox4.BackColor = Color.Aquamarine;
                richTextBox5.BackColor = Color.Aquamarine;
                richTextBox1.Clear();
                richTextBox2.Clear();
                richTextBox3.Clear();
                richTextBox4.Clear();
                richTextBox5.Clear();
            }
            else           //如果有数据 按照需求时间排序显示，显示前先将前一个数据清除
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) 
                {
                    string a = ds.Tables[0].Rows[i][0].ToString();
                    string b = ds.Tables[0].Rows[i][1].ToString();
                    string c = ds.Tables[0].Rows[i][2].ToString();
                    string d = ds.Tables[0].Rows[i][3].ToString();
                    string ee = ds.Tables[0].Rows[i][4].ToString();
                    string f = ds.Tables[0].Rows[i][5].ToString();
                    string g = ds.Tables[0].Rows[i][6].ToString();

                    string ff = "線別 ：" + a + "\n" + "工單 ：" + "\n" + b + "\n" + c + "\n" + "工單總數 ：" + d + "\n" + "時間 ：" + " " + ee + " \n" + "數量 ： " + "" +  f+ " \n已完成："+g +"";

                   

                    switch (i)
                    {
                        case 0:                 //第一个显示框
                            richTextBox1.Clear();
                          
                            richTextBox1.Text = ff;

                            back_color(richTextBox1);
                            if (hang == 1)
                            {
                                richTextBox2.Clear();
                                richTextBox3.Clear();
                                richTextBox4.Clear();
                                richTextBox5.Clear();
                                richTextBox2.BackColor = Color.Aquamarine;
                                richTextBox3.BackColor = Color.Aquamarine;
                                richTextBox4.BackColor = Color.Aquamarine;
                                richTextBox5.BackColor = Color.Aquamarine;
                            }
                            break;
                        case 1:                 //第二个显示框
                            richTextBox2.Clear();
                            
                            richTextBox2.Text = ff;
                            back_color(richTextBox2);
                            if (hang == 2)
                            {
                                richTextBox3.Clear();
                                richTextBox4.Clear();
                                richTextBox5.Clear();
                                richTextBox3.BackColor = Color.Aquamarine;
                                richTextBox4.BackColor = Color.Aquamarine;
                                richTextBox5.BackColor = Color.Aquamarine;
                            }
                            break;
                        case 2:                 //第三个显示框
                            richTextBox3.Clear();
                            
                            richTextBox3.Text = ff;
                            back_color(richTextBox3);
                            {
                                richTextBox4.Clear();
                                richTextBox5.Clear();
                                richTextBox4.BackColor = Color.Aquamarine;
                                richTextBox5.BackColor = Color.Aquamarine;
                            }
                            break;
                        case 3:
                            richTextBox4.Clear();   //第四个显示框
                            
                            richTextBox4.Text = ff;
                            back_color(richTextBox4);
                            if (hang == 4)
                            {
                                richTextBox5.Clear();
                                richTextBox5.BackColor = Color.Aquamarine;
                            }
                            break;
                        case 4:
                            richTextBox5.Clear();    //第五个显示框
                            richTextBox5.Text = ff;
                            back_color(richTextBox5);
                            break;

                    }

                }
            }
           
        }
        // 背景色设置 根据时间
        //private void back_color1()
        //{

        //string  H = DateTime.Now.Hour.ToString();
        //string mm = DateTime.Now.Minute.ToString();
        //string b = richTextBox1.Lines[5]; ;     // 需求时间
        //string bb = b.Substring(0, 2);       //前两位
        //string bbb = b.Substring(b.Length - 2, 2);  //后两位

        //decimal h = decimal.Parse(H);
        //decimal M = decimal.Parse(mm);
        //decimal B = decimal.Parse(bb);
        //decimal BB = decimal.Parse(bbb);

        //int c = ((int)B*60+(int)BB)-((int)h * 60 + (int)M);
        //if (c - 30 < 0)
        //{
        //    richTextBox1.BackColor = Color.Salmon;
        //}
        //else if (c - 60 < 0)
        //{
        //    richTextBox1.BackColor = Color.Orange;
        //}
        //else if (c - 90 < 0)
        //{
        //    richTextBox1.BackColor = Color.Khaki;
        //}
        //else
        //{
        //    richTextBox1.BackColor = Color.Aquamarine;
        //}
        //}
        //private void back_color2()
        //{
        //    string H = DateTime.Now.Hour.ToString();
        //    string mm = DateTime.Now.Minute.ToString();
        //    string b = richTextBox2.Lines[5]; ;     // 需求时间
        //    string bb = b.Substring(0, 2);       //前两位
        //    string bbb = b.Substring(b.Length - 2, 2);  //后两位

        //    decimal h = decimal.Parse(H);
        //    decimal M = decimal.Parse(mm);
        //    decimal B = decimal.Parse(bb);
        //    decimal BB = decimal.Parse(bbb);

        //    int c = ((int)B * 60 + (int)BB) - ((int)h * 60 + (int)M);
        //    if (c - 30 < 0)
        //    {
        //        richTextBox2.BackColor = Color.Salmon;
        //    }
        //    else if (c - 60 < 0)
        //    {
        //        richTextBox2.BackColor = Color.Orange;
        //    }
        //    else if (c - 90 < 0)
        //    {
        //        richTextBox2.BackColor = Color.Khaki;
        //    }
        //    else
        //    {
        //        richTextBox2.BackColor = Color.Aquamarine;
        //    }
        //}
        //private void back_color3()
        //{
        //    string H = DateTime.Now.Hour.ToString();
        //    string mm = DateTime.Now.Minute.ToString();
        //    string b = richTextBox3.Lines[5]; ;     // 需求时间
        //    string bb = b.Substring(0, 2);       //前两位
        //    string bbb = b.Substring(b.Length - 2, 2);  //后两位

        //    decimal h = decimal.Parse(H);
        //    decimal M = decimal.Parse(mm);
        //    decimal B = decimal.Parse(bb);
        //    decimal BB = decimal.Parse(bbb);

        //    int c = ((int)B * 60 + (int)BB) - ((int)h * 60 + (int)M);
        //    if (c - 30 < 0)
        //    {
        //        richTextBox3.BackColor = Color.Salmon;
        //    }
        //    else if (c - 60 < 0)
        //    {
        //        richTextBox3.BackColor = Color.Orange;
        //    }
        //    else if (c - 90 < 0)
        //    {
        //        richTextBox3.BackColor = Color.Khaki;
        //    }
        //    else
        //    {
        //        richTextBox3.BackColor = Color.Aquamarine;
        //    }
        //}
        //private void back_color4()
        //{
        //    string H = DateTime.Now.Hour.ToString();
        //    string mm = DateTime.Now.Minute.ToString();
        //    string b = richTextBox4.Lines[5]; ;     // 需求时间
        //    string bb = b.Substring(0, 2);       //前两位
        //    string bbb = b.Substring(b.Length - 2, 2);  //后两位

        //    decimal h = decimal.Parse(H);
        //    decimal M = decimal.Parse(mm);
        //    decimal B = decimal.Parse(bb);
        //    decimal BB = decimal.Parse(bbb);

        //    int c = ((int)B * 60 + (int)BB) - ((int)h * 60 + (int)M);
        //    if (c - 30 < 0)
        //    {
        //        richTextBox4.BackColor = Color.Salmon;
        //    }
        //    else if (c - 60 < 0)
        //    {
        //        richTextBox4.BackColor = Color.Orange;
        //    }
        //    else if (c - 90 < 0)
        //    {
        //        richTextBox4.BackColor = Color.Khaki;
        //    }
        //    else
        //    {
        //        richTextBox4.BackColor = Color.Aquamarine;
        //    }
        //}
        //private void back_color5()
        //{
        //    string H = DateTime.Now.Hour.ToString();
        //    string mm = DateTime.Now.Minute.ToString();
        //    string b = richTextBox5.Lines[5]; ;     // 需求时间
        //    string bb = b.Substring(0, 2);       //前两位
        //    string bbb = b.Substring(b.Length - 2, 2);  //后两位

        //    decimal h = decimal.Parse(H);
        //    decimal M = decimal.Parse(mm);
        //    decimal B = decimal.Parse(bb);
        //    decimal BB = decimal.Parse(bbb);

        //    int c = ((int)B * 60 + (int)BB) - ((int)h * 60 + (int)M);
        //    if (c - 30 < 0)
        //    {
        //        richTextBox5.BackColor = Color.Salmon;
        //    }
        //    else if (c - 60 < 0)
        //    {
        //        richTextBox5.BackColor = Color.Orange;
        //    }
        //    else if (c - 90 < 0)
        //    {
        //        richTextBox5.BackColor = Color.Khaki;
        //    }
        //    else
        //    {
        //        richTextBox5.BackColor = Color.Aquamarine;
        //    }
        //}
        private void back_color(RichTextBox richTextBox)
        {
            string H = DateTime.Now.Hour.ToString();
            string mm = DateTime.Now.Minute.ToString();
            string b = richTextBox.Lines[5]; ;     // 需求时间
            string bb = b.Substring(b.Length-6, 2);       //前两位
            var index = bb.IndexOf(":");
            if (index > 0)
            {
                bb = b.Substring(b.Length - 6, 1);
            }
            string bbb = b.Substring(b.Length - 3, 2);  //后两位

            decimal h = decimal.Parse(H);
            decimal M = decimal.Parse(mm);
            decimal B = decimal.Parse(bb);
            decimal BB = decimal.Parse(bbb);

            int c = ((int)B * 60 + (int)BB) - ((int)h * 60 + (int)M);
            if (c - 30 < 0)
            {
                richTextBox.BackColor = Color.Salmon;
            }
            else if (c - 60 < 0)
            {
                richTextBox.BackColor = Color.Orange;
            }
            else if (c - 90 < 0)
            {
                richTextBox.BackColor = Color.Khaki;
            }
            else
            {
                richTextBox.BackColor = Color.Aquamarine;
            }
        }
        //定時刷新显示
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            Show_text();  //在5个显示框显示数据函数 ，10秒运行一次

            
        }
        // 确认完成需求工单 5个  点击确认后将显示框对应的数据行的状态修改为0
        private void button1_Click(object sender, EventArgs e)
        {
            ture(richTextBox1, textBoxnum1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ture(richTextBox2, textBoxnum2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ture(richTextBox3, textBoxnum3);
        }
   
        private void button4_Click(object sender, EventArgs e)
        {
            ture(richTextBox4, textBoxnum4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ture(richTextBox5,textBoxnum5);
        }
        // 确认完成按钮执行动作
        private void ture(RichTextBox richTextBox ,TextBox textBox)
        {
            try
            {
                
                string a = richTextBox.Lines[0];
                string aa = a.Substring(a.Length - 4, 4);  //后两位
                string b = richTextBox.Lines[5];
                string bb = b.Substring(b.Length - 6, 5);       //前两位
                

                string c = richTextBox.Lines[6];
                string cc = c.Substring(5, c.Length - 5);
                string f = richTextBox.Lines[7];
                string ff = f.Substring(4, f.Length - 4);
                //string ff = f.Substring()

                string d = textBox.Text.Trim();

                int C = Convert.ToInt32(cc);
                int D = Convert.ToInt32(d);
                int F = Convert.ToInt32(ff);

                int g = D + F;
                int ee = C - D -F;

                string strSQL = "";

               
                 password frm = new password();
                frm.ShowDialog();
                if (password.a == 1)
                {
                    if (ee == 0)
                    {
                        strSQL = "UPDATE smt_td2 SET 狀態 = '0',已完成數量 = '"+ g +"' WHERE 線別 = '" + aa + "'AND 需求時間 = '" + bb + "'";
                    }
                    else if (ee > 0)
                    {
                        strSQL = "UPDATE smt_td2 SET 已完成數量 = '" + g + "' WHERE 線別 = '" + aa + "'AND 需求時間 = '" + bb + "'";
                    }
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    MySqlCommand comm = new MySqlCommand(strSQL, conn);
                    comm.ExecuteNonQuery();
                    textBox.Clear();
                }
            }
            catch
            { }
        }
        // 跳转显示Form3
        private void button7_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            this.Hide();
            frm.Show();
        }
        //退出按钮
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //跳转显示Form1
       
        public static void ThreadProc()
        {
            Application.Run(new Form1());
        }
        private void button8_Click(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            thread.SetApartmentState(System.Threading.ApartmentState.STA); //重点
            thread.Start();
            this.Close();
        }

        private void textTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form6 frm = new Form6();
            this.Hide();
            frm.Show();
        }

      

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
