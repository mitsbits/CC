namespace Impact.TextMessaging.Model
{
    public class Slot
    {
        public Slot(int index, Letter letter)
        {
            Letter = letter;
            Index = index;
        }

        public Letter Letter { get; private set; }
        public int Index { get; private set; }
    }
}