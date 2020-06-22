namespace DandLRemake.Equip
{
    public class Weapon : Equippable
    {
        public DamageType type { get; protected set; } = DamageType.Normal;

        public Weapon(int dropChance) : base(dropChance)
        {
            Name = "Праща";
            Info = "Оружие на скорую руку, малоэффективно";
        }

        public override void OnAnyTurn(Player player, Enemy enemy, bool repeat) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player){ }
    }
}
