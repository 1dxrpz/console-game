namespace ConsoleGame
{
	public enum ItemType
	{
		Weapon, Armor, Clothes, Food, HealMana, Misc
	}
	public abstract class Item
	{
		public virtual string Name { get; }
		public virtual string Description { get; }
		public virtual int Level { get; }
		public virtual float ManaStat { get; }
		public virtual float ManaRefillStat { get; }
		public virtual float HealthStat { get; }
		public virtual float HealthRefillStat { get; }
		public virtual float StrengthStat { get; }
		public virtual float ArmorStat { get; }
		public virtual float RegenMultiplierStat { get; }
		public virtual ItemType ItemType { get; }

	}
	public class DebugSword : Item
	{
		public override ItemType ItemType => ItemType.Weapon;
		public override string Name => "Debug sword";
		public override string Description => "Old rusty sword";
		public override int Level => 999;
		public override float StrengthStat => 999;
		public override float HealthStat => 999;
		public override float ArmorStat => 999;
		public override float ManaStat => 999;
		public override float RegenMultiplierStat => 999;
	}
	public class Dagger : Item
	{
		public override ItemType ItemType => ItemType.Weapon;
		public override string Name => "Silver dagger";
		public override string Description => "Dagger forged from silver";
		public override int Level => 1;
		public override float StrengthStat => 1;

	}
	public class Sword : Item
	{
		public override ItemType ItemType => ItemType.Weapon;
		public override string Name => "Default sword";
		public override string Description => "Old rusty sword";
		public override int Level => 1;
		public override float HealthStat => 1;
		public override float StrengthStat => 1;
	}
	public class EnchantedApple : Item
	{
		public override ItemType ItemType => ItemType.Food;
		public override string Name => "Enchanted apple";
		public override string Description => "Shining!";
		public override int Level => 1;
		public override float ManaRefillStat => 30;
		public override float HealthRefillStat => 50;
	}
}
