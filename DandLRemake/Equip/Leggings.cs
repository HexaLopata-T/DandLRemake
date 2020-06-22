using System;

namespace DandLRemake.Equip
{
    public class Leggings : Equippable
    {
        public Leggings(int dropChance) : base(dropChance)
        {
            Name = "Штаны";
            Info = "Стандартные рабочие штаны";
        }

        public override void OnAnyTurn(Player player, Enemy enemy, bool repeat) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }
    }

    public sealed class IronLeggings : Leggings
    {
        public IronLeggings(int dropChance) : base(dropChance)
        {
            Armor = 5;
            Name = "Железные поножи";
            Info = "Добротные поножи, дают немного брони";
            Price = 20;
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
