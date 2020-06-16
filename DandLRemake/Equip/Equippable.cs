using System;

namespace DandLRemake.Equip
{
    public abstract class Equippable
    {
        protected string name = "None";
        protected string info = "";
        public int DropChance { get; set; } = 100;

        public override string ToString()
        {
            return $"{name} : {info}";
        }
    }
}
