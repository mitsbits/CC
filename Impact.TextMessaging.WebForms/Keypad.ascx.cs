using System;

public partial class KeyPad : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSetTimes_OnClick(object sender, EventArgs e)
    {
        var typingTime = 0;
        var fixingTime = 0;
        if ((!int.TryParse(txtTypingTime.Text, out typingTime)
             || !int.TryParse(txtFixingTime.Text, out fixingTime)) ||
            (typingTime <= 0 || fixingTime <= 0))
        {
            pnlError.Visible = true;
            lblErrorMessage.Text = "Invalid time detected. Only possitive integers are allowed.";
            return;
        }
        var assistant = new Service();
        var keypad = assistant.Keypad(Session);
        keypad.SetTimes(typingTime, fixingTime);
        assistant.SetKeyPad(Session, keypad);
        pnlError.Visible = false;
        lblErrorMessage.Text = string.Empty;
    }
}