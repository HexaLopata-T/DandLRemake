using DandLRemake.Effects;

namespace DandLRemake.Equip
{
    public class Boots : Equippable
    {
        public Boots(int dropChance) : base(dropChance)
        {
            Name = "Рваные ботинки";
            Info = "Время их не пощадило";
        }

        public override void OnAnyTurn(Player player, Enemy enemy, bool repeat) { }

        public override void OnAttack(Player player, Enemy enemy, int damage) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player, Enemy enemy) { }

        public override void OnDamage(int damageAmount, DamageType damageType, Player player) { }

        public override void OnEquip(Player player) { }

        public override void OnUnequip(Player player) { }
    }

    public sealed class IronBoots : Boots
    {
        public IronBoots(int dropChance) : base(dropChance)
        {
            Armor = 3;
            Name = "Железные ботинки";
            Info = "Добротные ботинки, дают немного брони";
            Price = 15;
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

    public sealed class DevilsSlippers : Boots
    {
        public DevilsSlippers(int dropChance) : base(dropChance)
        {
            Armor = 20;
            Name = "Шлепки дьявола";
            Info = "Они выглядят стильно, но разве подарки дьявола когда-то приводили к хорошему?";
            Price = 99999;
        }

        public override void OnEquip(Player player)
        {
            Informer.SaveMessege("Ты не знаешь, на что подписался");
            player.Armor.Value += Armor;
        }

        public override void OnUnequip(Player player)
        {
            Informer.SaveMessege("Твои ошибки не оставят тебя просто так");
            player.ApplyDamage(player.HP.MaxValue / 3 + player.Armor.Value * player.Endurance, DamageType.Normal);
            player.SetEffect(new Obsession());
            player.Armor.Value -= Armor;
        }

        public override void OnAnyTurn(Player player, Enemy enemy, bool repeat)
        {
            if(!repeat)
            {
                Informer.SaveMessege("Шлепки впиваются вам в ноги с механической бесчеловечностью");
                player.ApplyDamage(66 * player.Level.Value + player.Armor.Value * player.Endurance, DamageType.Normal);
            }
        }
    }
}
