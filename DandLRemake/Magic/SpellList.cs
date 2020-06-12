using System;

namespace DandLRemake.Magic
{
    public static class SpellList
    {
            public static Spell[] magic = new Spell[]
            {
                new Cure(-1),
            };

            public static Spell ReturnMagic(int id, int level)
            {
                var magicForReturn = (Spell)magic[id].Clone(level);
                return magicForReturn;
            }
    }

    public sealed class Cure : Spell
    {
        public Cure(int _level) : base(_level)
        {
            Type = DamageType.Natural;
            defaultDamage = 60;
            Price = 7 * _level;
            Name = "Лечение";
        }

        public override object Clone(int _level)
        {
            return new Cure(_level);
        }

        public override bool Use(Enemy enemy, Player player)
        {
            var heal = defaultDamage * Level * player.Intelligence;
            if (player.HP.Value < player.HP.MaxValue)
            {
                player.ManaSpend(Price);
                player.Heal(heal);
                return false;
            }
            else
            {
                Informer.SaveMessege("Здоровье на максимуме");
                return true;
            }
        }
    }

    public sealed class EmptySpell : Spell
    {
        public EmptySpell(int _level) : base(_level) { }

        public override object Clone(int _level)
        {
            return new EmptySpell(1);
        }

        public override bool Use(Enemy enemy, Player player) { return true; }

        public override string ToString()
        {
            return ".........";
        }
    }
}