using Pastel;
using System.Drawing;

namespace ConsoleGame;
public class DialogSystem : GameEvents
{
	public static DialogSystem Source { get; } = SingletonUtil<DialogSystem>.Instance;

	public List<string> Dialogs = new();

	UI UI { get; set; } = UI.Source;

	public override void OnKeyPressed(ConsoleKey key)
	{
		switch (key)
		{
			case ConsoleKey.R:
				Random random = new Random();
				Dialogs.Add("# Name Name - (19)".Pastel(Color.Plum));
				Dialogs.Add(" Hello there!".Pastel(Color.Wheat));
				Console.SetCursorPosition(UI.ViewportWidth + 61, 1);
				var str = "Hello world Hello world";
				for (int i = 0; i < str.Length; i++)
				{
					Console.Beep(random.Next(4, 6) * 100 - 100, 80);
					Console.Write(str[i]);
				}
				UI.Redraw(Player);
				RedrawDialogWindow();
				break;
		}
	}

	public void RedrawDialogWindow()
	{
		//int dialog = 0;
		//Dialogs.TakeLast(20).ToList().ForEach(v =>
		//{
		//	Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
		//	dialog++;

		//	Console.Write(v);
		//});
	}
}
