using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets
{
    public struct Trade_Round_Result
    {
        public readonly Player Traveling_Player;

        public Trade_Round_Result(Player player)
        {
            Traveling_Player = player;
        }
    }
}
