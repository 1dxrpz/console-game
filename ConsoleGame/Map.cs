using Pastel;
using System.Drawing;

namespace ConsoleGame;

public class Map
{
	private Map() { }
	public static Map Source { get; } = SingletonUtil<Map>.Instance;

	UI UI { get; set; } = UI.Source;

	public int MapWidth = 16;
	public int MapHeight = 16;

	Dictionary<int, Color> ColorMap = new()
	{
		{ 0, Color.FromArgb(39, 156, 84) },
		{ 1, Color.FromArgb(204, 177, 102) },
		{ 2, Color.FromArgb(59, 124, 150) },
		{ 3, Color.FromArgb(30, 30, 30) },
		{ 4, Color.FromArgb(30, 30, 30) },
		{ 5, Color.FromArgb(30, 30, 30) },
		{ 6, Color.FromArgb(30, 30, 30) },
		{ 7, Color.FromArgb(30, 30, 30) },
		{ 8, Color.FromArgb(30, 30, 30) },
		{ 9, Color.FromArgb(30, 30, 30) },
	};

	string[] groundMap = [
		"2222222222222222",
		"2222222222222222",
		"2222222222222222",
		"2222222111222222",
		"2222211101122222",
		"2222111001122222",
		"2222110001222222",
		"2222211011222222",
		"2222221112222222",
		"2222222222222222",
		"2222222222222222",
		"2222222222222222",
		"2222222222222222",
		"2222222222222222",
		"2222222222222222",
		"2222222222222222",
	];
	string[] map = [
		"W W W W W W W W ",
		" W W W W W W W W",
		"W W W W W W W W ",
		" W W W W W W W W",
		"W W W W W W W W ",
		" W W W W W W W W",
		"W W W W W W W W ",
		" W W W W W W W W",
		"W W W W W W W W ",
		" W W W W W W W W",
		"W W W W W W W W ",
		" W W W W W W W W",
		"W W W W W W W W ",
		" W W W W W W W W",
		"W W W W W W W W ",
		" W W W W W W W W",
	];

	public char GetObjectKey(int x, int y)
	{
		return map[x][y];
	}

	public void Redraw(PlayerData Player)
	{
		for (int y = Player.Position.y - UI.ViewportHeight / 2 + 1, yV = 1; yV < UI.ViewportHeight - 1; yV++, y++)
		{
			for (int x = Player.Position.x - UI.ViewportWidth / 2 + 1, xV = 1; xV < UI.ViewportWidth - 1; xV++, x++)
			{
				Console.SetCursorPosition(xV * 2 + UI.MainWindowOffset, yV);
				
				if (y >= 0 && y < MapHeight && x >= 0 && x < MapWidth)
				{
					var ground = ColorMap[int.Parse($"{groundMap[y][x]}")];
					Console.Write(Objects.GetObject(map[y][x]).PastelBg(ground));
				}
				else
				{
					Console.Write("  ".PastelBg(Color.FromArgb(10, 10, 10)));
				}
			}
		}
		Console.SetCursorPosition(UI.ViewportWidth - 1 + UI.MainWindowOffset, (UI.ViewportHeight + 2) / 2 - 1);
		Console.Write("🐹".PastelBg(ColorMap[int.Parse($"{groundMap[Player.Position.y][Player.Position.x]}")]));
	}
}
