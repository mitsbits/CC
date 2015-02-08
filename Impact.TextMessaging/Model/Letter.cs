namespace Impact.TextMessaging.Model
{
    public class Letter
    {
        private readonly char _value;

        public Letter(char letter)
        {
            Guard.ForInValidLetter(letter);
            _value = letter;
        }

        public char Value
        {
            get { return _value; }
        }

        public bool IsEmpty
        {
            get { return Value == 0; }
        }

        public bool IsWhitespace
        {
            get { return Value == 32; }
        }

        public static Letter Whitespace()
        {
            return new Letter((char) 32);
        }

        public static Letter Empty()
        {
            return new Letter((char) 0);
        }

        public override bool Equals(object obj)
        {
            var item = obj as Letter;

            if (item == null)
            {
                return false;
            }

            return Value.Equals(item.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}