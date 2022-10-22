using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Players
{
    public class Human_PLayer : Player
    {
        private Game_Form _form;
        public Human_PLayer(Game_Form form)
        {
            _form = form;
        }
        public override void On_Player_Add_Card(Player actor)
        { }

        public override void On_Player_Drop_Card(Player actor, Item cost)
        { }

        public override void On_Player_Enter_Dangeon(Player actor)
        { }

        public override void On_Player_Exit_Dangeon(Player actor, bool result, int hp)
        { }

        public override void On_Player_Passed(Player actor)
        { }

        protected override void Forsed_add(Monster monster)
        { }

        protected override Move_Data Make_move(Equipment e, Monster monster) => _form.Make_Move(e, monster);

        protected override Spell Make_spell(Equipment e, Spell source) => Make_spell_async(e, source).Result;

        protected override bool Want_pass(Equipment e) => Want_pass_async(e).Result;

        private async Task<Spell> Make_spell_async(Equipment e, Spell source) => await Task.Run(() => _form.Make_Spell(e, source));
        private async Task<bool> Want_pass_async(Equipment e) => await Task.Run(() => _form.Want_Pass(e));
    }
}
