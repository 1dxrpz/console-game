using Pastel;
using System.Drawing;
using System.Reflection.Emit;

namespace ConsoleGame;
public class Inventory : GameEvents
{
	public static Inventory Source { get; } = SingletonUtil<Inventory>.Instance;

	UI UI { get; set; } = UI.Source;

	public List<string> inventory = new();
	public bool inventoryOpen = true;
	public ItemType currentCategory;
	public bool selectingCategory = true;
	public int currentItem = 0;
	
	static string[] inventoryCategories = [
		"⚔️ Weapons",
		"🎯 Armor",
		"👔 Clothes",
		"🍎 Food",
		"🧪 Heal/Mana",
		"💫 Misc",
	];

	public override void Init()
	{
		inventory = inventoryCategories.ToList();
		UI.ClearWindow(15, 15, 1, 10);
		DrawInventory();
		UI.Redraw(Player);
		UI.DrawWindow(15, 10, BaseData.borders, 1, 0);
		inventoryOpen = true;
	}

	public override void OnKeyPressed(ConsoleKey key)
	{
		switch (key)
		{
			case ConsoleKey.Enter:
				if (inventoryOpen)
				{
					if (selectingCategory)
					{
						selectingCategory = false;
						currentCategory = (ItemType)currentItem;
						inventory = Player.Inventory[(ItemType)currentItem].Select(v => v.Name).ToList();
						currentItem = 0;
						DrawInventory();
					}
				}
				break;
			case ConsoleKey.UpArrow:
				if (inventoryOpen)
				{
					currentItem = currentItem == 0 ? inventory.Count - 1 : currentItem - 1;
					DrawInventory();
				}
				break;
			case ConsoleKey.DownArrow:
				if (inventoryOpen)
				{
					currentItem = currentItem == inventory.Count - 1 ? 0 : currentItem + 1;
					DrawInventory();
				}
				break;
			case ConsoleKey.E:
				currentItem = 0;
				selectingCategory = true;
				inventory = inventoryCategories.ToList();
				if (inventoryOpen)
				{
					UI.ClearWindow(15, 15, 1, 10);
					DrawInventory();
					UI.Redraw(Player);
					UI.DrawWindow(15, 10, BaseData.borders, 1, 0);
					inventoryOpen = true;
				}
				else
				{
					DrawInventory();
					UI.Redraw(Player);
				}
				break;
		}
	}

	public void DrawStats()
	{
		UI.DrawWindow(15, 10, BaseData.borders, 1, 0);
		var inv = Player.Inventory[currentCategory];
		if (inv.Count == 0)
		{
			Console.SetCursorPosition(14, 15);
			Console.Write("Empty!".Pastel(Color.FromArgb(60, 60, 60)));
			Console.SetCursorPosition(10, 16);
			Console.Write("(e) to select".Pastel(Color.FromArgb(60, 60, 60)));
			Console.SetCursorPosition(13, 17);
			Console.Write("category".Pastel(Color.FromArgb(60, 60, 60)));
			return;
		}
		var item = inv[currentItem];

		Console.SetCursorPosition(4, 1);
		Console.Write(item.Name.Pastel(Color.Gold));
		Console.SetCursorPosition(25, 1);
		Console.Write($"LV{item.Level}".PadLeft(5, ' ').Pastel(Color.Cyan));
		Console.SetCursorPosition(4, 2);
		Console.Write(item.Description.Pastel(Color.DarkGray));
		Console.SetCursorPosition(3, 3);
		Console.Write((new string('─', 28)).Pastel(Color.FromArgb(50, 50, 50)));

		List<(float value, string statName, Color color)> stats = [
			(item.ManaStat, "mana", Color.Cyan),
			(item.StrengthStat, "strength", Color.RebeccaPurple),
			(item.HealthStat, "health", Color.MediumVioletRed),
			(item.ArmorStat, "armor", Color.Coral),
			(item.RegenMultiplierStat, "regen", Color.LightGreen),
			(item.ManaRefillStat, "mana refill", Color.Cyan),
			(item.HealthRefillStat, "health refill", Color.LightGreen),
		];

		int i = 0;

		stats.Where(v => v.value > 0)
			.ToList()
			.ForEach(v =>
			{
				Console.SetCursorPosition(4, 4 + i);
				Console.Write($"+{v.value} {v.statName}".Pastel(v.color));
				i++;
			});
	}

	public void DrawInventory()
	{
		UI.DrawWindow(15, 15, BaseData.borders, 1, 10, "Inventory");
		for (int i = 0; i < inventory.Count; i++)
		{
			Console.SetCursorPosition(3, 11 + i);

			Console.Write($" {inventory[i].PadRight(27, ' ')}"
				.PastelBg(i == currentItem ? Color.White : Color.DarkOrchid)
				.Pastel(i != currentItem ? Color.White : Color.Black)
			);
		}
		inventoryOpen = true;
		if (!selectingCategory)
		{
			DrawStats();
		}
	}
}
