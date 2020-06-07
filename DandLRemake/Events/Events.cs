﻿using System;
using DandLRemake.PropertiesAppointee;

namespace DandLRemake.Events
{
    static class EventList
    {
        public static Event[] events = new Event[]
        {
            new Chest(),
        };

        public static Event ReturnEvent(int id)
        {
            var eventForReturn = events[id];
            events[id] = (Event)events[id].Clone();
            return eventForReturn;
        }
    }
    public class Chest : Event
    {
        public Chest()
        {
            image = new string[] { "Вы встретили сундук", };

            choise = new string[]
            {
                "1.Забрать",
                "2.Пройти мимо",
                "",
                "",
                ""
            };
        }

        public override bool Action(char number, PropertyEditor stats, ref Player player)
        {
            {
                switch (number)
                {
                    case '1':
                        if (random.Next(0,2) > 0)
                        {
                            var gold = stats.GetValue(3) + random.Next(10, 101);

                            Informer.SaveMessege($"Вы получаете {gold} золота");

                            stats.SetValue(3, gold);
                        }
                        else
                        {
                            Informer.SaveMessege("Вы попали в огненную ловушку");
                            player.ApplyDamage(20, DamageType.Fire);
                        }
                        return false;
                    case '2':
                        Informer.SaveMessege("Вы проходите мимо");
                        return false;
                    default: return true;
                }

            }
        }

        public override object Clone()
        {
            return new Chest();
        }
    }
}
