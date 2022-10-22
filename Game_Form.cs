using Dungeons_And_Classes.Assets;
using Dungeons_And_Classes.Assets.Dungeon;
using Dungeons_And_Classes.Assets.Game_Hero;
using Dungeons_And_Classes.Assets.Game_Hero.Hero_Items;
using Dungeons_And_Classes.Assets.Players;
using System.Diagnostics;

namespace Dungeons_And_Classes
{
    public partial class Game_Form : Form
    {
        private Human_PLayer _player;
        private Game _game;
        private Equipment? _equipment;

        private bool? _pass;
        private Move_Data? _move;


        public Game_Form()
        {
            InitializeComponent();
            _player = new Human_PLayer(this);
            _game = new Game(new Player[] { _player }
            .Append(new Random_Player())
            .Append(new Random_Player())
            .Append(new Random_Player())
            .ToArray());

            _game.Game_Over += delegate ()
            {
                Invoke(On_Game_Over);
            };
        }

        public Move_Data Make_Move(Equipment e, Monster monster)
        {
            _equipment = e;
            Invoke(Show_Items);
            _move = null;
            MessageBox.Show(monster.Name);
            while (_move is null)
            { }
            return _move.Value;
        }
        public Spell Make_Spell(Equipment e, Spell source) => Spell.Make_Death_Curse(source, Monster.Skeleton);
        public bool Want_Pass(Equipment e)
        {
            _pass = null;
            MessageBox.Show("Pass?");
            while (_pass is null)
            { }
            return _pass.Value;
        }

        private void Show_Items()
        {
            listBox1.Items.Clear();
            foreach (var item in _equipment!.Items)
                listBox1.Items.Add(item);
        }

        private void Start_B_Click(object sender, EventArgs e)
        {
            Start_B.Enabled = false;
            new Task(_game.StartGame).Start();
        }

        private void On_Game_Over()
        {
            Start_B.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _pass = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _pass = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _move = new();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _move = new(_equipment!.Items.ElementAt(listBox1.SelectedIndex));
        }
    }
}