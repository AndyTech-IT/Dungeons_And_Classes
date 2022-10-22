using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using RandomGenerator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets
{
    public class Game
    {
        private static Random_Generator _generator = new();

        private static Monster_Type[] Cards_Pack
            => new[]
            {
                Monster_Type.Goblin, Monster_Type.Goblin,
                Monster_Type.Skeleton, Monster_Type.Skeleton,
                Monster_Type.Ork, Monster_Type.Ork,
                Monster_Type.Vampire, Monster_Type.Vampire,
                Monster_Type.Golem, Monster_Type.Golem,
                Monster_Type.Lich,
                Monster_Type.Demon,
                Monster_Type.Dragon
            };

        private readonly Monster_Type[]? _cards_template;
        private Monster[] _dangeon_cards;
        private Monster[] _source_cards;
        private Equipment _equipment;
        private Player[] _players;

        public Action<Player>? Player_Added_Card;
        public Action<Player, Item>? Player_Droped_Card;
        public Action<Player>? Player_Passed;

        public Game(Player[] players, Monster_Type[]? cards_template = null)
        {
            _dangeon_cards = Array.Empty<Monster>();
            _source_cards = Array.Empty<Monster>();
            _equipment = new Equipment();
            _players = players;

            if (cards_template is not null && cards_template.OrderBy(c => c).ToArray() == Cards_Pack)
                _cards_template = cards_template;
            else
            {
                Debug.WriteLine($"Cards template is wrong!");
                _cards_template = null;
            }

            foreach (var player in _players)
            {
                Player_Added_Card += player.On_Player_Add_Card;
                Player_Droped_Card+= player.On_Player_Drop_Card;
                Player_Passed += player.On_Player_Passed;
            }
        }

        public void StartGame()
        {
            Monster_Type[] cards;
            _equipment.Clear();
            if (_cards_template is null)
                cards = _generator.Shuffle(Cards_Pack);
            else
                cards = _cards_template;

            _source_cards = cards
                .Select(c => c switch
                {
                    Monster_Type.Goblin => Monster.Goblin,
                    Monster_Type.Skeleton => Monster.Skeleton,
                    Monster_Type.Ork => Monster.Ork,
                    Monster_Type.Vampire => Monster.Vampire,
                    Monster_Type.Golem => Monster.Golem,
                    Monster_Type.Lich => Monster.Lich,
                    Monster_Type.Demon => Monster.Demon,
                    Monster_Type.Dragon => Monster.Dragon,
                    _ => throw new NotSupportedException(nameof(c)),
                })
                .ToArray();

            _dangeon_cards = Array.Empty<Monster>();

            Next_Traid_Round();
        }

        private Traid_Round_Result Next_Traid_Round()
        {
            _players = _generator.Shuffle(_players);
            foreach(var player in _players)
            {
                player.Clear();
            }

            for (int card_i = 0, player_i = 0; card_i < _source_cards.Length; player_i %= _players.Length)
            {
                if (_players[player_i].Passed)
                {
                    player_i++;
                    continue;
                }

                _players[player_i].Move_Or_Pass(_equipment);
            }
        }
        private Travel_Round_Result Next_Travel_Round()
        {

        }
    }
}
