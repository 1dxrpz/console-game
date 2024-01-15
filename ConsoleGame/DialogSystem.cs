using Pastel;
using System.Drawing;
using System.Globalization;
using System.Speech.Synthesis;

namespace ConsoleGame;
public class DialogSystem : GameEvents
{
    class Replic
    {
        public string Title = "";
        public List<string> Lines = new();
    }
    public static DialogSystem Source { get; } = SingletonUtil<DialogSystem>.Instance;

    public int MaxLineWidth = 30;
    public int MaxLineCount = 18;
    List<Replic> Dialogs = new();

    public void SetAnswers(string first = "", string second = "", string third = "")
    {
        Answers[0] = first.Length == 0 ? "" : $" (R) ".Pastel(Color.Azure) + $"{first.PadRight(25, ' ')}";
        Answers[1] = second.Length == 0 ? "" : $" (F) ".Pastel(Color.Azure) + $"{second.PadRight(25, ' ')}";
        Answers[2] = third.Length == 0 ? "" : $" (V) ".Pastel(Color.Azure) + $"{third.PadRight(25, ' ')}";
    }

    string[] Answers = ["", "", ""];

    public void WriteReplic(string title, string str)
    {
        Replic replic = new();
        replic.Title = title;
        str.WordWrap(MaxLineWidth)
            .ToList()
            .ForEach(v =>
            {
                replic.Lines.Add($"| {v}");
            });
        Dialogs.Add(replic);

    }

    List<(string, string)> replics = new()
    {
        ("Кира Йошикагэ", "Меня зовут Кира Йошикагэ. Мне 33 года. Мой дом находится в северо-восточной части Морио, в районе поместий. Работаю в офисе сети магазинов Kame Yu и домой возвращаюсь, самое позднее, в восемь вечера."),
        ("Кира Йошикагэ", "Не курю, выпиваю изредка. Ложусь спать в 11 вечера и убеждаюсь, что получаю ровно восемь часов сна, несмотря ни на что."),
        ("Кира Йошикагэ", "Перед сном я пью тёплое молоко, а также минут двадцать уделяю разминке, поэтому до утра сплю без особых проблем. Утром я просыпаюсь, не чувствуя ни усталости, ни стресса, словно младенец."),
        ("Кира Йошикагэ", "На медосмотре мне сказали, что никаких проблем нет. Я пытаюсь донести, что я обычный человек, который хочет жить спокойной жизнью. Я не забиваю себе голову проблемами вроде побед или поражений, и не обзавожусь врагами, из-за которых не мог бы уснуть."),
        ("Кира Йошикагэ", "Я знаю наверняка: в таком способе взаимодействия с обществом и кроется счастье. Хотя, если бы мне пришлось сражаться, я бы никому не проиграл."),
        ("Кира Йошикагэ", "..."),
        ("Кира Йошикагэ", "..."),
    };

    UI UI { get; set; } = UI.Source;

    public int currentReplic = 0;

    public override void OnKeyPressed(ConsoleKey key)
    {
        Random random = new Random();
        switch (key)
        {
            case ConsoleKey.R:
                WriteReplic(
                    $"# {replics[currentReplic].Item1} - (1)",
                    replics[currentReplic].Item2
                );
                currentReplic++;
                UI.Redraw(Player);
                RedrawDialogWindow();
                break;
        }
    }

    public void RedrawDialogWindow()
    {
        SetAnswers("Continue");

        int dialog = 0;
        var last = Dialogs.Last();
        for (int i = 0; i < UI.ViewportHeight - 2; i++)
        {
            Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + i);
            Console.Write(new string(' ', MaxLineWidth + 5));
        }

        Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
        if (Dialogs.Count != 0)
        {
            var prev = Dialogs
                .Take(Dialogs.Count - 1);
            prev.ToList()
                .SelectMany(v => new List<string>() { v.Title }.Concat(v.Lines))
                .TakeLast(MaxLineCount - last.Lines.Count)
                .ToList()
                .ForEach(v =>
            {
                Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
                dialog++;
                Console.Write(v.Pastel(Color.Gray));

            });
        }
#pragma warning disable CA1416 // Validate platform compatibility

        Random rand = new Random();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        synthesizer.Volume = 100;
        synthesizer.Rate = 2;

        synthesizer.SetOutputToDefaultAudioDevice();
        var builder = new PromptBuilder();

        var task = Task.Run(() =>
        {
            builder.StartVoice(new CultureInfo("ru-RU"));
            builder.StartStyle(new PromptStyle());
            builder.AppendText(string.Join(' ', last.Lines));
            builder.EndStyle();
            builder.EndVoice();
            synthesizer.Speak(builder);
        });


        Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
        Console.Write(last.Title.Pastel(Color.Plum));
        dialog++;

        last.Lines.ToList().ForEach(v =>
        {
            Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
            dialog++;
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write($"{v[i]}".Pastel(Color.Wheat));
                Thread.Sleep(10);
            }
        });

        Answers
            .Where(v => v.Length != 0)
            .ToList()
            .ForEach(v =>
        {
            Console.SetCursorPosition(UI.ViewportWidth + 61, 1 + dialog);
            dialog++;
            Console.Write(v
                .PastelBg(Color.FromArgb(30, 30, 30))
                .Pastel(Color.Gold)
            );
        });

    }
}
