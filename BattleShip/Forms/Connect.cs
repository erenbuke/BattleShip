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
using System.Net.Sockets;
using BattleShip.Connection;
using System.IO;
using BattleShip.Forms;

namespace BattleShip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient client = connection.connect(sIP.Text, 5000);

            Stream stm = client.GetStream();
            connection.sendString(username.Text, stm);

            string message = connection.getString(stm);
            if (message.Equals("Waiting for player2"))
            {
                waiting.Visible = true;
            }
            else if (message.Equals("ok"))
            {
                Main f1 = new Main(client, stm);

                Hide();
                f1.ShowDialog();
                Close();
            }

            Task.Factory.StartNew(() =>
            {
                message = connection.getString(stm);
                if (message.Equals("ok"))
                {
                    Main f1 = new Main(client, stm);

                    Hide();
                    f1.ShowDialog();
                    Close();

                }
            });
        }
    }
}
