using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using RandomGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets
{
    public class Game
    {
        private static Random_Generator _generator = new Random_Generator();

        private Monster[] _dangeon_cards;
        private Monster[] _source_cards;
        private Equipment _equipment;
        private Player[] _players;

        public Action<Player>? Player_Added_Card;
        public Action<Player, Item>? Player_Droped_Card;
        public Action<Player>? Player_Passed;

        public Game(Player[] players)
        {
            _dangeon_cards = Array.Empty<Monster>();
            _source_cards = Array.Empty<Monster>();
            _equipment = new Equipment();
            _players = players;

            foreach (var player in _players)
            {
                Player_Added_Card += player.On_Player_Add_Card;
                Player_Droped_Card+= player.On_Player_Drop_Card;
                Player_Passed += player.On_Player_Passed;
            }
        }

        public void StartGame()
        {
            _equipment.Clear();
            _source_cards = _generator.Shuffle
            (
                new[]
                {
                    Monster.Goblin, Monster.Goblin,
                    Monster.Skeleton, Monster.Skeleton,
                    Monster.Ork, Monster.Ork,
                    Monster.Vampire, Monster.Vampire,
                    Monster.Golem, Monster.Golem,
                    Monster.Lich,
                    Monster.Demon,
                    Monster.Dragon
                }
            );
            _dangeon_cards = Array.Empty<Monster>();

            Next_Traid_Round();
        }

        private void Next_Traid_Round()
        {
            foreach(var player in _players)
            {
                if (player.Passed)
                    continue;
            }
        }
    }
}
