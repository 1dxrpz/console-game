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
		public virtual int Level { get; set; }
		public virtual float ManaStat { get; set; }
		public virtual float HealthStat { get; set; }
		public virtual float StrengthStat { get; set; }
		public virtual float ArmorStat { get; set; }
		public virtual float RegenMultiplierStat { get; set; }
		public virtual ItemType ItemType { get; set; }

	}
	public class Sword : Item
	{
		public override string Name => "Default sword";
		public override string Description => "Old rusty sword";
	}
}
