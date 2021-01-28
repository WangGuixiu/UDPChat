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
            string message = richTextBox2.Text;
            byte[] data = Encoding.UTF8.GetBytes(message);
            IPEndPoint hostRecv = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8898);
            newSend.SendData(hostRecv, data);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread recvThread = new Thread(StartRecv);
            recvThread.Start();
        }

        private void StartRecv()
        {
            while (true)
            {
                richTextBox1.Text = newRecv.RecvData();
            }
        }
    }
}
