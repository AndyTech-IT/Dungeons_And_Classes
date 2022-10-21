using Dungeons_And_Classes.Assets.Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Game_Hero.Hero_Items
{
    public class Spell: Item, IDefeatable
    {
        public enum Spell_Slot
        {
            First
        }
        public enum Spell_Type
        {
            Blank_Spell,
            Death_Curse
        }

        public override Spell Drop() => (Spell)base.Drop();

        private Predicate<Monster> _target;
        public bool Can_Defeat(Monster monster) => !Dropped && _target.Invoke(monster);

        public Spell_Type Spells_Type { get; private set; }


        public static Spell Raw_Spell => new("Чистое заклинание", Spell_Slot.First, Spell_Type.Blank_Spell, m => false);

        public static Spell Make_Death_Curse(Spell source, Monster target) 
            => source.Spells_Type == Spell_Type.Blank_Spell
            ? new ("Проклятье смерти", (Spell_Slot)source.Slot, Spell_Type.Death_Curse, m => m == target) 
            : throw new NotSupportedException("Source is not blank!");


        public Spell() => throw new NotSupportedException("Don't use this!");

        private Spell(string title, Spell_Slot slot, Spell_Type type, Predicate<Monster> target)
            : base(Item_Type.Spell, (int)slot, title)
        {
            Spells_Type = type;
            _target = target;
        }
    }
}
