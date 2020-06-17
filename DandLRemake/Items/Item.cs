namespace DandLRemake.Items
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public int DropChance { get; protected set; }
        public int Price { get; protected set; } = 0;
        public int Count { get; set; } = 1;

        public Item(int _dropChance)
        {
            DropChance = _dropChance;
        }

        public abstract void Use(Player player, Enemy enemy);

        public override string ToString()
        {
            return $"{Name}: {Count}";
        }
    }
}
