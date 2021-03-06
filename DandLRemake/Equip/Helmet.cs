using System;
using System.Linq.Expressions;

namespace DandLRemake.Equip
{
    public class Helmet : Equippable
    {
        public Helmet(int dropChance) : base(dropChance)
        {
            Name = "Соломенная шляпа";
            Info = "Шляпа от солнца, сделана в свободное время";
        }

        public override void OnAnyTurn(Player player, Enemy enemy, bool repeat) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }
    }

    public sealed class IronHelmet : Helmet
    {
        public IronHelmet(int dropChance) : base(dropChance)
        {
            Armor = 4;
            Name = "Железный шлем";
            Info = "Добротный шлем, дает немного брони";
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
