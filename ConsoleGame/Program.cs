public class Program
{
	public static class Objects
	{
		public static string Air = "0";
		public static string Wall = "1";
		public static string Key = "2";
		public static string Door = "3";
		public static string Mango = "4";
		public static string Enemy = "5";
	}

	static int ViewportWidth = 15;
	static int ViewportHeight = 15;
	static int MapWidth = 18;
	static int MapHeight = 16;
	static int ViewDistance = 6;

	static string[] Map = [
		"111111111111111111",
		"100002000000000001",
		"100002000000000001",
		"100002000000000001",
		"100003000000000001",
		"100000000000000001",
		"100000000000000001",
		"100000000000000001",
		"100000000000000001",
		"100000000000000001",
		"100000000000000001",
		"100000000000000001",
		"100000000000000001",
		"100000000000000001",
		"100000000000000001",
		"111111111111111111"
	];

	static int PlayerX = 0;
	static int PlayerY = 0;
	static List<(int, int)> Walls = new();

	public static void Main(string[] args)
	{
		Console.CursorVisible = false;
		Task.Run(() =>
		{
			for (int y = 0; y < ViewportHeight; y++)
			{
				for (int x = 0; x < ViewportWidth; x++)
				{
					if (x == 0 || x == ViewportWidth - 1 || y == 0 || y == ViewportHeight - 1)
					{
						Console.SetCursorPosition(x * 2, y);
						Console.ForegroundColor = ConsoleColor.White;
						Console.BackgroundColor = ConsoleColor.White;
						Console.Write("##");
						Console.ResetColor();
					}
				}
			}

			while (true)
			{

				for (int y = PlayerY - ViewportHeight / 2 + 2, yV = 1; yV < ViewportHeight - 1; yV++, y++)
				{
					for (int x = PlayerX - ViewportWidth / 2 + 3, xV = 1; xV < ViewportWidth - 1; xV++, x++)
					{
						Console.SetCursorPosition(xV * 2, yV);
						if (y >= 0 && y < MapHeight && x >= 0 && x < MapWidth)
						{
							var obj = $"{Map[y][x]}";

							if (obj == Objects.Air) Console.Write("  ");
							if (obj == Objects.Wall)
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.BackgroundColor = ConsoleColor.Black;
								Console.Write("🌲");
								Console.ResetColor();
							}
							if (obj == Objects.Key) Console.Write("🔑");
							if (obj == Objects.Door) Console.Write("🔒");
							if (obj == Objects.Mango) Console.Write("🥭");
							if (obj == Objects.Enemy) Console.Write("💀");

						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Green;
							Console.BackgroundColor = ConsoleColor.Green;
							Console.Write("##");
							Console.ResetColor();
						}
					}
				}

				Console.SetCursorPosition(ViewportWidth / 2 + 5, ViewportHeight / 2);
				Console.Write("😒");
				Console.ResetColor();
				Thread.Sleep(10);

				Console.SetCursorPosition(0, ViewportHeight + 2);
				Console.Write("                     ");
				Console.SetCursorPosition(0, ViewportHeight + 2);
				Console.Write($"{PlayerX}:{PlayerY}");
			}

		});
		while (true)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.DownArrow:
					Console.SetCursorPosition(PlayerX * 2, PlayerY + 1);
					Console.Write("⚔️");
					break;
				case ConsoleKey.LeftArrow:
					Console.SetCursorPosition(PlayerX * 2 - 2, PlayerY);
					Console.Write("⚔️");
					break;
				case ConsoleKey.UpArrow:
					Console.SetCursorPosition(PlayerX * 2, PlayerY - 1);
					Console.Write("⚔️");
					break;
				case ConsoleKey.RightArrow:
					Console.SetCursorPosition(PlayerX * 2 + 2, PlayerY);
					Console.Write("⚔️");
					break;
				case ConsoleKey.W:
					PlayerY = PlayerY == 0 ? 0 : PlayerY - 1;
					break;
				case ConsoleKey.A:
					PlayerX = PlayerX == 0 ? 0 : PlayerX - 1;
					break;
				case ConsoleKey.S:
					PlayerY = PlayerY == MapHeight - 3 ? MapHeight - 3 : PlayerY + 1;
					break;
				case ConsoleKey.D:
					PlayerX = PlayerX == MapWidth - 3 ? MapWidth - 3 : PlayerX + 1;
					break;
			}
		}

	}
}