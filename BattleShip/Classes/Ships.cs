using System;
using System.Collections.Generic;
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

        public static void createShip(Ships ship, int x, int y,Panel[,] panel)
        {
            for(int i = 0; i < ship.length; i++)
            {
                panel[x + i, y].BackColor = System.Drawing.Color.Black;
            }

            ship.x = x;
            ship.y = y;

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
    }

    
}
