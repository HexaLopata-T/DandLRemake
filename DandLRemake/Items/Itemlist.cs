namespace DandLRemake.Items
{
    public sealed class Shuriken : Item
    {
        public Shuriken(int i) : base(i)
        {
            Name = "Сюрикен";
        }

        public override void Use(Player player, Enemy enemy)
        {
            Informer.SaveMessege("Сюрикен брошен");
            enemy.ApplyDamage(player.BaseDamage * 3 * player.Dexterity, DamageType.Normal);
        }
    }

    public sealed class Potion : Item
    {
        public Potion(int i) : base(i)
        {
            Name = "Зелье лечения";
        }

        public override void Use(Player player, Enemy enemy)
        {
            Informer.SaveMessege("Вы выпили зелье лечения");
            player.Heal(player.HP.Value / 3);
        }
    }
}
