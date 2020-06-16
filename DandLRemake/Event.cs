using System;
using DandLRemake.PropertiesAppointee;

namespace DandLRemake
{
    public abstract class Event : IHaveEnvironment, ICloneable
    {
        protected string[] image;
        protected string[] choise;
        protected Random random = new Random();
        public string[] Choise { get { return choise; } }

        public string[] ReturnEnvironment()
        {
            return image;
        }

        public void SetChoiseToContinue()
        {
            choise = new string[7]
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

        public abstract bool Action(char number, ref Player player);

        public abstract object Clone();
    }
}
