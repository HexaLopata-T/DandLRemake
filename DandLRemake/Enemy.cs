using System;
using System.Collections.Generic;
using DandLRemake.Items;

namespace DandLRemake
{
    public abstract class Enemy : IHaveEnvironment, ICloneable
    {
        protected Random random;
        protected string[] image;

        private int hP;
        private int mana;
        private int armor;
        private int defaultDamage;
        private int dodgeChance;
        private int xP;

        protected List<Item> dropList = new List<Item>();

        public const int maxStat = 10000;

        public int HP { get { return hP; } protected set { if (value >= 0 & value <= maxStat) { hP = value; } else hP = 0; } }
        public int Mana { get { return mana; } protected set { if (value >= 0 & value <= maxStat) { mana = value; } else mana = 0; } }
        public int Armor { get { return armor; } protected set { if (value >= 0 & value <= maxStat) { armor = value; } else armor = 0; } }
        public int DefaultDamage { get { return defaultDamage; } protected set { if (value >= 0 & value <= maxStat) { defaultDamage = value; } else defaultDamage = 0; } }
        public int DodgeChance { get { return dodgeChance; } protected set { if (value >= 0 & value <= maxStat) { dodgeChance = value; } else dodgeChance = 0; } }
        public int XP { get { return xP; } protected set { if (value >= 0 & value <= maxStat) { xP = value; } else xP = 0; } }
        public string Name { get; protected set; } = "Враг";
        public DamageType damageType { get; protected set; } = DamageType.Normal;

        public virtual string[] ReturnEnvironment()
        {
            return image;
        }

        public virtual void Turn(ref Player player)
        {
            Informer.SaveMessege($"{Name} атакует");
            player.ApplyDamage(DefaultDamage + random.Next(-DefaultDamage/5, 2 * DefaultDamage/5), damageType);
        }

        public virtual void ApplyDamage(int damage, DamageType type)
        {
            if (random.Next(1, 101) >= DodgeChance && damage - Armor > 0)
            {
                HP -= (damage - Armor);
                Informer.SaveMessege($"{Name} получает {(damage - Armor)} урона");
                UpdateEnvironment();
            }
            else
            {
                Informer.SaveMessege($"{Name} увернулся");
            }
        }

        public virtual void UpdateEnvironment()
        {
            image = image = new string[]
            {
                $"Перед вами {Name}",
                $"Здоровье: {HP}",
                $"Мана: {Mana}"
            };
        }

        public abstract object Clone();

        public void DropItems(Player player)
        {
            foreach(var item in dropList)
            {
                if (random.Next(1, 101) <= item.DropChance)
                    player.ApplyItem(item);
            }
        }

    }
}