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
        public string name;
        public bool turned;
        public int[] hit;
        public int[] coordinates;

        public static Panel createShip(int length, Panel you)
        {
            Panel ship = new Panel();
            you.Controls.Add(ship);

            ship.Size = new System.Drawing.Size(30, length * 30);
            ship.Location = new System.Drawing.Point(0, 0);

            for(int i = 0; i < length; i++)
            {

            }

            return ship;
        }
    }

    
}
