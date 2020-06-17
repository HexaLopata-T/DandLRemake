using DandLRemake.Equip;
using DandLRemake.Items;
using System.Collections.Generic;

namespace DandLRemake
{
    static class EventList
    {
        public static Event[] events = new Event[]
        {
            new Chest(),
            new Shop(),
        };

        public static Event ReturnEvent(int id)
        {
            var eventForReturn = (Event)events[id].Clone();
            return eventForReturn;
        }
    }

    public sealed class Chest : Event
    {
        public Chest()
        {
            image = new string[] { "Вы встретили сундук", };

            choise = new string[7]
            {
                "1.Забрать",
                "2.Пройти мимо",
                "",
                "",
                "",
                "",
                "",
            };
        }

        public override bool Action(char number, ref Player player)
        {
            switch (number)
            {
                case '1':
                    if (random.Next(0,2) > 0)
                    {
                        var gold = random.Next(10, 101);
                        player.ApplyGold(gold);
                    }
                    else
                    {
                        Informer.SaveMessege("Вы попали в огненную ловушку");
                        player.ApplyDamage(random.Next(100, 301), DamageType.Fire);
                    }
                    return false;
                case '2':
                    Informer.SaveMessege("Вы проходите мимо");
                    return false;
                default: return true;
            }
        }

        public override object Clone()
        {
            return new Chest();
        }
    }

    public sealed class Shop : Event
    {
        private List<Equippable> equipList;
        private List<Item> itemList;

        private Equippable[] equipShowcase = new Equippable[3];
        private Item[] itemShowcase = new Item[2];

        public Shop()
        {
            equipList = new List<Equippable>();
            itemList = new List<Item>();
            SetEquipForSale();
            SetItemsForSale();

            image = new string[] { "Вы наткнулись на магазин", };

            equipShowcase[0] = equipList[random.Next(0, equipList.Count)];
            equipShowcase[1] = equipList[random.Next(0, equipList.Count)];
            equipShowcase[2] = equipList[random.Next(0, equipList.Count)];

            itemShowcase[0] = itemList[random.Next(0, itemList.Count)];
            itemShowcase[1] = itemList[random.Next(0, itemList.Count)];

            choise = new string[7]
            {
                $"1.{equipShowcase[0].Name}: {equipShowcase[0].Price} золота",
                $"2.{equipShowcase[1].Name}: {equipShowcase[1].Price} золота",
                $"3.{equipShowcase[2].Name}: {equipShowcase[2].Price} золота",
                $"4.{itemShowcase[0].Name}: {itemShowcase[0].Price} золота",
                $"5.{itemShowcase[1].Name}: {itemShowcase[1].Price} золота",
                "6.Уйти",
                "",
            };
        }

        private void SetItemsForSale()
        {
            itemList.Add(new Potion(0));
            itemList.Add(new Shuriken(0));
        }

        private void SetEquipForSale()
        {
            equipList.Add(new IronHelmet(0));
            equipList.Add(new IronChestplate(0));
            equipList.Add(new IronLeggings(0));
            equipList.Add(new IronBoots(0));
        }

        public override bool Action(char number, ref Player player)
        {
            switch(number)
            {
                case '1':
                    BuyingEquip(player, 0);
                    return true;
                case '2':
                    BuyingEquip(player, 1);
                    return true;
                case '3':
                    BuyingEquip(player, 2);
                    return true;
                case '4':
                    BuyingItem(player, 0);
                    return true;
                case '5':
                    BuyingItem(player, 1);
                    return true;
                case '6':
                    Informer.SaveMessege("Вы покидаете магазин");
                    return false;
                default:
                    return true;
            }
        }

        private bool BuyingEquip(Player player, int number)
        {
            if (player.Gold.Value >= equipShowcase[number].Price)
            {
                player.Gold.Value -= equipShowcase[number].Price;
                player.ApplyEquip(equipShowcase[number]);
                return true;
            }
            else
            {
                Informer.SaveMessege("Не хватает денег");
                return true;
            }
        }

        private bool BuyingItem(Player player, int number)
        {
            if (player.Gold.Value >= itemShowcase[number].Price)
            {
                player.Gold.Value -= itemShowcase[number].Price;
                player.ApplyItem(itemShowcase[number]);
                return true;
            }
            else
            {
                Informer.SaveMessege("Не хватает денег");
                return true;
            }
        }

        public override object Clone()
        {
            return new Shop();
        }
    }
}
