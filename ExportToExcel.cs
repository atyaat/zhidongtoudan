
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace datagridview
{
    class ExportToExcel
    {
        /// <summary> 
        /// 在没有安装Excel的情况下，将DataGridView数据导出到Excel 
        /// </summary> 
        public void _ExportToExcel(DataGridView dgv1)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出到Excel";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
            {
                return;
            }
            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            string str = "";
            try
            {
                for (int i = 0; i < dgv1.ColumnCount; i++)
                {
                    if (dgv1.Columns[i].Visible == false || dgv1.Columns[i].DataPropertyName == "")
                    {
                        continue;
                    }
                    str += dgv1.Columns[i].HeaderText;
                    str += "\t";
                }
                sw.WriteLine(str);
                for (int j = 0; j < dgv1.Rows.Count - 1; j++)
                {
                    string strTemp = "";
                    for (int k = 0; k < dgv1.Columns.Count; k++)
                    {
                        if (dgv1.Columns[k].Visible == false || dgv1.Columns[k].DataPropertyName == "")
                        {
                            continue;
                        }
                        object obj = dgv1.Rows[j].Cells[k].Value;
                        if (obj != null)
                        {
                            strTemp += dgv1.Rows[j].Cells[k].Value.ToString();
                        }
                        else
                        {
                            strTemp = "";
                        }
                        strTemp += "\t";
                    }
                    sw.WriteLine(strTemp);
                }
                sw.Close();
                myStream.Close();
                MessageBox.Show("成功导出到Excel文件：\n" + saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }




        /// <summary>
        /// 将DataGrirdView数据导出到Excel
        /// 需要引用Microsoft.Office.Interop.Excel.dll模块
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="bShowExcel"></param>
        /// <returns></returns>
        public bool DataGridviewShowToExcel(DataGridView dgv, bool bShowExcel)
        {
            if (dgv.Rows.Count == 0)
            {
                return false;
            }
            //建立Excel对象      
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = bShowExcel;
            //生成字段名称    
            int k = 0;
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                if (dgv.Columns[i].Visible == false || dgv.Columns[i].DataPropertyName == "")
                {
                    k++;
                    continue;
                }
                excel.Cells[1, i + 1 - k] = dgv.Columns[i].HeaderText;
            }
            //填充数据     
            for (int i = 0; i < dgv.RowCount; i++)
            {
                k = 0;
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    if (dgv.Columns[j].Visible == false || dgv.Columns[j].DataPropertyName == "")
                    {
                        k++;
                        continue;
                    }
                    try
                    {
                        if (j == 0)
                        {
                            ((Range)(excel.Cells[i + 2, j + 1 - k])).NumberFormat = "@";
                            excel.Cells[i + 2, j + 1 - k] = dgv[j, i].Value.ToString();
                            
                           
                        }
                        else
                            excel.Cells[i + 2, j + 1 - k] = dgv[j, i].Value.ToString();

                    }
                    catch { }

                }
            }
            return true;
        }






    }
}
