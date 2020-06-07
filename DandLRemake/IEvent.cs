using System;
using DandLRemake.PropertiesAppointee;

namespace DandLRemake
{
    public interface IEvent : IHaveEnvironment, ICloneable
    {   
        string[] Choise { get;}

        bool Action(char number, PropertyEditor stats, Player player);
    }
}
