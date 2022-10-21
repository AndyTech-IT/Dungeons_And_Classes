using Dungeons_And_Classes.Assets.Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Game_Hero.Hero_Items
{
    public interface IDefeatable
    {
        public bool Can_Defeat(Monster monster);
    }
}
