using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using Dungeons_And_Classes.Assets.Players;
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

        public const int MAX_DEFEAT = 2;
        public const int MAX_WIN = 2;

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

        public Action<Player>? Player_Enter_Dangeon;
        public Action<Player, bool, int>? Player_Exit_Dangeon;

        public Action? Game_Over;

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

                Player_Enter_Dangeon += player.On_Player_Enter_Dangeon;
                Player_Exit_Dangeon += player.On_Player_Exit_Dangeon;
            }

            Player_Exit_Dangeon += delegate (Player player, bool win, int _)
            {
                if (win)
                    player.Round_Win();
                else
                    player.Round_Defeat();
            };
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


            // Game cycle
            while (_players.Any(p => p.Win_Points == MAX_WIN) || _players.Where(p=> p.Defeat_Points != MAX_DEFEAT).Count() != 1)
            {
                Trade_Round_Result trade = Next_Trade_Round();

                Player travel_player = trade.Traveling_Player;

                foreach (var spell in _equipment.Spells.Where(s => s.Dropped == false))
                    _equipment.Make_Spell(travel_player.Make_Spell(_equipment, spell));

                Player_Enter_Dangeon?.Invoke(travel_player);
                Travel_Round_Result travel = Next_Travel_Round();

                Player_Exit_Dangeon?.Invoke(travel_player, travel.Result_Helth_Points > 0, travel.Result_Helth_Points);
            }

            foreach (var player in _players)
                player.Clear_Points();

            Game_Over?.Invoke();
        }

        private Trade_Round_Result Next_Trade_Round()
        {
            _players = _generator.Shuffle(_players);
            foreach(var player in _players)
            {
                player.Clear();
            }

            for (int card_i = 0, player_i = 0; card_i <= _source_cards.Length; player_i %= _players.Length)
            {
                Player player = _players[player_i];
                if (player.Passed || player.Defeat_Points == MAX_DEFEAT)
                {
                    player_i++;
                    continue;
                }

                if (player.Want_Pass(_equipment))
                {
                    // When you last who not passed
                    if (_players.Where(p => p.Passed == false && p.Defeat_Points < MAX_DEFEAT).Count() == 1)
                    {
                        return new(player);
                    }
                    player_i++;
                    Player_Passed?.Invoke(player);
                    continue;
                }

                // When you cant get a card
                if (card_i ==  _source_cards.Length)
                {
                    return new(player);
                }

                Monster card = _source_cards[card_i];
                Move_Data data;
                if (_equipment.Items.Any(i => i.Dropped == false))
                {
                     data = player.Make_Move(_equipment, card);
                }
                else
                {
                    player.Forsed_Add(card);
                    data = new();
                }

                if (data.Dropped_Item is not null)
                {
                    _equipment.Drop_Equipment(data.Dropped_Item.Type, data.Dropped_Item.Slot);
                    Player_Droped_Card?.Invoke(player, data.Dropped_Item);
                }
                else
                {
                    _dangeon_cards = _dangeon_cards.Append(card).ToArray();
                    Player_Added_Card?.Invoke(player);
                }

                player_i++;
                card_i++;
            }

            throw new IndexOutOfRangeException();
        }
        private Travel_Round_Result Next_Travel_Round()
        {
            Hero hero = new(_equipment);
            _dangeon_cards = _dangeon_cards.Reverse().ToArray();

            foreach(var card in _dangeon_cards)
            {
                hero.Try_Defiat(card);
            }
            return new(hero.Helth_Points);
        }
    }
}
