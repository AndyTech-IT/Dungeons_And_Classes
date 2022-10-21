using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Dungeon
{
    public struct Monster
    {
        public readonly string Name;
        public readonly int Power;

        public static Monster Unknown_Monster => new("Unknown", 0);
        public static Monster Goblin => new("Гоблин", 1);
        public static Monster Skeleton => new("Скелет", 2);
        public static Monster Ork => new("Орк", 3);
        public static Monster Vampire => new("Вампир", 4);
        public static Monster Golem => new("Голем", 5);
        public static Monster Lich => new("Лич", 6);
        public static Monster Demon => new("Демон", 7);
        public static Monster Dragon  => new("Дракон", 9);

        public Monster(string name, int power)
        {
            Name = name;
            Power = power;
        }

        public override bool Equals(object? obj)
        {
            return obj is Monster monster &&
                   Power == monster.Power;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Power);
        }

        public static bool operator ==(Monster left, Monster right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Monster left, Monster right)
        {
            return !(left == right);
        }
    }
}
