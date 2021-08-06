using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Classes
{
    class Tables
    {
        public int rowCount = 10;
        public int columnCount = 10;
        public bool[][] isHit;

        public void setTable(Tables table)
        {
            for(int i = 0; i < rowCount; i++)
            {
                for(int j = 0; j < columnCount; j++)
                {
                    table.isHit[i][j] = false;
                }
            }
        }
    }
}
