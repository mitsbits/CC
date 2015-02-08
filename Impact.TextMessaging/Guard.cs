namespace Impact.TextMessaging
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Guard
    {
        internal static void ForInValidKey(int key)
        {
            if (0 > key || 9 < key)
                throw new ArgumentOutOfRangeException("key");
        }

        internal static void ForInValidLetter(char letter)
        {
            if ((65 > letter || 90 < letter) && letter != 32 && letter != 0)
                throw new ArgumentOutOfRangeException("letter");
        }

        internal static void ForInValidSlot(int slot)
        {
            if (0 > slot || 2 < slot)
                throw new ArgumentOutOfRangeException("slot");
        }

        internal static void ForInValidMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");
            foreach (char letter in message)
            {
                if (letter == 0)
                    throw new ArgumentNullException("letter");
                ForInValidLetter(letter);
            }
        }

        internal static void ForInValidMessage(IEnumerable<Letter> letters)
        {
            if (letters == null || !letters.Any() || letters.Any(letter => letter.IsEmpty))
                throw new ArgumentNullException("letters");
        }

        internal static void ForInValidButtonConfig(IEnumerable<string> lines)
        {
            if (lines.Count() != 9) throw new ArgumentOutOfRangeException("lines");
        }
    }
}