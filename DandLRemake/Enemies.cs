using System;
using DandLRemake.Effects;
using DandLRemake.Equip;
using DandLRemake.Items;

namespace DandLRemake
{
    static class EnemyList
    {
        public static Enemy[] enemies = new Enemy[]
        {
            new Slime(-1),
            new Phoenix(-1),
            new Imp(-1),
        };

        public static Enemy ReturnEnemy(int id, int level)
        {
            var enemyForReturn = (Enemy)enemies[id].Clone(level);
            return enemyForReturn;
        }
    }

    public sealed class Slime : Enemy
    {
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
            Gold = Convert.ToInt32(10 * statsMultiply);

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
            Gold = Convert.ToInt32(13 * statsMultiply);

            damageType = DamageType.Fire;

            Name = "Феникс";

            random = new Random();

            dropList.Add(new Potion(20));

            equipDropList.Add(new IronHelmet(20));
            equipDropList.Add(new IronChestplate(15));
            equipDropList.Add(new IronLeggings(18));
            equipDropList.Add(new IronBoots(25));

            UpdateEnvironment();
        }

        public override object Clone(int _level)
        {
            return new Phoenix(_level);
        }
    }

    public sealed class Imp : Enemy
    {
        public Imp(int _level) : base(_level)
        {
            HP = Convert.ToInt32(135 * statsMultiply);
            Mana = Convert.ToInt32(70 * statsMultiply);
            Armor = Convert.ToInt32(7 * statsMultiply);
            DefaultDamage = Convert.ToInt32(57 * statsMultiply);
            DodgeChance = 5;
            XP = Convert.ToInt32(135 * statsMultiply);
            Gold = Convert.ToInt32(20 * statsMultiply);

            damageType = DamageType.Fire;

            Name = "Чертенок";

            random = new Random();

            equipDropList.Add(new DevilsSlippers(100));

            UpdateEnvironment();
        }

        public override void Turn(ref Player player)
        {
            if (Mana > 30 && random.Next(1, 101) < 10)
            {
                Mana -= 30;
                Informer.SaveMessege($"{Name} пытается поджечь вас");
                if (random.Next(1, 101) < 40)
                    player.SetEffect(new Burn());
                else
                    Informer.SaveMessege("Но не вышло");
            }
            base.Turn(ref player);
        }

        public override object Clone(int _level)
        {
            return new Imp(_level);
        }
    }
}
