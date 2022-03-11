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
        public Main(TcpClient client, Stream stm)
        {
            InitializeComponent();

            Users player = new Users();
            Users enemyplayer = new Users();
            string name = connection.getString(stm);

            player.username = name[0].ToString();
            enemyplayer.username = name[1].ToString();

            Console.WriteLine(player.username + "\n" + enemyplayer.username);

            label1.Text = player.username;
            label2.Text = enemyplayer.username;

            char a = 'A';
            Panel[,] panel = new Panel[11, 11];
            Panel[,] panel2 = new Panel[11, 11];

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    panel[i, j] = new Panel();
                    panel2[i, j] = new Panel();

                    panel[i, j].Name = i + "." + j;
                    panel2[i, j].Name = i + "." + j;

                    you.Controls.Add(panel[i, j]);
                    enemy.Controls.Add(panel2[i, j]);

                    panel[i, j].Size = new Size(30, 30);
                    panel[i, j].Location = new Point(i * 30, j * 30);

                    panel2[i, j].Size = new Size(30, 30);
                    panel2[i, j].Location = new Point(i * 30, j * 30);

                    if (i == 0 && j != 0)
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
                    else if (j == 0 && i != 0)
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
                    else if (i != 0 && j != 0)
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

            Ships[] allships = { ship2, ship31, ship32, ship4, ship5 };

            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    panel2[i, j].Click += new EventHandler((sender, e) => enemy_panel_click(sender, e, enemy, panel2, stm));
                }
            }

            player.shipCount = 5;
            enemyplayer.shipCount = 5;

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    panel[i, j].MouseDown += new MouseEventHandler((sender, e) => mouse_down(sender, e, panel, allships));
                    panel[i, j].MouseUp += new MouseEventHandler(mouse_up);
                }
            }

            ready.Click += new EventHandler((sender, e) => ready_Click(sender, e, panel, allships, stm));
            send.Click += new EventHandler((sender, e) => send_Click(sender, e, stm));

            int coorx, coory;

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    string str = connection.getString(stm);
                    if (str.EndsWith("-t"))
                    {
                        chat.Text += str.Substring(0, (str.Length - 2)) + "\n";
                    }
                    else if (str.EndsWith("-c"))
                    {
                        coorx = Int32.Parse(str.Substring(0, 1));
                        coory = Int32.Parse(str.Substring(1, 1));

                        panel[coorx + 1, coory + 1].BackColor = Color.Red;
                    }
                    else if (str.EndsWith("-c2"))
                    {
                        while (!str.EndsWith(" "))
                        {
                            coorx = Int32.Parse(str.Substring(0, 1));
                            coory = Int32.Parse(str.Substring(1, 1));

                            panel[coorx + 1, coory + 1].BackColor = Color.DarkRed;

                            str = connection.getString(stm);
                        }
                    }
                }
            });


        }

        private void Main_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void send_Click(object sender, EventArgs e, Stream stm)
        {
            string str = chattext.Text;
            str += "-t";

            connection.sendString(str, stm);
            chattext.Text = null;
        }

        static bool click = false;

        private void mouse_down(object sender, EventArgs e, Panel[,] panel, Ships[] allships)
        {
            int i, j, counter, finish, shiplength;
            int[] mtrx = new int[6];
            int[] mtry = new int[6];

            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;

            click = true;

            counter = 0;

            for (i = 1; i < 11; i++)
            {
                for (j = 1; j < 11; j++)
                {
                    if (panel[i, j].BackColor == Color.Yellow)
                    {
                        mtrx[counter] = i;
                        mtry[counter] = j;

                        counter++;
                    }
                }
            }

            shiplength = 0;
            finish = 0;

            while (shiplength < 5 && finish == 0)
            {
                if (allships[shiplength].x == mtrx[0] && allships[shiplength].y == mtry[0])
                {
                    finish = 1;
                }
                else
                {
                    shiplength++;
                }
            }

            Task.Factory.StartNew(() => {
                while (click)
                {
                    i = 0;

                    if (Cursor.Position.X - x >= 30)
                    {
                        if (!(!allships[shiplength].turned && allships[shiplength].x + 1 + allships[shiplength].length > 11) && !(allships[shiplength].x + 1 >= 11))
                        {
                            if (allships[shiplength].turned)
                            {
                                while (i < allships[shiplength].length && panel[allships[shiplength].x + 1, allships[shiplength].y + i].BackColor != Color.DarkGray)
                                {
                                    i++;
                                }

                                if (i == allships[shiplength].length)
                                {
                                    x = Cursor.Position.X;

                                    Ships.eraseShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                    allships[shiplength].x++;
                                    Ships.createShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                }
                            }
                            else
                            {
                                if (panel[allships[shiplength].x + allships[shiplength].length, allships[shiplength].y].BackColor != Color.DarkGray)
                                {
                                    x = Cursor.Position.X;

                                    Ships.eraseShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                    allships[shiplength].x++;
                                    Ships.createShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                }
                            }
                        }
                    }
                    else if (x - Cursor.Position.X >= 30)
                    {
                        if (!(allships[shiplength].x - 1 < 1))
                        {
                            if (allships[shiplength].turned)
                            {
                                while (i < allships[shiplength].length && panel[allships[shiplength].x - 1, allships[shiplength].y + i].BackColor != Color.DarkGray)
                                {
                                    i++;
                                }

                                if (i == allships[shiplength].length)
                                {
                                    x = Cursor.Position.X;

                                    Ships.eraseShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                    allships[shiplength].x--;
                                    Ships.createShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                }
                            }
                            else
                            {
                                if (panel[allships[shiplength].x - 1, allships[shiplength].y].BackColor != Color.DarkGray)
                                {
                                    x = Cursor.Position.X;

                                    Ships.eraseShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                    allships[shiplength].x--;
                                    Ships.createShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                }
                            }
                        }
                    }
                    else if (Cursor.Position.Y - y >= 30)
                    {
                        if (!(allships[shiplength].turned && allships[shiplength].y + 1 + allships[shiplength].length > 11) && !(allships[shiplength].y + 1 >= 11))
                        {
                            if (!allships[shiplength].turned)
                            {
                                while (i < allships[shiplength].length && panel[allships[shiplength].x + i, allships[shiplength].y + 1].BackColor != Color.DarkGray)
                                {
                                    i++;
                                }

                                if (i == allships[shiplength].length)
                                {
                                    y = Cursor.Position.Y;

                                    Ships.eraseShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                    allships[shiplength].y++;
                                    Ships.createShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                }
                            }
                            else
                            {
                                if (panel[allships[shiplength].x, allships[shiplength].y + allships[shiplength].length].BackColor != Color.DarkGray)
                                {
                                    y = Cursor.Position.Y;

                                    Ships.eraseShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                    allships[shiplength].y++;
                                    Ships.createShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                }
                            }
                        }
                    }
                    else if (y - Cursor.Position.Y >= 30)
                    {
                        if (!(allships[shiplength].y - 1 < 1))
                        {
                            if (!allships[shiplength].turned)
                            {
                                while (i < allships[shiplength].length && panel[allships[shiplength].x + i, allships[shiplength].y - 1].BackColor != Color.DarkGray)
                                {
                                    i++;
                                }

                                if (i == allships[shiplength].length)
                                {
                                    y = Cursor.Position.Y;

                                    Ships.eraseShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                    allships[shiplength].y--;
                                    Ships.createShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                }
                            }
                            else
                            {
                                if (panel[allships[shiplength].x, allships[shiplength].y - 1].BackColor != Color.DarkGray)
                                {
                                    y = Cursor.Position.Y;

                                    Ships.eraseShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                    allships[shiplength].y--;
                                    Ships.createShip(allships[shiplength], allships[shiplength].x, allships[shiplength].y, panel);
                                }
                            }
                        }
                    }
                }
            });
        }

        private void mouse_up(object sender, EventArgs e)
        {
            click = false;
        }

        private void ready_Click(object sender, EventArgs e, Panel[,] panel, Ships[] allships, Stream stm)
        {
            int[,] arr = new int[10, 10];
            int i, j, k;
            string str = null;

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    arr[i, j] = 0;

                    panel[i + 1, j + 1].MouseHover -= Ships.handlerList[i + 1, j + 1, 0];
                    panel[i + 1, j + 1].MouseLeave -= Ships.handlerList[i + 1, j + 1, 1];

                    if (Ships.handlerList[i + 1, j + 1, 2] != null)
                    {
                        for (k = 0; k < 5; k++)
                        {
                            if (i + 1 + k <= 10)
                            {
                                panel[i + 1 + k, j + 1].DoubleClick -= Ships.handlerList[i + 1, j + 1, 2];
                            }

                            if (j + 1 + k <= 10)
                            {
                                panel[i + 1, j + 1 + k].DoubleClick -= Ships.handlerList[i + 1, j + 1, 2];
                            }
                        }
                    }
                }
            }

            k = 1;

            foreach (Ships ship in allships)
            {
                if (ship.turned)
                {
                    for (i = 0; i < ship.length; i++)
                    {
                        arr[ship.y - 1 + i, ship.x - 1] = k;
                    }
                    k++;
                }
                else
                {
                    for (i = 0; i < ship.length; i++)
                    {
                        arr[ship.y - 1, ship.x - 1 + i] = k;
                    }
                    k++;
                }
            }

            i = 0;

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    str += arr[i, j];
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }

            connection.sendString(str, stm);

            ready.Enabled = false;
        }

        private void enemy_panel_click(object sender, EventArgs e, Panel enemy, Panel[,] panel, Stream stm)
        {
            int x = Cursor.Position.X - this.Location.X;
            int y = Cursor.Position.Y - this.Location.Y - 23;
            int i, j;
            string msg;

            x -= enemy.Location.X;
            y -= enemy.Location.Y;

            x /= 30;
            y /= 30;

            if (panel[x, y].BackColor == Color.White)
            {
                string shoot = x + "-" + y + "-c";

                Console.WriteLine(x + "-" + y);

                connection.sendString(shoot, stm);
                Task.Factory.StartNew(() =>
                {
                    //System.Threading.Thread.Sleep(2000);
                    msg = connection.getString(stm);

                    if (msg.EndsWith("-c") && !msg.Substring(0, 1).Equals("0"))
                    {
                        panel[x, y].BackColor = Color.Red;
                    }
                    else if (msg.EndsWith("-c") && msg.Substring(0, 1).Equals("0"))
                    {
                        panel[x, y].BackColor = Color.Aqua;
                    }
                    else if (msg.EndsWith("-s1"))
                    {
                        while (!msg.EndsWith(" "))
                        {
                            i = Int32.Parse(msg.Substring(0, 1));
                            j = Int32.Parse(msg.Substring(1, 1));

                            panel[i + 1, j + 1].BackColor = Color.DarkRed;

                            msg = connection.getString(stm);
                        }
                    }
                });

            }
        }
    }
}