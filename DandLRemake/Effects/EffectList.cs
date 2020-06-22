

using System;

namespace DandLRemake.Effects
{
    public sealed class None : Effect
    {
        public override void OnAnyTurn(Player player, Enemy enemy, bool repeat) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player) { }
    }

    public sealed class Burn : Effect
    {
        int counter;

        public Burn()
        {
            Name = "Ожог";
            counter = 6;
        }

        public override void OnAnyTurn(Player player, Enemy enemy, bool repeat)
        {
            if(!repeat)
            {
                Informer.SaveMessege("Вы страдаете от ожога");
                player.ApplyDamage(player.HP.MaxValue / 13, DamageType.Fire);
                counter--;
                if (counter == 0)
                    player.DeleteEffect();
            }
        }

        public override void OnAttack(Player player, Enemy enemy, int damage)
        {

        }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy)
        {

        }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player)
        {

        }
    }

    public sealed class Obsession : Effect
    {
        public Obsession()
        {
            Name = "Одержимость";
        }

        public override void OnAnyTurn(Player player, Enemy enemy, bool repeat) { }

        public override void OnAttack(Player player, Enemy enemy, int damage)
        {
            if (new Random().Next(1, 101) < 15)
            {
                Informer.SaveMessege("Будучи одержимым властью дьявола вы раните себя своими собственными руками");
                player.Morality.Value -= 30;
                player.ApplyDamage(damage, DamageType.Normal);
            }
        }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player) { }
    }
}
