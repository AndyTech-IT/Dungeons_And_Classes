using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_And_Classes.Assets.Dungeon
{
    public struct Monster
    {
        public readonly Monster_Type Type;
        public readonly string Name;
        public readonly int Power;

        public static Monster Unknown_Monster => new("Unknown", Monster_Type.Unknown);
        public static Monster Goblin => new("Гоблин", Monster_Type.Goblin);
        public static Monster Skeleton => new("Скелет", Monster_Type.Skeleton);
        public static Monster Ork => new("Орк", Monster_Type.Ork);
        public static Monster Vampire => new("Вампир", Monster_Type.Vampire);
        public static Monster Golem => new("Голем", Monster_Type.Golem);
        public static Monster Lich => new("Лич", Monster_Type.Lich);
        public static Monster Demon => new("Демон", Monster_Type.Demon);
        public static Monster Dragon  => new("Дракон", Monster_Type.Dragon);

        public Monster(string name, Monster_Type type)
        {
            Name = name;
            Type = type;
            Power = (int)type;
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
