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

        public static void eraseShip(Ships ship, Panel[,] panel)
        {
            if (ship.turned)
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[ship.x, ship.y + i].BackColor = System.Drawing.Color.White;
                    panel[ship.x, ship.y + i].MouseHover -= new EventHandler((sender, e) => hover(sender, e, panel, ship));
                    panel[ship.x, ship.y + i].MouseLeave -= new EventHandler((sender, e) => hover_leave(sender, e, panel, ship));
                }
            }
            else
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[ship.x + i, ship.y].BackColor = System.Drawing.Color.White;
                    panel[ship.x + i, ship.y].MouseHover -= new EventHandler((sender, e) => hover(sender, e, panel, ship));
                    panel[ship.x + i, ship.y].MouseLeave -= new EventHandler((sender, e) => hover_leave(sender, e, panel, ship));
                }
            }
        }

        public static void createShip(Ships ship, int x, int y,Panel[,] panel)
        {
            if (ship.turned)
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[x, y + i].BackColor = System.Drawing.Color.DarkGray;
                    panel[x, y + i].Name = i.ToString();
                    panel[x, y + i].MouseHover += new EventHandler((sender, e) => hover(sender, e, panel, ship));
                    panel[x, y + i].MouseLeave += new EventHandler((sender, e) => hover_leave(sender, e, panel, ship));
                }

                ship.x = x;
                ship.y = y;
            }
            else
            {
                for (int i = 0; i < ship.length; i++)
                {
                    panel[x + i, y].BackColor = System.Drawing.Color.DarkGray;
                    panel[x + i, y].MouseHover += new EventHandler((sender, e) => hover(sender, e, panel, ship));
                    panel[x + i, y].MouseLeave += new EventHandler((sender, e) => hover_leave(sender, e, panel, ship));
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
;
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
