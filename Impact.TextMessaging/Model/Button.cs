namespace Impact.TextMessaging.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class PadKey
    {
        private readonly int _key;
        private readonly IList<Slot> _slots;

        public PadKey(int key)
        {
            Guard.ForInValidKey(key);
            _key = key;
            _slots = new List<Slot>();
        }

        public int Digit
        {
            get { return _key; }
        }

        public Letter this[int i]
        {
            get
            {
                if (Digit == 0) return Letter.Whitespace();
                if (_slots.Count() <= i) return Letter.Empty();
                Slot slot = _slots.OrderBy(l => l.Index).ToArray()[i];
                return slot == null ? Letter.Empty() : slot.Letter;
            }
        }

        public IReadOnlyList<Slot> Slots
        {
            get { return (IReadOnlyList<Slot>) _slots; }
        }

        public void SetSlot(int spot, Letter letter, out Letter ejected)
        {
            if (Digit == 0)
            {
                ejected = Letter.Whitespace();
                return;
            }
            if (_slots.Count() <= spot)
            {
                ejected = Letter.Empty();
                return;
            }
            Slot ejectedSlot = _slots.OrderBy(l => l.Index).ToArray()[spot];
            ejected = ejectedSlot.Letter;
            _slots.Add(new Slot(ejectedSlot.Index, letter));
        }

        public void AddSlot(Letter letter)
        {
            if (Digit <= 0) return;
            int newIndex = (_slots.Any()) ? _slots.Max(s => s.Index) + 1 : 1;
            _slots.Add(new Slot(newIndex, letter));
        }
    }
}