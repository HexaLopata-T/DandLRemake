using System;
using DandLRemake.Items;

namespace DandLRemake
{
    static class EnemyList
    {
        public static Enemy[] enemies = new Enemy[]
        {
            new Slime(-1),
            new Phoenix(-1),
        };

        public static Enemy ReturnEnemy(int id, int level)
        {
            var enemyForReturn = (Enemy)enemies[id].Clone(level);
            return enemyForReturn;
        }
    }

    public sealed class Slime : Enemy
    {
        public override string[] ReturnEnvironment()
        {
            return image;
        }

        public override object Clone(int _level)
        {
            return new Slime(_level);
        }

        public Slime(int _level) : base(_level)
        {
            HP = Convert.ToInt32(100 * statsMultiply);
            Mana = Convert.ToInt32(50 * statsMultiply);
            Armor = Convert.ToInt32(5 * statsMultiply);
            DefaultDamage = Convert.ToInt32(50 * statsMultiply);
            DodgeChance = 10;
            XP = Convert.ToInt32(100 * statsMultiply);

            damageType = DamageType.Normal;

            Name = "Слизень";

            random = new Random();

            dropList.Add(new Potion(15));
            dropList.Add(new Shuriken(10));

            UpdateEnvironment();
        }
    }

    public sealed class Phoenix : Enemy
    {

        public Phoenix(int _level) : base(_level)
        {
            HP = Convert.ToInt32(160 * statsMultiply);
            Mana = Convert.ToInt32(200 * statsMultiply);
            Armor = 0;
            DefaultDamage = Convert.ToInt32(80 * statsMultiply);
            DodgeChance = 10;
            XP = Convert.ToInt32(150 * statsMultiply);

            damageType = DamageType.Fire;

            Name = "Феникс";

            random = new Random();

            UpdateEnvironment();
        }

        public override object Clone(int _level)
        {
            return new Phoenix(_level);
        }
    }
}
