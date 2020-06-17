using System;

namespace DandLRemake.Equip
{
    public class Chestplate : Equippable
    {
        public Chestplate(int dropChance): base(dropChance)
        {
            Name = "Старая рубаха";
            Info = "Уже давно покрылась пятнами";
        }
        public override void OnAnyTurn(Player player, Enemy enemy) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }
    }

    public sealed class IronChestplate : Chestplate
    {
        public IronChestplate(int dropChance) : base(dropChance)
        {
            Armor = 6;
            Name = "Железный нагрудник";
            Info = "Добротный нагрудник, дает немного брони";
            Price = 25;
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
