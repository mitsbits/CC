namespace Impact.TextMessaging
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class Extensions
    {
        public static IReadOnlyCollection<Letter> ToLetters(this string message)
        {
            Guard.ForInValidMessage(message);
            List<Letter> bucket = message.Select(letter => new Letter(letter)).ToList();
            return new ReadOnlyCollection<Letter>(bucket);
        }

        public static IReadOnlyCollection<Tap> ToTaps(this IReadOnlyCollection<Letter> letters)
        {
            Guard.ForInValidMessage(letters);
            var bucket = new List<Tap>();
            Letter[] source = letters.ToArray();
            Tap current = null;
            for (int i = 0; i < source.Count(); i++)
            {             
                if (i == 0)
                {
                    current = new Tap(i, Letter.Empty(), source[i]);
                    bucket.Add(current);
                }
                else
                {
                    var next = current.NextTap(source[i]);
                    bucket.Add(next);
                    current = next;
                }
            }
            return new ReadOnlyCollection<Tap>(bucket);
        }

        public static IReadOnlyCollection<Tap> ToTaps(this string message)
        {
            IReadOnlyCollection<Letter> letters = message.ToLetters();
            return letters.ToTaps();
        }

        public static IReadOnlyCollection<PadKey> ToButtons(this string[] config)
        {
            Guard.ForInValidButtonConfig(config);
            var zero = new PadKey(0);
            zero.AddSlot(Letter.Whitespace());
            var buttons = new List<PadKey> { zero };
            for (short i = 0; i < 9; i++)
            {
                var button = new PadKey(i+1);
                if (!String.IsNullOrWhiteSpace(config[i]))
                {
                    foreach (var letter in config[i])
                    {
                        if (!string.IsNullOrWhiteSpace(letter.ToString()))
                        {
                            button.AddSlot(new Letter(letter));
                        }
                    }
                }
                buttons.Add(button);
            }
            return new ReadOnlyCollection<PadKey>(buttons);
        }

        public static string[] ToConfig(this Keypad keypad)
        {
            var buttons = keypad.PadKeys.Where(b => b.Digit > 0).OrderBy(b=>b.Digit).ToList();
            var bucket = buttons.Select(
                button => string.Join("", 
                    button.Slots.Select(s => s.Letter.ToString())))
                    .ToList();
            return bucket.ToArray();
        }

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}