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
using BattleShip.Classes;

namespace BattleShip.Forms
{
    public partial class Main : Form
    {
        public Main(TcpClient client,Stream stm)
        {
            InitializeComponent();

            char a = 'A';
            Panel[,] panel = new Panel[11,11];
            Panel[,] panel2 = new Panel[11,11];

            for (int i = 0; i < 11; i++)
            {
                for(int j = 0; j < 11; j++)
                {
                    panel[i, j] = new Panel();
                    panel2[i, j] = new Panel();

                    panel[i, j].Name = i + "." + j;
                    panel2[i, j].Name = i + "." + j;

                    you.Controls.Add(panel[i,j]);
                    enemy.Controls.Add(panel2[i,j]);

                    panel[i, j].Size = new Size(30, 30);
                    panel[i, j].Location = new Point(i * 30, j * 30);

                    panel2[i, j].Size = new Size(30, 30);
                    panel2[i, j].Location = new Point(i * 30, j * 30);

                    if(i == 0 && j != 0)
                    {
                        Label label = new Label();
                        Label label2 = new Label();

                        panel[i, j].Controls.Add(label);
                        panel2[i, j].Controls.Add(label2);

                        label.Text = j.ToString();
                        label.Location = new Point(15, 15);

                        label2.Text = j.ToString();
                        label2.Location = new Point(15, 15);
                    }
                    else if(j == 0 && i != 0)
                    {
                        Label label = new Label();
                        Label label2 = new Label();

                        panel2[i, j].Controls.Add(label2);
                        panel[i, j].Controls.Add(label);

                        label.Text = a.ToString();
                        label.Location = new Point(15, 15);

                        label2.Text = a.ToString();
                        label2.Location = new Point(15, 15);

                        a += (char)1;
                    }
                    else if(i != 0 && j != 0)
                    {
                        panel[i, j].BackColor = Color.White;
                        panel[i, j].BorderStyle = BorderStyle.FixedSingle;

                        panel2[i, j].BackColor = Color.White;
                        panel2[i, j].BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }

            Ships ship2 = new Ships();
            Ships ship31 = new Ships();
            Ships ship32 = new Ships();
            Ships ship4 = new Ships();
            Ships ship5 = new Ships();

            ship2.length = 2;
            ship31.length = 3;
            ship32.length = 3;
            ship4.length = 4;
            ship5.length = 5;

            Ships.createShip(ship2, 1, 1, panel);
            Ships.createShip(ship31, 1, 2, panel);
            Ships.createShip(ship32, 1, 3, panel);
            Ships.createShip(ship4, 1, 4, panel);
            Ships.createShip(ship5, 1, 5, panel);

            panel[ship2.x, ship2.y].DoubleClick += new EventHandler((sender, e) => panel_Click(sender, e, ship2, panel));
            panel[ship31.x, ship31.y].DoubleClick += new EventHandler((sender, e) => panel_Click(sender, e, ship31, panel));
            panel[ship32.x, ship32.y].DoubleClick += new EventHandler((sender, e) => panel_Click(sender, e, ship32, panel));
            panel[ship4.x, ship4.y].DoubleClick += new EventHandler((sender, e) => panel_Click(sender, e, ship4, panel));
            panel[ship5.x, ship5.y].DoubleClick += new EventHandler((sender, e) => panel_Click(sender, e, ship5, panel));
            /*
            panelship2.Click += new EventHandler((sender, e) => panel_Click(sender, e, panelship2));
            panelship31.Click += new EventHandler((sender, e) => panel_Click(sender, e, panelship31));
            panelship32.Click += new EventHandler((sender, e) => panel_Click(sender, e, panelship32));
            panelship4.Click += new EventHandler((sender, e) => panel_Click(sender, e, panelship4));
            panelship5.Click += new EventHandler((sender, e) => panel_Click(sender, e, panelship5));
            */

            Users player = new Users();
            Users enemyplayer = new Users();

            player.shipCount = 5;
            enemyplayer.shipCount = 5;

            ready.Click += new EventHandler((sender, e) => ready_Click(sender, e, panel));
            send.Click += new EventHandler((sender, e) => send_Click(sender, e, stm));


            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    string str = connection.getString(stm);
                    if (str.EndsWith("-t"))
                    {
                        chat.Text += str.Substring(0, (str.Length - 2)) + "\n";
                    }
                    else
                    {
                        
                    }
                }
            });


        }

        private void send_Click(object sender, EventArgs e, Stream stm)
        {
            string str = chattext.Text;
            str += "-t";

            connection.sendString(str, stm);
            chattext.Text = null;
        }

        private void panel_Click(object sender, EventArgs e, Ships ship, Panel[,] panels)
        {
            int i = 0;
            if (ship.turned)
            {
                while(i < ship.length && panels[ship.x + i,ship.y].BackColor != Color.DarkGray)
                {
                    i++;
                }
                if(i == ship.length)
                {
                    Ships.eraseShip(ship, panels);
                    ship.turned = false;
                    Ships.createShip(ship, ship.x, ship.y, panels);
                }
                else
                {
                    MessageBox.Show("Döndürülemiyor.");
                }
            }
            else
            {
                while (i < ship.length && panels[ship.x, ship.y + i].BackColor != Color.DarkGray)
                {
                    i++;
                }
                if(i == ship.length)
                {
                    Ships.eraseShip(ship, panels);
                    ship.turned = true;
                    Ships.createShip(ship, ship.x, ship.y, panels);
                }

            }
        }

        private void ready_Click(object sender, EventArgs e, Panel[,] panel)
        {
            int[,] arr = new int[10, 10];
            int i, j;

            for(i = 0; i < 10; i++)
            {
                for(j = 0; j < 10; j++)
                {
                    if(panel[i, j].BackColor == Color.DarkGray)
                    {
                        arr[i, j] = 1;
                    }
                }
            }
        }
    }
}