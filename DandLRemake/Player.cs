using System;
using DandLRemake.PropertiesAppointee;

namespace DandLRemake
{

    public class Player
    {
        public Property HP { get; protected set; }
        public Property Mana { get; protected set; }
        public Property Armor { get; protected set; }
        public Property Gold { get; protected set; }
        public Property Satiety { get; protected set; }
        public Property Morality { get; protected set; }
        public Property XP { get; protected set; }

        private const int moralMultiply = 6;
        private const int satietyMultiply = 3;

        private int strength = 5;
        private int endurance = 5;
        private int dexterity = 5;
        private int luck = 5;
        private int intelligence = 5;

        protected Random random;

        protected int baseDamage = 5;
        protected int baseDodgeChance = 5;

        public PropertyEditor MyStats;

        public bool IsDead = false;

        public static string[] MovesToShow { get; protected set; }
        public static string[] RealMoves { get; protected set; }

        public Player(int _additionalStr = 0, int _additionalEndur = 0, int _additionalDex = 0, int _additionalLuck = 0, int _additionalIntel = 0)
        {
            MyStats = new PropertyEditor();

            HP = MyStats.ReturnProperty(0);
            Mana = MyStats.ReturnProperty(1);
            Armor = MyStats.ReturnProperty(2);
            Gold = MyStats.ReturnProperty(3);
            Satiety = MyStats.ReturnProperty(4);
            Morality = MyStats.ReturnProperty(5);
            XP = MyStats.ReturnProperty(6);

            random = new Random();

            strength += _additionalStr;
            endurance += _additionalEndur;
            dexterity += _additionalDex;
            luck += _additionalLuck;
            intelligence += _additionalIntel;

            RealMoves = new string[5]
            {
                "1.Удар",
                "2.Магия",
                "3.Специальное умение",
                "4.Предметы",
                "5.Побег"
            };

            MovesToShow = new string[5];
            SetMovesToShowToReal();
        }

        public void SetMovesToShowToContinue()
        {
            MovesToShow = new string[5]
            {
                "1.Продолжить",
                "",
                "",
                "",
                ""
            };
        }

        public void SetMovesToShowToReal()
        {
            for (int i = 0; i < MovesToShow.Length; i++)
            {
                MovesToShow[i] = RealMoves[i];
            }
        }

        public void SetMovesToShowForLevelUp()
        {
            MovesToShow = new string[5]
            {
                $"1.Сила {strength}",
                $"2.Выносливость {endurance}",
                $"3.Ловкость {dexterity}",
                $"4.Удача {luck}",
                $"5.Интеллект {intelligence}"
            };
        }

        public bool Turn(char choise, Enemy enemy)
        {
            switch(choise)
            {
                case '1':
                    Attack(enemy);
                    return false;
                case '2':
                    Magic(enemy);
                    return false;
                case '3':
                    Special(enemy);
                    return false;
                case '4':
                    Item(enemy);
                    return false;
                case '5':
                    Flee();
                    return false;
                default: return true;
            }
        }

        public virtual void Flee()
        {
            Informer.SaveMessege("Anything");
        }

        public void Item(Enemy enemy)
        {
            Informer.SaveMessege("Anything");
        }

        public virtual void Special(Enemy enemy)
        {
            Informer.SaveMessege("Anything");
        }

        public virtual void Magic(Enemy enemy)
        {
            Informer.SaveMessege("Anything");
        }

        public virtual void Attack(Enemy enemy)
        {
            Informer.SaveMessege("Игрок атакует");
            Morality.Value -= moralMultiply;

            var damage = baseDamage + random.Next(-1 * baseDamage/5, 2 * baseDamage/5) * strength/5;

            enemy.ApplyDamage(damage, DamageType.Normal);
        }

        public virtual void ApplyDamage(int _basedamage, DamageType type)
        {
            var damage = (_basedamage - Armor.Value * endurance / 5);
            if(random.Next(1, 101) > baseDodgeChance * dexterity / 5)
            if (damage > 0)
            {
                HP.Value -= damage;
                Informer.SaveMessege($"Вы получаете {damage} урона");
            }
            else
                Informer.SaveMessege($"Вы получаете 0 урона");
            CheckLive();
        }

        public virtual void Hunger()
        {
            Satiety.Value -= satietyMultiply;
            CheckLive();
        }

        public bool ApplyXP(int value)
        {
            XP.Value += value;
            Informer.SaveMessege($"Вы получили {value} опыта");

            if (XP.Value >= XP.MaxValue)
            {
                Informer.SaveMessege("Вы получили новый уровень");
                XP.MaxValue += XP.MaxValue / 2;
                XP.Value = 0;
                return true;
            }
            return false;
        }

        public bool ApplyLevel()
        {
            var choise = Console.ReadKey().KeyChar;
            var repeat = true;

            switch(choise)
            {
                case '1':
                    strength++;
                    repeat = false;
                    break;
                case '2':
                    endurance++;
                    repeat = false;
                    break;
                case '3':
                    dexterity++;
                    repeat = false;
                    break;
                case '4':
                    luck++;
                    repeat = false;
                    break;
                case '5':
                    intelligence++;
                    repeat = false;
                    break;
            }

            return repeat;
        }

        private void CheckLive()
        {
            if(HP.Value <= 0)
            {
                IsDead = true;
            }
        }
    }
}
