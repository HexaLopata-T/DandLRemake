using System;

namespace DandLRemake.Equip
{
    public abstract class Equippable
    {
        public int Armor { get; protected set; } = 0;

        protected string name = "???";
        protected string info = "???";
        public int DropChance { get; protected set; }

        public Equippable(int dropChance)
        {
            DropChance = dropChance;
        }

        public override string ToString()
        {
            return $"{name} : {info}";
        }

        public abstract void OnEquip(Player player);
        public abstract void OnAttack(Player player, Enemy enemy, int damage);
        public abstract void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy);
        public abstract void OnAnyTurn(Player player, Enemy enemy);
        public abstract void OnUnequip(Player player);
    }
}
