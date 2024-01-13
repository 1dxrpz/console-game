using Pastel;
using System.Drawing;
using System.Linq;

namespace ConsoleGame;
public class DialogSystem : GameEvents
{
	class Replic
	{
		public string Title;
		public List<string> Lines = new();
	}
	public static DialogSystem Source { get; } = SingletonUtil<DialogSystem>.Instance;

	public int MaxLineWidth = 35;
	public int MaxLineCount = 18;
	List<Replic> Dialogs = new();

	public void WriteReplic(string str, string title)
	{
		Replic replic = new();
		replic.Title = title.Pastel(Color.Plum);
		str.WordWrap(MaxLineWidth)
			.ToList()
			.ForEach(v =>
			{
				replic.Lines.Add($"| {v}".Pastel(Color.Wheat));
			});
		Dialogs.Add(replic);
	}

	UI UI { get; set; } = UI.Source;

	public override void OnKeyPressed(ConsoleKey key)
	{
		Random random = new Random();
		switch (key)
		{
			case ConsoleKey.R:

				WriteReplic(
					$"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam turpis ante, accumsan at turpis in, blandit laoreet libero. {random.Next(200)}",
					"# Name name - (19)"
				);
				Console.SetCursorPosition(UI.ViewportWidth + 61, 1);

				UI.Redraw(Player);
				RedrawDialogWindow();
				break;
		}
	}

	public void RedrawDialogWindow()
	{
		int dialog = 0;
		Random random = new Random();
		if (Dialogs.Count != 0)
		{

			Dialogs.TakeLast(MaxLineCount).Take(Dialogs.Count - 1).ToList().ForEach(v =>
			{
				Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
				Console.Write(new string(' ', MaxLineWidth));
				Console.Write(v.Title);
				v.Lines.ForEach(line =>
				{
					Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
					dialog++;
					Console.Write(line);
				});
			});
		}

		var last = Dialogs.Last();

		Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
		Console.Write(last.Title);
		last.Lines.ToList().ForEach(v =>
		{
			Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
			Console.Write(new string(' ', MaxLineWidth));
			Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
			dialog++;
			Console.Write(v);
		});

	}
}
