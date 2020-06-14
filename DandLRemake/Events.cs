using DandLRemake.PropertiesAppointee;

namespace DandLRemake
{
    static class EventList
    {
        public static Event[] events = new Event[]
        {
            new Chest(),
        };

        public static Event ReturnEvent(int id)
        {
            var eventForReturn = (Event)events[id].Clone();
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

        public override bool Action(char number, ref Player player)
        {
            {
                switch (number)
                {
                    case '1':
                        if (random.Next(0,2) > 0)
                        {
                            var gold = player.Gold.Value + random.Next(10, 101);

                            Informer.SaveMessege($"Вы получаете {gold} золота");

                            player.Gold.Value = gold;
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
        }

        public override object Clone()
        {
            return new Chest();
        }
    }
}
