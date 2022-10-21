using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Game_Hero
{
    public class Equipment
    {
        private const int ARMORS_COUNT = (int)Armor.Armor_Slot.Shield + 1;
        private const int WEAPONS_COUNT = (int)Weapon.Weapon_Slot.Sword + 1; 
        private const int SPELLS_COUNT = (int)Spell.Spell_Slot.First + 1;

        public IEnumerable<Item> Items { get; private set; }

        public IEnumerable<Armor> Armors => _armors.Cast<Armor>();
        private IEnumerable<Item> _armors
        {
            get => Items
            .Take(ARMORS_COUNT);

            set
            {
                if (value is null)
                    throw new NullReferenceException(nameof(value));
                Items = value.Union(Items.Skip(ARMORS_COUNT));
            }
        }

        public IEnumerable<Weapon> Weapons => _weapons.Cast<Weapon>();
        private IEnumerable<Item> _weapons
        {
            get => Items
            .Skip(ARMORS_COUNT)
            .Take(WEAPONS_COUNT);

            set
            {
                if (value is null)
                    throw new NullReferenceException(nameof(value));
                Items = Items
                    .Take(ARMORS_COUNT)
                    .Union(value)
                    .Union(Items.Skip(ARMORS_COUNT + WEAPONS_COUNT));
            }
        }

        public IEnumerable<Spell> Spells => _spells.Cast<Spell>();
        public IEnumerable<Item> _spells
        {
            get => Items
            .Skip(ARMORS_COUNT + WEAPONS_COUNT)
            .Take(SPELLS_COUNT);

            private set
            {
                if (value is null)
                    throw new NullReferenceException(nameof(value));
                Items = Items
                    .Take(ARMORS_COUNT + WEAPONS_COUNT)
                    .Union(value);
            }
        }

        public Equipment()
        {
            Items = Array.Empty<Item>();
        }

        public void Clear()
        {
            Items = new Item[]
            {
                Armor.Panoply,
                Armor.Shield,
                Weapon.Torch,
                Weapon.Spear,
                Weapon.Sword,
                Spell.Raw_Spell
            };
        }

        public void Drop_Equipment(Item_Type type, int slot)
        {
            switch (type)
            {
                case Item_Type.Armor:
                    _armors = _armors.Select(a => a.Slot == slot ? a.Drop() : a);
                    break;
                case Item_Type.Weapon:
                    _weapons = _weapons.Select(w => w.Slot == slot ? w.Drop() : w);
                    break;
                case Item_Type.Spell:
                    _spells = _spells.Select(s => s.Slot == slot ? s.Drop() : s);
                    break;
            }
        }

        public void Make_Spell(Spell.Spell_Slot slot, Spell.Spell_Type type, Monster target)
        {
            switch (type)
            {
                case Spell.Spell_Type.Blank_Spell:
                    break;
                case Spell.Spell_Type.Death_Curse:
                    _spells = _spells.Cast<Spell>().Select(s => (Spell.Spell_Slot)s.Slot == slot ? Spell.Make_Death_Curse(s, target) : s);
                    break;
            }
        }

        public override string ToString() => 
            $"Armors:\n" +
            $"{string.Join("\t\n", Armors)}\n" +
            $"Weapons:\n" +
            $"{string.Join("\t\n", Weapons)}\n" +
            $"Spells:\n" +
            $"{string.Join("\t\n", Spells)}";

    }
}
