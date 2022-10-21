using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Game_Hero.Hero_Items
{
    public class Armor : Item
    {
        public int Armor_Power { get; private set; }

        public enum Armor_Slot
        {
            Panoply,
            Shield
        }

        public override Armor Drop()
        {
            Armor_Power = 0;
            return (Armor)base.Drop();
        }

        public static Armor Panoply => new("Доспехи", Armor_Slot.Panoply, 5);
        public static Armor Shield => new("Щит", Armor_Slot.Shield, 3);

        public Armor() => throw new NotSupportedException("Don't use this!");

        private Armor(string title, Armor_Slot slot, int power)
            : base(Item_Type.Armor, (int)slot, title)
        {
            if (slot < Armor_Slot.Panoply || slot > Armor_Slot.Shield)
                throw new NotSupportedException(nameof(slot));

            Armor_Power = power;
        }
    }
}
