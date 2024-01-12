using System.Globalization;
using System.Reflection;

namespace ConsoleGame;

public static class Util
{
	public static bool CreateConditional(Func<bool> condition, Action action) => condition() &&
		new Func<bool>(() =>
		{
			action();
			return true;
		})();
}

public class SingletonUtil<T> where T : class
{
	private static readonly object s_lock = new object();
	private static T? s_instance;

	static SingletonUtil() { }

	public static T Instance
	{
		get
		{
			if (s_instance == null)
			{
				lock (s_lock)
				{
					CultureInfo cultureInfo = CultureInfo.InvariantCulture;
					s_instance = typeof(T).InvokeMember(typeof(T).Name,
						BindingFlags.CreateInstance |
						BindingFlags.Instance |
						BindingFlags.Public |
						BindingFlags.NonPublic,
						null, null, null, cultureInfo) as T;
				}
			}
			return s_instance!;
		}
	}

	
}
