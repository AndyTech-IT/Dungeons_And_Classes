using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Game_Hero
{
    public class Hero
    {
        public int Helth_Points { get; private set; }
        private Equipment _equipment;

        public Hero(Equipment equipment)
        {
            _equipment = equipment;
            Helth_Points = 3;
            foreach (var armor in equipment.Armors)
                Helth_Points += armor.Armor_Power;
        }

        public bool Try_Defiat(Monster monster)
        {
            if (Can_Defeat(monster))
                return true;

            Helth_Points -= monster.Power;
            return false;
        }

        private bool Can_Defeat(Monster monster) => _equipment.Spells.Any(s => s.Can_Defeat(monster)) || _equipment.Weapons.Any(w => w.Can_Defeat(monster));
    }
}
