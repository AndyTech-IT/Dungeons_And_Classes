using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Game_Hero.Hero_Items
{
    public abstract class Item
    {
        public Item_Type Type { get; private set; }
        public int Slot { get; private set; }
        public string Title { get; private set; }
        public bool Dropped { get; private set; }

        public Item() => throw new NotSupportedException("Don't use this!");
        
        public virtual Item Drop()
        {
            Dropped = true;
            return this;
        }

        protected Item(Item_Type type, int slot, string title)
        {
            Type = type;
            Slot = slot;
            Title = title;
            Dropped = false;
        }

        public override string ToString() => $"{(Dropped ? "Dropped" : GetType().Name)} {Title}";
    }
}
