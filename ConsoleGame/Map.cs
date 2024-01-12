using Pastel;
using System.Drawing;
using System.Text.RegularExpressions;

namespace ConsoleGame;

public class Map
{
	public int MapWidth = 16;
	public int MapHeight = 16;

	static Dictionary<int, Color> ColorMap = new()
	{
		{ 0, Color.FromArgb(30, 100, 30) },
		{ 1, Color.FromArgb(204, 177, 102) },
		{ 2, Color.FromArgb(30, 30, 30) },
		{ 3, Color.FromArgb(30, 30, 30) },
		{ 4, Color.FromArgb(30, 30, 30) },
		{ 5, Color.FromArgb(30, 30, 30) },
		{ 6, Color.FromArgb(30, 30, 30) },
		{ 7, Color.FromArgb(30, 30, 30) },
		{ 8, Color.FromArgb(30, 30, 30) },
		{ 9, Color.FromArgb(30, 30, 30) },
	};

	static string[] groundMap = [
		"0000000000000000",
		"0100000000000000",
		"0110000000000000",
		"0100000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
		"0000000000000000",
	];
	static string[] map = [
		"TTTTTTTTTTTTTTTT",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"T              T",
		"TTTTTTTTTTTTTTTT",
	];

	public Map()
	{
	}

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
