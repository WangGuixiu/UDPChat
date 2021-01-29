using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;


namespace UDPChat
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        udpSend newSend = new udpSend();
        udpRecv newRecv = new udpRecv();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = null;
            if (richTextBox2.Text == "")
            {
                MessageBox.Show("输入不能为空！");
                return;
            }
            else
            {
                message = richTextBox2.Text;
                richTextBox2.Text = "";
            }
            byte[] data = Encoding.UTF8.GetBytes(message);
            IPEndPoint hostRecv = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8898);
            newSend.SendData(hostRecv, data);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Thread recvThread = new Thread(StartRecv);
            recvThread.IsBackground = true;
            recvThread.Start();
            GetIP();
        }

        private void StartRecv()
        {
            string message = null;
            while(true)
            {
                message = newRecv.RecvData();
                if (message == "") MessageBox.Show("alive");
                else richTextBox1.Text += message + "\n";
            }
        }

        private void GetIP()
        {
            Match match;
            string ipaddress;
            int i = 0;
            IPHostEntry hostIP = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in hostIP.AddressList)
            {
                match = Regex.Match(ip.ToString(), @"\d{1,}\.\d{1,}\.\d{1,}\.\d{1,}");
                ipaddress = match.ToString();
                if (ipaddress != "") comboBox1.Items.Add(ipaddress);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Match match;
            IPEndPoint host = null;
            string message = null;
            byte[] data = Encoding.UTF8.GetBytes("");
            match = Regex.Match(comboBox1.Text, @"\d{1,}\.\d{1,}\.\d{1,}\.");
            for (int i = 1; i < 255; i++)
            {
                host = new IPEndPoint(IPAddress.Parse(match.ToString() + i.ToString()), 8898);
                newSend.SendData(host, data);
            }
            MessageBox.Show(message);
        }
    }
}
