using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Connection
{
    class connection
    {
        public void connect(string ip, int port)
        {
            TcpClient client = new TcpClient();
            Console.WriteLine("Connecting...");

            client.Connect(ip, port);
            Console.WriteLine("Connected.");
            Stream tcpstrm = client.GetStream();
        }

        public void sendString(string str)
        {

        }
    }
}
