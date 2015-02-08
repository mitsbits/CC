using Impact.TextMessaging.Model;
using System.Web.SessionState;

public class Service
{
    private const string _keypadKey = "Impact.TextMessaging.Model.Keypad";

    public Keypad Keypad(HttpSessionState state)
    {
        var keypad = state[_keypadKey] as Keypad;
        if (keypad != null) return keypad;
        keypad = new Keypad(10, 50);
        state[_keypadKey] = keypad;
        return keypad;
    }

    public void SetKeyPad(HttpSessionState context, Keypad keypad)
    {
        context[_keypadKey] = keypad;
    }
}