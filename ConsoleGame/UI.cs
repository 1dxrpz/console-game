using Pastel;
using System.Drawing;
namespace ConsoleGame;
public static class AnsiColors
{
	public static string DARK_BLACK = "\u001B[30m";
	public static string DARK_RED = "\u001B[31m";
	public static string DARK_GREEN = "\u001B[32m";
	public static string DARK_YELLOW = "\u001B[33m";
	public static string DARK_BLUE = "\u001B[34m";
	public static string DARK_PURPLE = "\u001B[35m";
	public static string DARK_CYAN = "\u001B[36m";
	public static string WHITE = "\u001B[37m";
	public static string BLACK = "\u001B[90m";
	public static string RED = "\u001B[91m";
	public static string GREEN = "\u001B[92m";
	public static string YELLOW = "\u001B[93m";
	public static string BLUE = "\u001B[94m";
	public static string PURPLE = "\u001B[95m";
	public static string CYAN = "\u001B[96m";
	public static string GRAY = "\u001B[90m";
	public static string BOLD = "\u001B[1m";
	public static string UNBOLD = "\u001B[21m";
	public static string UNDERLINE = "\u001B[4m";
	public static string STOP_UNDERLINE = "\u001B[24m";
	public static string BLINK = "\u001B[5m";
	public static string RESET = "\u001B[0m";
	public static string CLEARSCREEN = "\u033b[H\u033b[2J";
}

public class UI
{
	private UI() { }
	public static UI Source { get; } = SingletonUtil<UI>.Instance;

	public int MainWindowOffset = 34;
	public int ViewportWidth = 25;
	public int ViewportHeight = 25;
	public int ViewDistance = 6;

	int percent(float value, float max, int m) => (int)(value / (float)max * m);
	
	public void Redraw(PlayerData Player)
	{
		Console.ResetColor();

		var health = percent(Player.Health.current, Player.Health.max, 30);
		var mana = percent(Player.Mana.current, Player.Mana.max, 30);

		Console.SetCursorPosition(10 + MainWindowOffset, ViewportHeight + 2);
		Console.WriteLine($"" +
			$"{new string('▃', mana).Pastel(Color.FromArgb(92, 177, 181))}" +
			$"{new string('▃', 30 - mana).Pastel(Color.FromArgb(40, 40, 40))}"
		);

		Console.SetCursorPosition(10 + MainWindowOffset, ViewportHeight + 3);
		Console.WriteLine(
			$"{new string('█', health).Pastel(Color.FromArgb(176, 59, 66))}" +
			$"{new string('█', 30 - health).Pastel(Color.FromArgb(60, 60, 60))}"
		);

		Console.SetCursorPosition(MainWindowOffset, ViewportHeight + 2);
		Console.WriteLine($"\x1b[40m{AnsiColors.WHITE}{Player.Mana.current,3:0#}/{Player.Mana.max}");

		Console.SetCursorPosition(MainWindowOffset, ViewportHeight + 3);
		Console.WriteLine($"\x1b[40m{AnsiColors.WHITE}{Player.Health.current,3:0#}/{Player.Health.max}");

		Console.SetCursorPosition(45 + MainWindowOffset, ViewportHeight + 2);
		Console.WriteLine($"\x1b[40m{AnsiColors.WHITE}+{Player.Regen.mana:0.0#}");
		Console.SetCursorPosition(45 + MainWindowOffset, ViewportHeight + 3);
		Console.WriteLine($"\x1b[40m{AnsiColors.WHITE}+{Player.Regen.health:0.0#}");

		Console.ResetColor();

		Console.SetCursorPosition(MainWindowOffset, ViewportHeight);
		Console.Write($"\U0001f7e1 {Player.Money}");
		Console.SetCursorPosition(10 + MainWindowOffset, ViewportHeight);
		Console.Write($"⚪ {Player.Coins}");

		Console.SetCursorPosition(MainWindowOffset, ViewportHeight + 1);
		Console.Write($"⚔️ {Player.Strength:0.0#}");
		Console.SetCursorPosition(10 + MainWindowOffset, ViewportHeight + 1);
		Console.WriteLine($"🛡️ {Player.Armor:0.0#}");
		Console.SetCursorPosition(20 + MainWindowOffset, ViewportHeight + 1);
		Console.WriteLine($"🤍 {Player.RegenMultiplier:0.0#}");
		Console.SetCursorPosition(30 + MainWindowOffset, ViewportHeight + 1);
		Console.WriteLine($"LVL {Player.Level}");
		Console.SetCursorPosition(40 + MainWindowOffset, ViewportHeight + 1);
		Console.WriteLine($"✨{Player.Experience.current}/{Player.Experience.max}");


		Console.SetCursorPosition(ViewportWidth + 60, 0);
		Console.Write($"╭═────────────═ ⌘ ═────────────═╮");
		Console.SetCursorPosition(ViewportWidth + 60, ViewportHeight - 1);
		Console.Write($"╰═────────────═ ⌘ ═────────────═╯");
	}

	public void ClearWindow(int width, int height, int posX = 0, int posY = 0) =>
	DrawWindow(width, height, ["  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "], posX, posY);
	public void DrawWindow(int width, int height, string[] borders, int posX = 0, int posY = 0, string label = "")
	{
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				var pos = (x, y);
				
				Console.SetCursorPosition((x + posX) * 2, y + posY);
				switch (pos)
				{
					default: Console.Write("  ");
						break;
					case (0, 0):
						Console.Write(borders[0]);
						break;
					case (int, int) v when v.x == 0 && v.y == height - 1:
						Console.Write(borders[5]);
						break;
					case (int, int) v when v.x == width - 1 && v.y == 0:
						Console.Write(borders[2]);
						break;
					case (int, int) v when v.x == width - 1 && v.y == height - 1:
						Console.Write(borders[7]);
						break;
					case (int, int) v when v.y == 0:
						Console.Write(borders[1]);
						break;
					case (int, int) v when v.y == height - 1:
						Console.Write(borders[6]);
						break;
					case (int, int) v when v.x == 0:
						Console.Write(borders[3]);
						break;
					case (int, int) v when v.x == width - 1:
						Console.Write(borders[4]);
						break;
				}
				Console.ResetColor();

			}
		}
		if (label.Length != 0)
		{
			Console.SetCursorPosition(posX * 2 + 2, posY);
			Console.Write($" {label} ");
		}
	}
}
