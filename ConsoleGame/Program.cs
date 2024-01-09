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

		public static List<string> Colideable = [ Wall ];
		public static bool IsColideable(char obj) => Colideable.Contains($"{obj}");
	}


	static int ViewportWidth = 25;
	static int ViewportHeight = 25;
	static int MapWidth = 6;
	static int MapHeight = 6;
	static int ViewDistance = 6;

	static string[] Map = [
		"111111",
		"100001",
		"100001",
		"111001",
		"100001",
		"111111"
	];

	static int PlayerX = 1;
	static int PlayerY = 1;
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

				for (int y = PlayerY - ViewportHeight / 2 + 1, yV = 1; yV < ViewportHeight - 1; yV++, y++)
				{
					for (int x = PlayerX - ViewportWidth / 2 + 1, xV = 1; xV < ViewportWidth - 1; xV++, x++)
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
							Console.ForegroundColor = ConsoleColor.DarkGreen;
							Console.BackgroundColor = ConsoleColor.DarkGreen;
							Console.Write("##");
							Console.ResetColor();
						}
					}
				}

				Console.SetCursorPosition((ViewportWidth - 1), (ViewportHeight + 2) / 2 - 1);
				Console.Write("😒");
				Console.ResetColor();
				Thread.Sleep(30);

				Console.SetCursorPosition(0, ViewportHeight);
				Console.Write("                     ");
				Console.SetCursorPosition(0, ViewportHeight);

				Console.ForegroundColor = ConsoleColor.Blue;
				Console.Write($"▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃");
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine($"▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃▃");

				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write($"██████████████████");
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine($"████████████████████████████████");

				Console.WriteLine($"{PlayerX}:{PlayerY}");
			}

		});
		while (true)
		{
			switch (Console.ReadKey(true).Key)
			{
				//case ConsoleKey.DownArrow:
				//	Console.SetCursorPosition(PlayerX * 2, PlayerY + 1);
				//	Console.Write("⚔️");
				//	break;
				//case ConsoleKey.LeftArrow:
				//	Console.SetCursorPosition(PlayerX * 2 - 2, PlayerY);
				//	Console.Write("⚔️");
				//	break;
				//case ConsoleKey.UpArrow:
				//	Console.SetCursorPosition(PlayerX * 2, PlayerY - 1);
				//	Console.Write("⚔️");
				//	break;
				//case ConsoleKey.RightArrow:
				//	Console.SetCursorPosition(PlayerX * 2 + 2, PlayerY);
				//	Console.Write("⚔️");
				//	break;
				case ConsoleKey.W:
					if (!Objects.IsColideable(Map[PlayerY - 1][PlayerX]))
						PlayerY = PlayerY == 0 ? 0 : PlayerY - 1;
					break;
				case ConsoleKey.A:
					if (!Objects.IsColideable(Map[PlayerY][PlayerX - 1]))
						PlayerX = PlayerX == 0 ? 0 : PlayerX - 1;
					break;
				case ConsoleKey.S:
					if (!Objects.IsColideable(Map[PlayerY + 1][PlayerX]))
						PlayerY = PlayerY == MapHeight - 2 ? MapHeight - 2 : PlayerY + 1;
					break;
				case ConsoleKey.D:
					if (!Objects.IsColideable(Map[PlayerY][PlayerX + 1]))
						PlayerX = PlayerX == MapWidth - 2 ? MapWidth - 2 : PlayerX + 1;
					break;
			}
		}

	}
}