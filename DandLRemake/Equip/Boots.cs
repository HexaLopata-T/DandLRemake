using System;

namespace DandLRemake.Equip
{
    public class Boots : Equippable
    {
        public Boots(int dropChance) : base(dropChance)
        {
            name = "Рваные ботинки";
            info = "Время их не пощадило";
        }

        public override void OnAnyTurn(Player player, Enemy enemy) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }
    }

    public sealed class IronBoots : Boots
    {
        public IronBoots(int dropChance) : base(dropChance)
        {
            Armor = 3;
            name = "Железные ботинки";
            info = "Добротные ботинки, дают немного брони";
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
