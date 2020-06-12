using System;
using System.Collections.Generic;
using DandLRemake.Items;

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
            HP = 100;
            Mana = 50;
            Armor = 5;
            DefaultDamage = 50;
            DodgeChance = 10;
            XP = 100;

            damageType = DamageType.Normal;

            Name = "Слизень";

            random = new Random();

            dropList.Add(new Potion(15));
            dropList.Add(new Potion(10));

            UpdateEnvironment();
        }
    }

    public sealed class Phoenix : Enemy
    {

        public Phoenix()
        {
            HP = 16;
            Mana = 200;
            Armor = 0;
            DefaultDamage = 80;
            DodgeChance = 10;
            XP = 150;

            damageType = DamageType.Fire;

            Name = "Феникс";

            random = new Random();

            UpdateEnvironment();
        }

        public override object Clone()
        {
            return new Phoenix();
        }
    }
}
