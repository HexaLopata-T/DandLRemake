namespace DandLRemake.Magic
{
    public abstract class Spell
    {
        private int level;
        protected int defaultDamage = 0;
        protected DamageType Type = DamageType.None;
        private int price;

        protected int Level { get { return level; } set { if (value > 0) level = value; else throw new System.Exception("Отрицательный уровень"); } }
        public int Price { get { return price; } protected set { if (value > 0) { price = value; } else throw new System.Exception("Отрицательная стоимость"); } }
        public string Name { get; protected set; }

        public Spell(int _level)
        {
            Level = _level;
        }

        public abstract bool Use(Enemy enemy, Player player);

        public abstract object Clone(int _level);

        public override string ToString()
        {
            return $"{Name} {Level}-го уровня";
        }
    }
}
