using System;

namespace DandLRemake
{
    static class EnemyList
    {
        public static Enemy[] enemies = new Enemy[]
        {
            new Slime(),
            new Phoenix(),
        };

        public static Enemy ReturnEnemy(int id)
        {
            var enemyForReturn = enemies[id];
            enemies[id] = (Enemy)enemies[id].Clone();
            return enemyForReturn;
        }
    }

    public sealed class Slime : Enemy
    {
        public override string[] ReturnEnvironment()
        {
            return image;
        }

        public override object Clone()
        {
            return new Slime();
        }

        public Slime()
        {
            HP = 10;
            Mana = 5;
            Armor = 1;
            DefaultDamage = 5;
            DodgeChance = 10;
            XP = 10;

            damageType = DamageType.Normal;

            Name = "Слизень";

            random = new Random();

            UpdateStats();
        }
    }

    public sealed class Phoenix : Enemy
    {

        public Phoenix()
        {
            HP = 16;
            Mana = 20;
            Armor = 0;
            DefaultDamage = 8;
            DodgeChance = 10;
            XP = 15;

            damageType = DamageType.Fire;

            Name = "Феникс";

            random = new Random();

            UpdateStats();
        }

        public override object Clone()
        {
            return new Phoenix();
        }
    }
}
