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

    public static string PadCenter(this string source, int size, char padChar)
    {
        int padding = size - source.Length;
        int padLeft = padding / 2 + source.Length;
        return source.PadLeft(padLeft, padChar).PadRight(size, padChar);
    }

    public static IEnumerable<string> ChunkSplit(this string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }
    public static IEnumerable<string> WordWrap(this string str, int chunkSize)
    {
        var lines = new List<string>();
        var words = str.Split(" ");
        string result = "";
        foreach (var item in words)
        {
            if (item.Length + result.Length <= chunkSize)
            {
                result += $"{item} ";

            }
            else
            {
                lines.Add(result);
                result = $"{item} ";
            }
        }
        lines.Add(result);
        return lines;
    }

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
