using System;

namespace DandLRemake.Equip
{
    public class Ring : Equippable
    {
        public Ring(int dropChance) : base(dropChance)
        {
            Name = "Костяное кольцо";
            Info = "Семейная реликвия";
        }

        public override void OnAnyTurn(Player player, Enemy enemy) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }
    }
}
