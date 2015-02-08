namespace Impact.TextMessaging.Model
{
    using System.Linq;

    public class Tap
    {
        private readonly Letter _current;
        private readonly int _index;
        private readonly Letter _next;

        public Tap(int index, Letter current, Letter next)
        {
            _index = index;
            _current = current;
            _next = next;
        }

        public int Index
        {
            get { return _index; }
        }

        public Letter Current
        {
            get { return _current; }
        }

        public Letter Next
        {
            get { return _next; }
        }

        public Tap NextTap(Letter nextLetter)
        {
            return new Tap(Index + 1, Next, nextLetter);
        }

        private bool InOnSameKey(Keypad keypad)
        {

            if (Next.IsWhitespace || Current.IsWhitespace || Current.IsEmpty) return false;
            if (Next.Equals(Current)) return true;
            return keypad.PadKeys.Any(b => b.Slots.Count(s => s.Letter.Equals(Current) || s.Letter.Equals(Next)) == 2);
        }

        private Slot NextPosition(Keypad keypad)
        {
            if (Next.IsWhitespace) return new Slot(1, Letter.Whitespace());
            return keypad.PadKeys.SelectMany(b => b.Slots).FirstOrDefault(s => s.Letter.Equals(Next));
        }

        private Slot CurrentPosition(Keypad keypad)
        {
            return keypad.PadKeys.SelectMany(b => b.Slots).FirstOrDefault(s => s.Letter.Equals(Current));
        }

        internal int Calculate(Keypad keypad)
        {
            var nextPosition = NextPosition(keypad);
            if (nextPosition == null) return 0;
            return (InOnSameKey(keypad))
                ? keypad.FixingTime + (keypad.TypingTime*nextPosition.Index)
                : keypad.TypingTime*nextPosition.Index;
        }
    }
}