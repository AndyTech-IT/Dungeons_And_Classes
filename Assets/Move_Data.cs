using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets
{
    public struct Move_Data
    {
        public bool Dropped => Dropped_Item != null;
        public readonly Item? Dropped_Item;

        public Move_Data(Item? dropping_item = null)
        {
            Dropped_Item = dropping_item;
        }
    }
}
