using System;
using System.Collections.Generic;
using DandLRemake.Magic;
using DandLRemake.Items;
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
        public Property Level { get; protected set; }

        protected Spell magicSlot1;
        protected Spell magicSlot2;
        protected Spell magicSlot3;
        protected Spell magicSlot4;

        protected const int moralMultiply = 6;
        protected const int satietyMultiply = 3;
        protected const double statsMultiply = 1.1;

        protected int strength = 5;
        protected int endurance = 5;
        protected int dexterity = 5;
        protected int luck = 5;
        protected int intelligence = 5;

        public int Strength => strength;
        public int Endurance => endurance;
        public int Dexterity => dexterity;
        public int Luck => luck;
        public int Intelligence => intelligence;

        protected Random random;

        protected int baseDamage = 10;
        protected int baseDodgeChance = 1;
        protected int baseFleeChance = 20;

        public int BaseDamage { get { return baseDamage; } }
        public int BaseDodgeChance { get { return baseDodgeChance; } }
        public int BaseFleeChance { get { return baseFleeChance; } }

        public PropertyEditor MyStats;

        public bool IsDead { get; set; } = false;
        public bool IsOpenMenu { get; set; } = false;
        public bool IsOpenItemMenu { get; set; } = false;
        public bool IsFlee { get; set; } = false;
        protected int numberOfPocket = 0;
        protected List<Item> inventory;

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
            Level = MyStats.ReturnProperty(7);

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

            magicSlot1 = new Cure(1);
            magicSlot2 = new EmptySpell(1);
            magicSlot3 = new EmptySpell(1);
            magicSlot4 = new EmptySpell(1);

            inventory = new List<Item>();
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

        protected void SetMovesToShowToSpells()
        {
            MovesToShow = new string[5]
            {
                "1.Назад",
                "2." + magicSlot1.ToString(),
                "3." + magicSlot2.ToString(),
                "4." + magicSlot3.ToString(),
                "5."+ magicSlot4.ToString()
            };
        }

        protected void SetMovesToShowToItems()
        {
            MovesToShow[0] = "1.Назад";
            for (int i = 0; i < MovesToShow.Length - 1; i++)
            {
                if (inventory.Count > i + numberOfPocket * 4)
                    MovesToShow[i + 1] = $"{i + 2}.{inventory[i + numberOfPocket * 4]}"; 
                else
                    MovesToShow[i + 1] = $"{i + 2}..........";
            }

            MovesToShow[3] += " D -> ";
            MovesToShow[4] += " A <- ";
        }

        public bool Turn(char choise, Enemy enemy)
        {
            if(IsOpenItemMenu)
                return Item(enemy);
            if (IsOpenMenu)
                return Magic(enemy);
            else
            {
                switch (choise)
                {
                    case '1':
                        Attack(enemy);
                        return false;
                    case '2':
                        return Magic(enemy);
                    case '3':
                        Special(enemy);
                        return false;
                    case '4':
                        return Item(enemy);
                    case '5':
                        Flee();
                        return false;
                    default: return true;
                }
            }
        }

        public virtual void Flee()
        {
            if (random.Next(1, 101) < baseFleeChance * Luck / 5)
            {
                IsFlee = true;
                Informer.SaveMessege("Вы попытались сбежать... И у вас получилось!");
            }
            else
                Informer.SaveMessege("Вы попытались сбежать... Но не смогли");
        }

        public bool Item(Enemy enemy)
        {
            if (IsOpenItemMenu)
            {
                var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        SetMovesToShowToReal();
                        IsOpenMenu = false;
                        return true;
                    case '2':
                        if (inventory.Count > 0 + numberOfPocket * 4)
                        {
                            inventory[0 + numberOfPocket * 4].Use(this, enemy);
                            DeleteItem(inventory[0 + numberOfPocket * 4]);
                            IsOpenItemMenu = false;
                            return false;
                        }
                        return true;
                    case '3':
                        if (inventory.Count > 1 + numberOfPocket * 4)
                        {
                            inventory[1 + numberOfPocket * 4].Use(this, enemy);
                            DeleteItem(inventory[1 + numberOfPocket * 4]);
                            IsOpenItemMenu = false;
                            return false;
                        }
                        return true;
                    case '4':
                        if (inventory.Count > 2 + numberOfPocket * 4)
                        { 
                            inventory[2 + numberOfPocket * 4].Use(this, enemy);
                            DeleteItem(inventory[2 + numberOfPocket * 4]);
                            IsOpenItemMenu = false;
                            return false;
                        }
                        return true;
                    case '5':
                        if (inventory.Count > 3 + numberOfPocket * 4)
                        {
                            inventory[3 + numberOfPocket * 4].Use(this, enemy);
                            DeleteItem(inventory[3 + numberOfPocket * 4]);
                            IsOpenItemMenu = false;
                            return false;
                        }
                        return true;
                    case 'a':
                    case 'A':
                    case 'ф':
                    case 'Ф':
                        if (numberOfPocket - 1 < 0)
                            numberOfPocket = (inventory.Count - 1) / 4;
                        else
                            numberOfPocket--;
                        SetMovesToShowToItems();
                        return true;
                    case 'd':
                    case 'D':
                    case 'в':
                    case 'В':
                        if (numberOfPocket + 1 >= (double)inventory.Count / 4)
                            numberOfPocket = 0;
                        else
                            numberOfPocket++;
                        SetMovesToShowToItems();
                        return true;
                    default:
                        return true;
                }
            }
            else
            {
                IsOpenItemMenu = true;
                SetMovesToShowToItems();
                return true;
            }
        }

        public virtual void Special(Enemy enemy)
        {
            Informer.SaveMessege("Anything");
        }

        public virtual bool Magic(Enemy enemy)
        {
            //if return true - repeat
            if(IsOpenMenu)
            {
                var input = Console.ReadKey().KeyChar;
                switch(input)
                {
                    case '1':
                        SetMovesToShowToReal();
                        IsOpenMenu = false;
                        return true;
                    case '2':
                        if (Mana.Value >= magicSlot1.Price)
                        {
                            var result = magicSlot1.Use(enemy, this);
                            IsOpenMenu = result;
                            return result;
                        }
                        else
                        {
                            Informer.SaveMessege("Не хватает маны");
                            return true;
                        }
                    case '3':
                        if (Mana.Value >= magicSlot2.Price)
                        {
                            var result = magicSlot2.Use(enemy, this);
                            IsOpenMenu = result;
                            return result;
                        }
                        else
                        {
                            Informer.SaveMessege("Не хватает маны");
                            return true;
                        }
                    case '4':
                        if (Mana.Value >= magicSlot3.Price)
                        {
                            var result = magicSlot3.Use(enemy, this);
                            IsOpenMenu = result;
                            return result;
                        }
                        else
                        {
                            Informer.SaveMessege("Не хватает маны");
                            return true;
                        }
                    case '5':
                        if (Mana.Value >= magicSlot4.Price)
                        {
                            var result = magicSlot4.Use(enemy, this);
                            IsOpenMenu = result;
                            return result;
                        }
                        else
                        {
                            Informer.SaveMessege("Не хватает маны");
                            return true;
                        }
                    default:
                        return true;
                }
            }
            else
            {
                IsOpenMenu = true;
                SetMovesToShowToSpells();
                return true;
            }
        }

        public virtual void Attack(Enemy enemy)
        {
            Informer.SaveMessege("Игрок атакует");
            Morality.Value -= moralMultiply;

            var damage = (baseDamage * strength + random.Next(-1 * baseDamage/5 * strength, 2 * baseDamage/5 * strength)) ;

            enemy.ApplyDamage(damage, DamageType.Normal);
        }

        public virtual void ApplyDamage(int _baseDamage, DamageType type)
        {
            var damage = (_baseDamage - Armor.Value * endurance);
            if(random.Next(1, 101) > baseDodgeChance * dexterity)
                if (damage > 0)
                {
                    HP.Value -= damage;
                    Informer.SaveMessege($"Вы получаете {damage} урона");
                }
                else
                    Informer.SaveMessege("Вы получаете 0 урона");
            else
                Informer.SaveMessege("Вы увернулись");
            CheckLive();
        }

        public virtual void ManaSpend(int amount)
        {
            Mana.Value -= amount;
            Informer.SaveMessege($"Вы потратили {amount} маны");
        }

        public virtual void Heal(int amount)
        {
            HP.Value += amount;
            Informer.SaveMessege($"Вы восстановили {amount} здоровья");
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
                    Level.Value++;
                    IncreaseStats();
                    break;
                case '2':
                    endurance++;
                    repeat = false;
                    Level.Value++;
                    IncreaseStats();
                    break;
                case '3':
                    dexterity++;
                    repeat = false;
                    Level.Value++;
                    IncreaseStats();
                    break;
                case '4':
                    luck++;
                    repeat = false;
                    Level.Value++;
                    IncreaseStats();
                    break;
                case '5':
                    intelligence++;
                    repeat = false;
                    Level.Value++;
                    IncreaseStats();
                    break;
            }

            return repeat;
        }

        private void IncreaseStats()
        {
            HP.Value = Convert.ToInt32(HP.Value * statsMultiply);
            Mana.Value = Convert.ToInt32(Mana.Value * statsMultiply);
        }

        private void CheckLive()
        {
            if(HP.Value <= 0)
            {
                IsDead = true;
            }
        }

        public void ApplyItem(Item _item)
        {
            if (inventory.Count > 0)
            {
                foreach (var item in inventory)
                {
                    if (item.GetType() == _item.GetType())
                    {
                        item.Count++;
                        Informer.SaveMessege($"Вы получили {_item.Name}");
                        return;
                    }
                }
                inventory.Add(_item);
                Informer.SaveMessege($"Вы получили {_item.Name}");
            }
            else
                inventory.Add(_item);
            Informer.SaveMessege($"Вы получили {_item.Name}");

        }

        protected void DeleteItem(Item _item)
        {
            _item.Count--;
            if (_item.Count <= 0)
                inventory.Remove(_item);
        }
    }
}