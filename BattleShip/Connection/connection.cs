using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip.Connection
{
    class connection
    {
        public static TcpClient connect(string ip, int port)
        {
            try
            {
                TcpClient client = new TcpClient();
                Console.WriteLine("Connecting...");

                client.Connect(ip, port);
                Console.WriteLine("Connected.");

                return client;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static void sendString(string str, Stream stm)
        {
            try
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] b = asen.GetBytes(str);
                stm.Write(b, 0, b.Length);
                Console.WriteLine("Message sent.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string getString(Stream stm)
        {
            try
            {
                byte[] b = new byte[500];
                int k = stm.Read(b, 0, 500);
                string message = string.Empty;

                for (int i = 0; i < k; i++)
                {
                    message += Convert.ToChar(b[i]);
                }

                Console.WriteLine("Message received.");

                return message;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
