namespace ConsoleGame;
public class Objects
{
	static Dictionary<char, string> objects = new()
	{
		{' ', "  "},
		{'0', "  "},
		{'T', "🌲"},
		{'K', "🔑"},
		{'L', "🔒"},
		{'M', "🥭"},
		{'E', "💀"},
		{'B', "🛌" }
	};

	static List<char> Colideable = ['T', '0'];

	public static bool IsColideable(char obj) => Colideable.Contains(obj);
	public static string GetObject(char key) => objects[key];
}
