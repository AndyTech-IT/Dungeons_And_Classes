using Dungeons_And_Classes.Assets.Dungeon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Game_Hero.Hero_Items
{
    public class Weapon : Item, IDefeatable
    {
        public enum Weapon_Slot
        {
            Torch,
            Spear,
            Sword
        }
        protected Predicate<Monster> _target { get; }
        public bool Can_Defeat(Monster monster) => !Dropped && _target.Invoke(monster);

        public override Weapon Drop() => (Weapon)base.Drop();

        public static Weapon Torch => new("Факел", Weapon_Slot.Torch, m => m.Power <= 3);
        public static Weapon Spear => new("Драконье копьё", Weapon_Slot.Spear, m => m.Power == 9);
        public static Weapon Sword => new("Небесный меч", Weapon_Slot.Sword, m => m.Power % 2 == 0);


        public Weapon() => throw new NotSupportedException("Don't use this!");

        protected Weapon(Item_Type type, string title, int slot, Predicate<Monster> target) 
            : base(type, slot, title)
        {
            _target = target;
        }

        private Weapon(string title, Weapon_Slot slot, Predicate<Monster> target)
            : base(Item_Type.Weapon, (int)slot, title)
        {
            if (slot < Weapon_Slot.Torch || slot > Weapon_Slot.Sword)
                throw new NotSupportedException(nameof(slot));

            _target = target;
        }
    }
}
