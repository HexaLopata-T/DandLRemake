namespace DandLRemake.Equip
{
    public class Weapon : Equippable
    {
        public DamageType type { get; protected set; } = DamageType.Normal;

        public Weapon(int dropChance) : base(dropChance)
        {
            name = "Праща";
            info = "Оружие на скорую руку, малоэффективно";
        }

        public override void OnAnyTurn(Player player, Enemy enemy) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }
    }
}
