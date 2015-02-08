namespace Impact.TextMessaging.Model
{
    using Contracts;
    using DTO;
    using System.Collections.Generic;
    using System.Linq;

    public class Keypad : IKeypad
    {
        private readonly IReadOnlyCollection<PadKey> _buttons;
        private int _fixingTime;
        private int _typingTime;
        private static readonly string[] _defConfig = { "", "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ" };

        public Keypad(int typingTime, int fixingTime)
        {
            _typingTime = typingTime;
            _fixingTime = fixingTime;
            _buttons = _defConfig.ToButtons();
        }


        public Keypad(int typingTime, int fixingTime, string[] config)
        {
            _typingTime = typingTime;
            _fixingTime = fixingTime;
            _buttons = config.ToButtons();
        }

        public void SetTimes(int typingTime, int fixingTime)
        {
            _typingTime = typingTime;
            _fixingTime = fixingTime;
        }

        public IReadOnlyList<PadKey> PadKeys
        {
            get { return _buttons.ToList(); }
        }

        public int TypingTime
        {
            get { return _typingTime; }
        }

        public int FixingTime
        {
            get { return _fixingTime; }
        }


        public MessageResult Calculate(string message)
        {
            IReadOnlyCollection<Tap> taps = message.ToTaps();
            var bucket = taps.Select(
                tap => new TapResult()
                {
                    Letter = tap.Next, 
                    Value = tap.Calculate(this)
                }).ToList();

            return new MessageResult(bucket, message);
        }

   
    }
}