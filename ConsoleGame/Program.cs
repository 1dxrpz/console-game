using ConsoleGame;

public class Program
{
	static string Dices = "⚀⚁⚂⚃⚄⚅🎲";
	static string[] borders = ["╭═", "──", "═╮", "│ ", " │", "╰═", "──", "═╯"];

	public static void Main(string[] args)
	{
		PlayerData Player = new();
		UI UI = new();
		Map Map = new();
		Objects Objects = new();

		bool inventory = false;

		Console.CursorVisible = false;
		Player.SetPosition(1, 1);
		UI.DrawWindow(8, 10, borders, UI.ViewportWidth + 1, 0);
		UI.DrawWindow(UI.ViewportWidth, UI.ViewportHeight, borders);
		UI.Redraw(Player);
		Map.Redraw(Player);

		Player.OnHealthEmpty(() =>
		{
			UI.DrawWindow(UI.ViewportWidth, UI.ViewportHeight, borders);
			Console.SetCursorPosition(UI.ViewportWidth - 5, (UI.ViewportHeight + 2) / 2 - 1);

			Console.Write("Game Over");
			Console.SetCursorPosition(UI.ViewportWidth - 11, (UI.ViewportHeight + 2) / 2);
			Console.Write("(R) to start new game");
		});

		while (true)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.T:
					Player.AddHealth(-13);
					UI.Redraw(Player);
					break;
				case ConsoleKey.E:
					if (inventory)
					{
						UI.ClearWindow(8, 15, UI.ViewportWidth + 1, 10);
						inventory = false;
					}
					else
					{
						UI.DrawWindow(8, 15, borders, UI.ViewportWidth + 1, 10, "Inventory");
						inventory = true;
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