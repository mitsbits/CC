namespace Impact.TextMessaging.Contracts
{
    using DTO;
    public interface IKeypad
    {
        int TypingTime { get; }
        int FixingTime { get; }
        MessageResult Calculate(string message);
    }
}