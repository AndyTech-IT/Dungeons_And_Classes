using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using RandomGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Players
{
    public class Random_Player : Player
    {
        private static Random_Generator _generator = new();


        public override void On_Player_Add_Card(Player actor)
        {}

        public override void On_Player_Drop_Card(Player actor, Item cost)
        {}

        public override void On_Player_Enter_Dangeon(Player actor)
        {}

        public override void On_Player_Exit_Dangeon(Player actor, bool result, int hp)
        {}

        public override void On_Player_Passed(Player actor)
        {}

        protected override void Forsed_add(Monster monster)
        { }

        protected override Move_Data Make_move(Equipment e, Monster monster)
        {
            if (_generator.Next_Double() < .5)
            {
                return new();
            }
            return new(_generator.Next_Item(e.Items.Where(i => i.Dropped == false).ToArray()));
        }

        protected override Spell Make_spell(Equipment e, Spell source)
        {
            Monster monster = (Monster_Type)_generator.Next_Integer((int)Monster_Type.Goblin, (int)Monster_Type.Dragon) switch
            {
                Monster_Type.Goblin => Monster.Goblin,
                Monster_Type.Skeleton => Monster.Skeleton,
                Monster_Type.Ork => Monster.Ork,
                Monster_Type.Vampire => Monster.Vampire,
                Monster_Type.Golem => Monster.Golem,
                Monster_Type.Lich => Monster.Lich,
                Monster_Type.Demon => Monster.Demon,
                Monster_Type.Dragon => Monster.Dragon,
                _ => throw new NotSupportedException(),
            };
            return Spell.Make_Death_Curse(source, monster);
        }

        protected override bool Want_pass(Equipment e) => _generator.Next_Double() < .2;
    }
}
