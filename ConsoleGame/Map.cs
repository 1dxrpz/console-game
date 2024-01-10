namespace ConsoleGame;

public class Map
{
	public int MapWidth = 6;
	public int MapHeight = 6;

	static string[] map = [
		"TTTTTT",
		"T    T",
		"T    T",
		"TTT BT",
		"T    T",
		"TTTTTT"
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
				Console.SetCursorPosition(xV * 2, yV);
				if (y >= 0 && y < MapHeight && x >= 0 && x < MapWidth)
					Console.Write(Objects.GetObject(map[y][x]));
				else
				{
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.Write("##");
					Console.ResetColor();
				}
			}
		}
		Console.SetCursorPosition(UI.ViewportWidth - 1, (UI.ViewportHeight + 2) / 2 - 1);
		Console.Write("🫥");
	}
}
