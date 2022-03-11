using BattleShip.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip.Classes
{
    class Ships
    {
        public int length;
        public bool turned;
        public int[] hit;
        public int x;
        public int y;

        public static EventHandler[,,] handlerList = new EventHandler[12, 12, 3];

        public static void eraseShip(Ships ship, int x, int y, Panel[,] panel)
        {
            if (ship.turned)
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[x, y + i].BackColor = System.Drawing.Color.White;
                    panel[x, y + i].MouseHover -= handlerList[x, y + i, 0];
                    panel[x, y + i].MouseLeave -= handlerList[x, y + i, 1];
                    panel[x, y + i].DoubleClick -= handlerList[x, y, 2];
                }
            }
            else
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[x + i, y].BackColor = System.Drawing.Color.White;
                    panel[x + i, y].MouseHover -= handlerList[x + i, y, 0];
                    panel[x + i, y].MouseLeave -= handlerList[x + i, y, 1];
                    panel[x + i, y].DoubleClick -= handlerList[x, y, 2];
                }
            }
        }

        public static void createShip(Ships ship, int x, int y, Panel[,] panel)
        {
            handlerList[x, y, 2] = new EventHandler((sender, e) => panel_Click(sender, e, ship, panel));

            if (ship.turned)
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[x, y + i].BackColor = System.Drawing.Color.DarkGray;
                    panel[x, y + i].Name = i.ToString();

                    handlerList[x, y + i, 0] = new EventHandler((sender, e) => hover(sender, e, panel, ship));
                    panel[x, y + i].MouseHover += handlerList[x, y + i, 0];

                    handlerList[x, y + i, 1] = new EventHandler((sender, e) => hover_leave(sender, e, panel, ship));
                    panel[x, y + i].MouseLeave += handlerList[x, y + i, 1];

                    panel[x, y + i].DoubleClick += handlerList[x, y, 2];
                }

                ship.x = x;
                ship.y = y;
            }
            else
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[x + i, y].BackColor = System.Drawing.Color.DarkGray;

                    handlerList[x + i, y, 0] = new EventHandler((sender, e) => hover(sender, e, panel, ship));
                    panel[x + i, y].MouseHover += handlerList[x + i, y, 0];

                    handlerList[x + i, y, 1] = new EventHandler((sender, e) => hover_leave(sender, e, panel, ship));
                    panel[x + i, y].MouseLeave += handlerList[x + i, y, 1];

                    panel[x + i, y].DoubleClick += handlerList[x, y, 2];
                }

                ship.x = x;
                ship.y = y;
            }

            /*
            panelship.Size = new System.Drawing.Size(30, ship.length * 30);
            panelship.Location = new System.Drawing.Point(startpoint, 0);
            panelship.BackColor = System.Drawing.Color.Gray;
            panelship.BorderStyle = BorderStyle.FixedSingle;
            */

            /*for(int i = 0; i < ship.length; i++)
            {
                Panel shippart = new Panel();
                panelship.Controls.Add(shippart);

                shippart.Size = new System.Drawing.Size(30, 30);
                shippart.Location = new System.Drawing.Point(0, i * 30);
                shippart.BackColor = System.Drawing.Color.Gray;
                shippart.BorderStyle = BorderStyle.FixedSingle;
            }*/

        }

        private static void panel_Click(object sender, EventArgs e, Ships ship, Panel[,] panels)
        {
            int i = 0;
            bool control = true;
            if (ship.turned)
            {
                while (control && i < ship.length && (panels[ship.x + i, ship.y].BackColor != Color.DarkGray || i == 0))
                {
                    i++;

                    if (ship.x + i >= 11)
                    {
                        control = false;
                    }
                }
                if (i == ship.length)
                {
                    eraseShip(ship, ship.x, ship.y, panels);
                    ship.turned = false;
                    createShip(ship, ship.x, ship.y, panels);
                }
                else
                {
                    Task.Factory.StartNew(() =>
                    {
                        MessageBox.Show("Ship cannot be turned.");
                    });
                }
            }
            else
            {
                while (control && i < ship.length && (panels[ship.x, ship.y + i].BackColor != Color.DarkGray || i == 0))
                {
                    i++;

                    if (ship.y + i >= 11)
                    {
                        control = false;
                    }
                }
                if (i == ship.length)
                {
                    eraseShip(ship, ship.x, ship.y, panels);
                    ship.turned = true;
                    createShip(ship, ship.x, ship.y, panels);
                }
                else
                {
                    Task.Factory.StartNew(() =>
                    {
                        MessageBox.Show("Ship cannot be turned.");
                    });
                }
            }
        }

        private static void hover(object sender, EventArgs e, Panel[,] panel, Ships ship)
        {
            if (ship.turned)
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[ship.x, ship.y + i].BackColor = Color.Yellow;
                }
            }
            else
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[ship.x + i, ship.y].BackColor = Color.Yellow;
                }
            }

        }


        private static void hover_leave(object sender, EventArgs e, Panel[,] panel, Ships ship)
        {
            if (ship.turned)
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[ship.x, ship.y + i].BackColor = Color.DarkGray;
                }
            }
            else
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[ship.x + i, ship.y].BackColor = Color.DarkGray;
                }
            }
        }
    }


}
