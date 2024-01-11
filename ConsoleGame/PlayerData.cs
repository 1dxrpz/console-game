namespace ConsoleGame;
public class PlayerData
{
	public string Name { get; set; }
	public bool IsDead { get; private set; } = false;
	public (int x, int y) Position { get; private set; }
	public (float current, float max) Health { get; private set; } = (100, 100);
	public (float current, float max) Mana { get; private set; } = (100, 100);
	public (int health, int mana) Regen { get; private set; } = (12, 10);
	public float Strength { get; set; }
	public int Armor { get; set; }
	public int RegenMultiplier { get; set; }
	public int Level { get; set; }
	public int Money { get; set; }
	public int Coins { get; set; }
	public (int current, int max) Experience { get; set; }

	public Dictionary<ItemType, List<Item>> Inventory { get; set; } = new()
	{
		{ ItemType.Weapon, new() },
		{ ItemType.Armor, new() },
		{ ItemType.Clothes, new() },
		{ ItemType.Food	, new() },
		{ ItemType.HealMana, new() },
		{ ItemType.Misc, new() },
	};
	
	public void SetHealth(float health)
	{
		Health = (health, Health.max);
		if (health == 0)
		{
			IsDead = true;
			NoHealthEvent?.Invoke();
		}
	}
	public void AddHealth(float value) => SetHealth(Math.Clamp(Health.current + value, 0, Health.max));
	public void SetMaxHealth(float max) => Health = (Health.current, max);
	public void SetMana(float mana)
	{
		Mana = (mana, Mana.max);
		if (mana == 0) NoManaEvent?.Invoke();
	}
	public void AddMana(float value) => SetMana(Math.Clamp(Mana.current + value, 0, Mana.max));
	public void SetMaxMana(float max) => Mana = (Mana.current, max);

	public void SetPosition(int x, int y) => Position = (x, y);
	public void Move(int x, int y) => SetPosition(Position.x + x, Position.y + y);

	Action NoManaEvent = null!;
	Action NoHealthEvent = null!;

	public void OnHealthEmpty(Action action) => NoHealthEvent = action;
	public void OnManaEmpty(Action action) => NoManaEvent = action;
}
