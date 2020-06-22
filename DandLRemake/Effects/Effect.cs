namespace DandLRemake.Effects
{
    public abstract class Effect
    {
        public string Name { get; protected set; } = "???";

        public abstract void OnAttack(Player player, Enemy enemy, int damage);
        public abstract void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy);
        public abstract void OnDamage(int damageAmount, DamageType damageType, Player player);
        public abstract void OnAnyTurn(Player player, Enemy enemy, bool repeat);

        public override string ToString()
        {
            return Name;
        }
    }
}