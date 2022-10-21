using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using System.Diagnostics;

namespace Dungeons_And_Classes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Equipment e = new();
            Debug.WriteLine("Source:\n" + e);
            e.Drop_Equipment(Item_Type.Armor, 0);
            e.Drop_Equipment(Item_Type.Weapon, 0);
            e.Make_Spell(Spell.Spell_Slot.First, Spell.Spell_Type.Death_Curse, new Monster("", 1));
            Debug.WriteLine("Result:\n" + e);
        }
    }
}