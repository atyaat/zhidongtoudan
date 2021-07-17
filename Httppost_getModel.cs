using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace datagridview
{
 public class Httppost_getModel
    {
        public string[] get_Model(string MO)
        {
            string sign = Sign_get(MO);//获取秘钥
           
            string rest =GetPage(MO, sign).ToString();//从API上获取工单信息


            //以下处理返回的信息

            //rest = rest.Replace("description\": \"", "description\":[");
            //rest = rest.Replace("\\", "");
            //rest = rest.Replace("}\"}", "}]}");
            //rest = rest.Replace("rn", "");
            //rest = rest.Replace("}\"", "}]");
            
            rest = rest.Replace("{", "[{");
            rest = rest.Replace("Target Qty", "Target_Qty");
            rest = rest.Replace("Cust Code", "Cust_Code");
            rest = rest.Replace("Cust PartNo", "Cust_PartNo");
            rest = rest.Replace("}", "}]");
            string results = rest.Substring(1, rest.Length - 2);
            string[] model_target = new string[3];

            try
            {
                var jObject = JObject.Parse(results);
                GetWOMaterialIssueList Reponse = JsonConvert.DeserializeObject<GetWOMaterialIssueList>(results);
                if (Reponse.description.Count > 0)
                {

                    model_target[0] = Reponse.description[0].Target_Qty;
                    model_target[1] = Reponse.description[0].Model;//获取机种
                                                             //dataGridView2.DataSource = Reponse.Message;
                                                                   // 获取工单总数
                                                                   //Target_Qty

                }
                //else { }
                //{ return model_target[2]; }
            }
            catch
            {
                //return "没查到机种";
            }
            return model_target;

        }




        public string Sign_get(string mo) //获取秘钥
        {//{"factory":"DG5","testType":"GET_MO_INFO","routingData":"0}2512015652}","testData": []} 
            string signrtr = "894A0F0DF84A4799E0530CCA940AC604" + "{\"factory\":\"DG5\",\"testType\":\"GET_MO_INFO\",\"routingData\":\"0}";

            signrtr += string.Format(@"{0 }", mo);
            signrtr += "}\",\"testData\":[]}";
           
            string sign = StringToMD5Hash(signrtr);
            return sign;

        }

        /// <summary>
        /// Post数据到网站
        /// </summary>
        /// <param name="posturl">网址</param>
        /// <param name="postData">参数bai</param>
        /// <returns></returns>
        public JObject GetPage( string MO,string sign)

        {
           
           
            string posturl = "http://10.148.192.37:10101/TDC/DELTA_DEAL_TEST_DATA_I?sign=";
            posturl += sign;
           
            try
            {
              
               
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(posturl);

               
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers["tokenID"] = "894A0F0DF8494799E0530CCA940AC604";
                request.ProtocolVersion = HttpVersion.Version10;
                
                string postData = "{\"factory\":\"DG5\",\"testType\":\"GET_MO_INFO\",\"routingData\":\"0}";

                postData += string.Format(@"{0 }", MO);
                postData += "}\",\"testData\":[]}";
               
                using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
                {
                    dataStream.Write(postData);
                    dataStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                //解析josn
                JObject jo = JObject.Parse(retString);
                //textBox1.Text = jo.ToString();
                //Response.Write(jo["message"]["mmmm"].ToString());
                return jo;
           

               
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                //return string.Empty;
                return null;
            }
        }


        public string StringToMD5Hash(string inputString)//MD5加密
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString().ToUpper();//將字符转化为大写
        }


    }
}






//定义传回数据内容的样式
public class GetWOMaterialIssueList
{
    public string Result { get; set; }//定义Result

    public List<DATA_rest> description { get; set; }//定义Message
    public GetWOMaterialIssueList()
    {
        description = new List<DATA_rest>();//实例化Message
    }
}

public class DATA_rest//定义数据的样式，
{

    public string Model { get; set; }
    public string Target_Qty { get; set; }
}

