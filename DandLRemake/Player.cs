using System;
using System.Collections.Generic;
using DandLRemake.Magic;
using DandLRemake.Items;
using DandLRemake.Equip;
using DandLRemake.PropertiesAppointee;
using DandLRemake.Effects;

namespace DandLRemake
{
    public class Player
    {
        #region Properties
        public Property HP { get; protected set; }
        public Property Mana { get; protected set; }
        public Property Armor { get; protected set; }
        public Property Gold { get; protected set; }
        public Property Satiety { get; protected set; }
        public Property Morality { get; protected set; }
        public Property XP { get; protected set; }
        public Property Level { get; protected set; }
        #endregion

        protected Spell[] spells;

        #region Equip
        protected Helmet helmet;
        protected Chestplate chestplate;
        protected Leggings leggings;
        protected Boots boots;
        protected Ring ring1;
        protected Weapon weapon;
        #endregion

        public Effect State { get; protected set; } = new None();

        #region Inventories
        protected List<Helmet> helmetInventory;
        protected List<Chestplate> chestplatesInventory;
        protected List<Leggings> leggingsInventory;
        protected List<Boots> bootsInventory;
        protected List<Ring> ringsInventory;
        protected List<Weapon> weaponsInventory;
        protected List<Item> itemInventory;
        #endregion

        #region Multiplies
        protected const int moralMultiply = 6;
        protected const int satietyMultiply = 3;
        protected const double statsMultiply = 1.1;
        #endregion

        #region Stats
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
        #endregion

        protected Random random;

        #region BaseStats
        protected int baseDamage = 10;
        protected int baseDodgeChance = 1;
        protected int baseFleeChance = 20;


        public int BaseDamage { get { return baseDamage; } }
        public int BaseDodgeChance { get { return baseDodgeChance; } }
        public int BaseFleeChance { get { return baseFleeChance; } }
        #endregion

        public PropertyEditor MyStats;

        public bool IsDead { get; set; } = false;
        public bool IsFlee { get; set; } = false;

        public MenuPage IsMenuOpenOn { get; protected set; }
        protected int numberOfPocket = 0;
        protected const int pocketSize = 6;

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

            IsMenuOpenOn = new MenuPage();
            IsMenuOpenOn = MenuPage.Main;

            RealMoves = new string[7]
            {
                "1.Удар",
                "2.Магия",
                "3.Специальное умение",
                "4.Предметы",
                "5.Побег",
                "6.Изменить снаряжение",
                ""
            };

            MovesToShow = new string[7];
            SetMovesToShowToReal();

            spells = new Spell[6]
            {
                new Cure(1),
                new EmptySpell(1),
                new EmptySpell(1),
                new EmptySpell(1),
                new EmptySpell(1),
                new EmptySpell(1),
            };

            helmet = new Helmet(0);
            chestplate = new Chestplate(0);
            leggings = new Leggings(0);
            boots = new Boots(0);
            ring1 = new Ring(0);
            weapon = new Weapon(0);

            itemInventory = new List<Item>();
            helmetInventory = new List<Helmet>();
            chestplatesInventory = new List<Chestplate>();
            leggingsInventory = new List<Leggings>();
            bootsInventory = new List<Boots>();
            ringsInventory = new List<Ring>();
            weaponsInventory = new List<Weapon>();
        }

        #region Set moves to show
        public void SetMovesToShowToContinue()
        {
            MovesToShow = new string[7]
            {
                "1.Продолжить",
                "",
                "",
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
            MovesToShow = new string[7]
            {
                $"1.Сила {strength}",
                $"2.Выносливость {endurance}",
                $"3.Ловкость {dexterity}",
                $"4.Удача {luck}",
                $"5.Интеллект {intelligence}",
                "",
                ""
            };
        }

        protected void SetMovesToShowToSpells()
        {
            MovesToShow = new string[7]
            {
                "1.Назад",
                "2." + spells[0].ToString(),
                "3." + spells[1].ToString(),
                "4." + spells[2].ToString(),
                "5."+ spells[3].ToString(),
                "6." + spells[4].ToString(),
                "7." + spells[5].ToString()
            };
        }

        protected void SetMovesToShowToAnyInventory<T>(List<T> inventory)
        {
            MovesToShow[0] = "1.Назад";
            for (int i = 0; i < MovesToShow.Length - 1; i++)
            {
                if (inventory.Count > i + numberOfPocket * pocketSize)
                    MovesToShow[i + 1] = $"{i + 2}.{inventory[i + numberOfPocket * pocketSize]}";
                else
                    MovesToShow[i + 1] = $"{i + 2}..........";
            }
            if (inventory.Count > pocketSize)
            {
                MovesToShow[5] += " D -> ";
                MovesToShow[6] += " A <- ";
            }
        }

        protected void SetMovesToShowToEquip()
        {
            MovesToShow = new string[7]
            {
                "1.Назад",
                "2." + helmet.ToString(),
                "3." + chestplate.ToString(),
                "4." + leggings.ToString(),
                "5." + boots.ToString(),
                "6." + ring1.ToString(),
                "7." + weapon.ToString()
            };
        }
        #endregion

        public bool Turn(char choise, Enemy enemy)
        {
            bool repeat = true;
            if (IsMenuOpenOn == MenuPage.Item)
            {
                repeat = Item(enemy);
                OnAnyTurn(enemy, repeat);
                return repeat;
            }
            if (IsMenuOpenOn == MenuPage.Magic)
            {
                repeat = Magic(enemy);
                OnAnyTurn(enemy, repeat);
                return repeat;
            }
            if (IsMenuOpenOn != MenuPage.Main && IsMenuOpenOn != MenuPage.Magic && IsMenuOpenOn != MenuPage.Item)
            {
                repeat = ChangeEquip();
                OnAnyTurn(enemy, repeat);
                return repeat;
            }
            else
            {
                switch (choise)
                {
                    case '1':
                        Attack(enemy);
                        OnAnyTurn(enemy, false);
                        return false;
                    case '2':
                        repeat = Magic(enemy);
                        OnAnyTurn(enemy, repeat);
                        return repeat;
                    case '3':
                        Special(enemy);
                        OnAnyTurn(enemy, false);
                        return false;
                    case '4':
                        repeat = Item(enemy);
                        OnAnyTurn(enemy, repeat);
                        return repeat;
                    case '5':
                        Flee();
                        OnAnyTurn(enemy, false);
                        return false;
                    case '6':
                        repeat = ChangeEquip();
                        OnAnyTurn(enemy, repeat);
                        return repeat;
                    default: return true;
                }
            }
        }

        private bool ChangeEquip()
        {
            if (IsMenuOpenOn == MenuPage.Equip)
            {
                var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        SetMovesToShowToReal();
                        IsMenuOpenOn = MenuPage.Main;
                        return true;
                    case '2':
                        IsMenuOpenOn = MenuPage.Helmets;
                        SetMovesToShowToAnyInventory(helmetInventory);
                        numberOfPocket = 0;
                        return true;
                    case '3':
                        IsMenuOpenOn = MenuPage.Chestplates;
                        SetMovesToShowToAnyInventory(chestplatesInventory);
                        numberOfPocket = 0;
                        return true;
                    case '4':
                        IsMenuOpenOn = MenuPage.Leggings;
                        SetMovesToShowToAnyInventory(leggingsInventory);
                        numberOfPocket = 0;
                        return true;
                    case '5':
                        IsMenuOpenOn = MenuPage.Boots;
                        SetMovesToShowToAnyInventory(bootsInventory);
                        numberOfPocket = 0;
                        return true;
                    case '6':
                        IsMenuOpenOn = MenuPage.Rings;
                        SetMovesToShowToAnyInventory(ringsInventory);
                        numberOfPocket = 0;
                        return true;
                    case '7':
                        IsMenuOpenOn = MenuPage.Weapons;
                        SetMovesToShowToAnyInventory(weaponsInventory);
                        numberOfPocket = 0;
                        return true;
                    default:
                        return true;
                }
            }
            else
            {
                switch (IsMenuOpenOn)
                {
                    case MenuPage.Main:
                        IsMenuOpenOn = MenuPage.Equip;
                        SetMovesToShowToEquip();
                        return true;
                    case MenuPage.Helmets:
                        ChangeCurrentEquip(ref helmet, helmetInventory);
                        return true;
                    case MenuPage.Chestplates:
                        ChangeCurrentEquip(ref chestplate, chestplatesInventory);
                        return true;
                    case MenuPage.Leggings:
                        ChangeCurrentEquip(ref leggings, leggingsInventory);
                        return true;
                    case MenuPage.Boots:
                        ChangeCurrentEquip(ref boots, bootsInventory);
                        return true;
                    case MenuPage.Rings:
                        ChangeCurrentEquip(ref ring1, ringsInventory);
                        return true;
                    case MenuPage.Weapons:
                        ChangeCurrentEquip(ref weapon, weaponsInventory);
                        return true;
                    default:
                        IsMenuOpenOn = MenuPage.Main;
                        SetMovesToShowToReal();
                        return true;
                }
            }
        }

        private void ChangeCurrentEquip<T>(ref T equip, List<T> inventory)
        {
            var input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '1':
                    SetMovesToShowToEquip();
                    IsMenuOpenOn = MenuPage.Equip;
                    break;
                case '2':
                    SetEquip(ref equip, inventory, 0);
                    break;
                case '3':
                    SetEquip(ref equip, inventory, 1);
                    break;
                case '4':
                    SetEquip(ref equip, inventory, 2);
                    break;
                case '5':
                    SetEquip(ref equip, inventory, 3);
                    break;
                case '6':
                    SetEquip(ref equip, inventory, 4);
                    break;
                case '7':
                    SetEquip(ref equip, inventory, 5);
                    break;
                case 'a':
                case 'A':
                case 'ф':
                case 'Ф':
                    if (numberOfPocket - 1 < 0)
                        numberOfPocket = (inventory.Count - 1) / pocketSize;
                    else
                        numberOfPocket--;
                    SetMovesToShowToAnyInventory(inventory);
                    break;
                case 'd':
                case 'D':
                case 'в':
                case 'В':
                    if (numberOfPocket + 1 >= (double)inventory.Count / pocketSize)
                        numberOfPocket = 0;
                    else
                        numberOfPocket++;
                    SetMovesToShowToAnyInventory(inventory);
                    break;
                default:
                    break;
            }
        }

        private void SetEquip<T>(ref T equip, List<T> inventory, int number)
        {
            number += numberOfPocket * pocketSize;
            var takenOffEquip = equip;
            if (number < inventory.Count)
            {
                OnUnequip();
                equip = inventory[number];
                inventory.RemoveAt(number);
                inventory.Add(takenOffEquip);
                OnEquip();
            }
            SetMovesToShowToAnyInventory(inventory);
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
            if (IsMenuOpenOn == MenuPage.Item)
            {
                var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        SetMovesToShowToReal();
                        IsMenuOpenOn = MenuPage.Main;
                        return true;
                    case '2':
                        return UseItem(enemy, 0);
                    case '3':
                        return UseItem(enemy, 1);
                    case '4':
                        return UseItem(enemy, 2);
                    case '5':
                        return UseItem(enemy, 3);
                    case '6':
                        return UseItem(enemy, 4);
                    case '7':
                        return UseItem(enemy, 5);
                    case 'a':
                    case 'A':
                    case 'ф':
                    case 'Ф':
                        if (numberOfPocket - 1 < 0)
                            numberOfPocket = (itemInventory.Count - 1) / pocketSize;
                        else
                            numberOfPocket--;
                        SetMovesToShowToAnyInventory(itemInventory);
                        return true;
                    case 'd':
                    case 'D':
                    case 'в':
                    case 'В':
                        if (numberOfPocket + 1 >= (double)itemInventory.Count / pocketSize)
                            numberOfPocket = 0;
                        else
                            numberOfPocket++;
                        SetMovesToShowToAnyInventory(itemInventory);
                        return true;
                    default:
                        return true;
                }
            }
            else
            {
                IsMenuOpenOn = MenuPage.Item;
                numberOfPocket = 0;
                SetMovesToShowToAnyInventory(itemInventory);
                return true;
            }
        }

        private bool UseItem(Enemy enemy, int number)
        {
            if (itemInventory.Count > number + numberOfPocket * pocketSize)
            {
                itemInventory[number + numberOfPocket * pocketSize].Use(this, enemy);
                DeleteItem(itemInventory[number + numberOfPocket * pocketSize]);
                IsMenuOpenOn = MenuPage.Main;
                return false;
            }
            return true;
        }

        public virtual void Special(Enemy enemy)
        {
            Informer.SaveMessege("Anything");
        }

        public virtual bool Magic(Enemy enemy)
        {
            //if return true - repeat
            if (IsMenuOpenOn == MenuPage.Magic)
            {
                var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        SetMovesToShowToReal();
                        IsMenuOpenOn = MenuPage.Main;
                        return true;
                    case '2':
                        return UseSpell(enemy, 0);
                    case '3':
                        return UseSpell(enemy, 1);
                    case '4':
                        return UseSpell(enemy, 2);
                    case '5':
                        return UseSpell(enemy, 3);
                    case '6':
                        return UseSpell(enemy, 4);
                    case '7':
                        return UseSpell(enemy, 5);
                    default:
                        return true;
                }
            }
            else
            {
                IsMenuOpenOn = MenuPage.Magic;
                SetMovesToShowToSpells();
                return true;
            }
        }

        private bool UseSpell(Enemy enemy, int number)
        {
            if (Mana.Value >= spells[number].Price)
            {
                var result = spells[number].Use(enemy, this);
                if (result)
                    IsMenuOpenOn = MenuPage.Magic;
                else
                    IsMenuOpenOn = MenuPage.Main;
                return result;
            }
            else
            {
                Informer.SaveMessege("Не хватает маны");
                return true;
            }
        }

        public virtual void Attack(Enemy enemy)
        {
            Informer.SaveMessege("Игрок атакует");
            Morality.Value -= moralMultiply;

            var damage = (baseDamage * strength + random.Next(-1 * baseDamage / 5 * strength, 2 * baseDamage / 5 * strength));
            OnAttack(enemy, damage);

            enemy.ApplyDamage(damage, DamageType.Normal);
        }

        #region Effects
        private void OnAttack(Enemy enemy, int damage)
        {
            helmet.OnAttack(this, enemy, damage);
            chestplate.OnAttack(this, enemy, damage);
            leggings.OnAttack(this, enemy, damage);
            boots.OnAttack(this, enemy, damage);
            ring1.OnAttack(this, enemy, damage);
            weapon.OnAttack(this, enemy, damage);
            State.OnAttack(this, enemy, damage);
        }
        private void OnDamage(int damage, DamageType damageType, Enemy enemy)
        {
            helmet.OnDamage(damage, damageType, this, enemy);
            chestplate.OnDamage(damage, damageType, this, enemy);
            leggings.OnDamage(damage, damageType, this, enemy);
            boots.OnDamage(damage, damageType, this, enemy);
            ring1.OnDamage(damage, damageType, this, enemy);
            weapon.OnDamage(damage, damageType, this, enemy);
            State.OnDamage(damage, damageType, this, enemy);

        }
        private void OnDamage(int damage, DamageType damageType)
        {
            helmet.OnDamage(damage, damageType, this);
            chestplate.OnDamage(damage, damageType, this);
            leggings.OnDamage(damage, damageType, this);
            boots.OnDamage(damage, damageType, this);
            ring1.OnDamage(damage, damageType, this);
            weapon.OnDamage(damage, damageType, this);
            State.OnDamage(damage, damageType, this);

        }
        private void OnAnyTurn(Enemy enemy, bool repeat)
        {
            helmet.OnAnyTurn(this, enemy, repeat);
            chestplate.OnAnyTurn(this, enemy, repeat);
            leggings.OnAnyTurn(this, enemy, repeat);
            boots.OnAnyTurn(this, enemy, repeat);
            ring1.OnAnyTurn(this, enemy, repeat);
            weapon.OnAnyTurn(this, enemy, repeat);
            State.OnAnyTurn(this, enemy, repeat);
        }
        private void OnEquip()
        {
            helmet.OnEquip(this);
            chestplate.OnEquip(this);
            leggings.OnEquip(this);
            boots.OnEquip(this);
            ring1.OnEquip(this);
            weapon.OnEquip(this);
        }
        private void OnUnequip()
        {
            helmet.OnUnequip(this);
            chestplate.OnUnequip(this);
            leggings.OnUnequip(this);
            boots.OnUnequip(this);
            ring1.OnUnequip(this);
            weapon.OnUnequip(this);
        }
        #endregion

        public virtual void ApplyDamage(int _baseDamage, DamageType type, Enemy enemy)
        {
            var damage = (_baseDamage - Armor.Value * endurance / 2);
            OnDamage(_baseDamage, type, enemy);

            if (random.Next(1, 101) > baseDodgeChance * dexterity)
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

        public virtual void ApplyDamage(int _baseDamage, DamageType type)
        {
            var damage = (_baseDamage - Armor.Value * endurance / 2);
            OnDamage(_baseDamage, type);

            if (random.Next(1, 101) > baseDodgeChance * dexterity)
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
            if (HP.Value + amount >= HP.MaxValue)
                amount -= (HP.Value + amount - HP.MaxValue);
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

            switch (choise)
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
            HP.MaxValue = Convert.ToInt32(HP.MaxValue * statsMultiply);
            Mana.MaxValue = Convert.ToInt32(Mana.MaxValue * statsMultiply);
        }

        private void CheckLive()
        {
            if (HP.Value <= 0 || Satiety.Value <= 0 || Morality.Value <= 0)
            {
                IsDead = true;
            }
        }

        public void ApplyItem(Item _item)
        {
            if (itemInventory.Count > 0)
            {
                foreach (var item in itemInventory)
                {
                    if (item.GetType() == _item.GetType())
                    {
                        item.Count++;
                        Informer.SaveMessege($"Вы получили {_item.Name}");
                        return;
                    }
                }
                itemInventory.Add(_item);
                Informer.SaveMessege($"Вы получили {_item.Name}");
            }
            else
            {
                itemInventory.Add(_item);
                Informer.SaveMessege($"Вы получили {_item.Name}");
            }

        }

        public void ApplyEquip(Equippable equippable)
        {
            if (equippable is Helmet)
            {
                helmetInventory.Add((Helmet)equippable);
                Informer.SaveMessege($"Вы получили {equippable.Name}");
            }
            if (equippable is Chestplate)
            {
                chestplatesInventory.Add((Chestplate)equippable);
                Informer.SaveMessege($"Вы получили {equippable.Name}");
            }
            if (equippable is Leggings)
            {
                leggingsInventory.Add((Leggings)equippable);
                Informer.SaveMessege($"Вы получили {equippable.Name}");
            }
            if (equippable is Boots)
            {
                bootsInventory.Add((Boots)equippable);
                Informer.SaveMessege($"Вы получили {equippable.Name}");
            }
            if (equippable is Ring)
            {
                ringsInventory.Add((Ring)equippable);
                Informer.SaveMessege($"Вы получили {equippable.Name}");
            }
        }

        public void ApplyGold(int amount)
        {
            Gold.Value += amount;
            Informer.SaveMessege($"Вы получили {amount} золота");
        }

        protected void DeleteItem(Item _item)
        {
            _item.Count--;
            if (_item.Count <= 0)
                itemInventory.Remove(_item);
        }

        public void SetEffect(Effect effect)
        {
            State = effect;
            Informer.SaveMessege($"Наложен эффект: {effect}");
        }

        public void DeleteEffect()
        {
            if (State.GetType() != typeof(None))
            {
                Informer.SaveMessege($"Вы избавились от: {State}");
                State = new None();
            }
        }
    }
}