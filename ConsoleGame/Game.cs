using Pastel;
using System.Drawing;

namespace ConsoleGame;

public class GameEvents
{
	internal static Action<ConsoleKey> OnKeyPressedEvent;
	internal static Action OnInitEvent;

	public static PlayerData Player;

	public GameEvents()
	{
		OnKeyPressedEvent += OnKeyPressed;
		OnInitEvent += Init;
	}

	public virtual void OnKeyPressed(ConsoleKey key) { }
	public virtual void Init() { }
}

public class Game
{
	PlayerData Player = new();
	UI UI { get; set; } = UI.Source;
	Map Map { get; set; } = Map.Source;
	Inventory Inventory { get; set; } = Inventory.Source;

	public Game()
	{
		GameEvents.Player = Player;

		Console.Clear();
		Console.CursorVisible = false;
		Player.SetPosition(1, 1);

		UI.DrawWindow(15, 10, BaseData.borders, 1, 0);
		UI.DrawWindow(UI.ViewportWidth, UI.ViewportHeight, BaseData.borders, UI.MainWindowOffset / 2);
		UI.Redraw(Player);
		Map.Redraw(Player);

		Player.OnHealthEmpty(() =>
		{
			UI.DrawWindow(UI.ViewportWidth, UI.ViewportHeight, BaseData.borders, UI.MainWindowOffset / 2);
			Console.SetCursorPosition(UI.ViewportWidth - 5 + UI.MainWindowOffset, (UI.ViewportHeight + 2) / 2 - 1);

			Console.Write("Game Over");
			Console.SetCursorPosition(UI.ViewportWidth - 11 + UI.MainWindowOffset, (UI.ViewportHeight + 2) / 2);
			Console.Write("(R) to start new game");
		});

		Player.AddItem(new Sword());
		Player.AddItem(new Dagger());
		Player.AddItem(new DebugSword());
		Player.AddItem(new EnchantedApple());

		GameEvents.OnInitEvent?.Invoke();
		
		while (true)
		{
			var key = Console.ReadKey(true).Key;
			GameEvents.OnKeyPressedEvent?.Invoke(key);

			switch (key)
			{
				case ConsoleKey.R:
					Random random = new Random();
					UI.Dialogs.Add("# Name Name - (19)".Pastel(Color.Plum));
					UI.Dialogs.Add(" Hello there!".Pastel(Color.Wheat));
					UI.Redraw(Player);
					break;
				case ConsoleKey.T:
					Player.AddHealth(-13);
					UI.Redraw(Player);
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
