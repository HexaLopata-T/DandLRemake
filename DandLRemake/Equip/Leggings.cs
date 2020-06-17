using System;

namespace DandLRemake.Equip
{
    public class Leggings : Equippable
    {
        public Leggings(int dropChance) : base(dropChance)
        {
            name = "Штаны";
            info = "Стандартные рабочие штаны";
        }

        public override void OnAnyTurn(Player player, Enemy enemy) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }
    }

    public sealed class IronLeggings : Leggings
    {
        public IronLeggings(int dropChance) : base(dropChance)
        {
            Armor = 4;
            name = "Железные поножи";
            info = "Добротные поножи, дают немного брони";
        }

        public override void OnEquip(Player player)
        {
            player.Armor.Value += Armor;
        }

        public override void OnUnequip(Player player)
        {
            player.Armor.Value -= Armor;
        }
    }
}
