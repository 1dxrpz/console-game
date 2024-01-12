using ConsoleGame;
using Pastel;
using System.Drawing;
using System.Numerics;

public class Program
{
	static string Dices = "⚀⚁⚂⚃⚄⚅🎲";
	static string[] borders = ["╭═", "──", "═╮", "│ ", " │", "╰═", "──", "═╯"];

	static PlayerData Player = new();
	static UI UI = new();
	static Map Map = new();
	static Objects Objects = new();

	static List<string> inventory = new();
	static bool inventoryOpen = false;
	static bool selectingCategory = false;
	static int currentItem = 0;

	static string[] inventoryCategories = [
		"⚔️ Weapons",
		"🎯 Armor",
		"👔 Clothes",
		"🍎 Food",
		"🧪 Heal/Mana",
		"💫 Misc",
	];

	static void DrawInventory()
	{
		UI.DrawWindow(15, 15, borders, 1, 10, "Inventory");
		for (int i = 0; i < inventory.Count; i++)
		{
			Console.SetCursorPosition(3, 11 + i);
			
			Console.Write($" {inventory[i].PadRight(27, ' ')}"
				.PastelBg(i == currentItem ? Color.White : Color.DarkOrchid)
				.Pastel(i != currentItem ? Color.White : Color.Black)
			);
		}
		inventoryOpen = true;
	}

	public static void Main(string[] args)
	{
		Console.Clear();
		Console.CursorVisible = false;
		Player.SetPosition(1, 1);
		UI.DrawWindow(15, 10, borders, 1, 0);
		UI.DrawWindow(UI.ViewportWidth, UI.ViewportHeight, borders, UI.MainWindowOffset / 2);
		UI.Redraw(Player);
		Map.Redraw(Player);

		Player.OnHealthEmpty(() =>
		{
			UI.DrawWindow(UI.ViewportWidth, UI.ViewportHeight, borders, UI.MainWindowOffset / 2);
			Console.SetCursorPosition(UI.ViewportWidth - 5 + UI.MainWindowOffset, (UI.ViewportHeight + 2) / 2 - 1);

			Console.Write("Game Over");
			Console.SetCursorPosition(UI.ViewportWidth - 11 + UI.MainWindowOffset, (UI.ViewportHeight + 2) / 2);
			Console.Write("(R) to start new game");
		});

		Player.Inventory[ItemType.Weapon].Add(new Sword());
		Player.Inventory[ItemType.Weapon].Add(new Sword());
		Player.Inventory[ItemType.Weapon].Add(new Sword());
		Player.Inventory[ItemType.Weapon].Add(new Sword());

		while (true)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.T:
					Player.AddHealth(-13);
					UI.Redraw(Player);
					break;
				case ConsoleKey.Enter:
					if (inventoryOpen)
					{
						if (!selectingCategory)
						{
							selectingCategory = true;
							inventory = Player.Inventory[(ItemType)currentItem].Select(v => v.Name).ToList();
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
					selectingCategory = false;
					inventory = inventoryCategories.ToList();
					if (inventoryOpen)
					{
						UI.ClearWindow(15, 15, 1, 10);
						inventoryOpen = false;
					}
					else
					{
						DrawInventory();
						UI.Redraw(Player);
					}
					break;
				case ConsoleKey.W:
					if (!Objects.IsColideable(Map.GetObjectKey(Player.Position.y - 1, Player.Position.x)))
					{
						Player.Move(0, -1);
						Map.Redraw(Player);
					}
					break;
				case ConsoleKey.A:
					if (!Objects.IsColideable(Map.GetObjectKey(Player.Position.y, Player.Position.x - 1)))
					{
						Player.Move(-1, 0);
						Map.Redraw(Player);
					}
					break;
				case ConsoleKey.S:
					if (!Objects.IsColideable(Map.GetObjectKey(Player.Position.y + 1, Player.Position.x)))
					{
						Player.Move(0, 1);
						Map.Redraw(Player);
					}
					break;
				case ConsoleKey.D:
					if (!Objects.IsColideable(Map.GetObjectKey(Player.Position.y, Player.Position.x + 1)))
					{
						Player.Move(1, 0);
						Map.Redraw(Player);
					}
					break;
			}
		}
	}
}