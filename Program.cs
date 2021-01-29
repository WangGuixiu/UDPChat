using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace UDPChat
{
    struct udpData
    {
        public string message;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    // UDP发送数据
    public class udpSend
    {
        private static IPEndPoint hostSend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8899);
        UdpClient udpSendSock = new UdpClient(hostSend);

        public void SendData(IPEndPoint hostRecv, byte[] data)
        {
            try
            {
                udpSendSock.Send(data, data.Length, hostRecv);
            }
            catch
            {
                ;
            }
        }
    }

    // UDP接收数据
    public class udpRecv
    {
        private static IPEndPoint hostRecv = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8898);
        UdpClient udpRecvSock = new UdpClient(hostRecv);
        byte[] data = null;

        public string RecvData()
        {
            data = udpRecvSock.Receive(ref hostRecv);
            return Encoding.UTF8.GetString(data);
        }
    }
}
