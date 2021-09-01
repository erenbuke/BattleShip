using BattleShip.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip.Forms
{
    public partial class Main : Form
    {
        public Main(TcpClient client,Stream stm)
        {
            InitializeComponent(stm);

            char a = 'A';

            for (int i = 0; i < 11; i++)
            {
                for(int j = 0; j < 11; j++)
                {
                    Panel panel = new Panel();
                    Panel panel2 = new Panel();

                    you.Controls.Add(panel);
                    enemy.Controls.Add(panel2);

                    panel.Size = new Size(30, 30);
                    panel.Location = new Point(i * 30, j * 30);

                    panel2.Size = new Size(30, 30);
                    panel2.Location = new Point(i * 30, j * 30);

                    if(i == 0 && j != 0)
                    {
                        Label label = new Label();
                        Label label2 = new Label();

                        panel.Controls.Add(label);
                        panel2.Controls.Add(label2);

                        label.Text = j.ToString();
                        label.Location = new Point(15, 15);

                        label2.Text = j.ToString();
                        label2.Location = new Point(15, 15);
                    }
                    else if(j == 0 && i != 0)
                    {
                        Label label = new Label();
                        Label label2 = new Label();

                        panel2.Controls.Add(label2);
                        panel.Controls.Add(label);

                        label.Text = a.ToString();
                        label.Location = new Point(15, 15);

                        label2.Text = a.ToString();
                        label2.Location = new Point(15, 15);

                        a += (char)1;
                    }
                    else if(i != 0 && j != 0)
                    {
                        panel.BackColor = Color.White;
                        panel.BorderStyle = BorderStyle.FixedSingle;

                        panel2.BackColor = Color.White;
                        panel2.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    string str = connection.getString(stm);
                    chat.Text += str + "\n";
                }
            });


        }

        private void send_Click(object sender, EventArgs e, Stream stm)
        {
            string str = chattext.Text;

            connection.sendString(str, stm);
        }
    }
}
