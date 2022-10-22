using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets
{
    public abstract class Player
    {
        public bool Passed { get; private set; }

        public int Win_Points { get; private set; }
        public int Defeat_Points { get; private set; }

        protected IEnumerable<Monster> _dangeon_cards;
        protected IEnumerable<Monster> _dropped_cards;

        public abstract void On_Player_Add_Card(Player actor);
        public abstract void On_Player_Drop_Card(Player actor, Item cost);
        public abstract void On_Player_Passed(Player actor);
        public abstract void On_Player_Enter_Dangeon(Player actor);
        public abstract void On_Player_Exit_Dangeon(Player actor, bool result, int hp);

        public Player()
        {
            Passed = false;
            _dangeon_cards = Array.Empty<Monster>();
            _dropped_cards = Array.Empty<Monster>();
            Win_Points = 0;
            Defeat_Points = 0;
        }

        public void Clear()
        {
            Passed = false;
            _dangeon_cards = Array.Empty<Monster>();
            _dropped_cards = Array.Empty<Monster>();
            Win_Points = 0;
            Defeat_Points = 0;
        }

        public void Round_Win() => Win_Points++;
        public void Round_Defeat() => Defeat_Points++;

        public bool Want_Pass(Equipment e)
        {
            Passed = Want_pass(e);
            return Passed;
        }

        public Move_Data Make_Move(Equipment e, Monster monster)
        {
            Move_Data result = Make_move(e, monster);
            if (result.Dropped_Item is not null)
            {
                _dropped_cards = _dropped_cards.Append(monster);
            }
            else
            {
                _dangeon_cards = _dangeon_cards.Append(monster);
            }
            return result;
        }

        public Spell Make_Spell(Equipment e, Spell source) => Make_spell(e, source);


        protected abstract bool Want_pass(Equipment e);
        protected abstract Move_Data Make_move(Equipment e, Monster monster);
        protected abstract Spell Make_spell(Equipment e, Spell source);
    }
}
