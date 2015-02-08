using Impact.TextMessaging.DTO;
using Impact.TextMessaging.Model;
using System;

public partial class Query : System.Web.UI.UserControl
{
    public MessageResult _result = null;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnQuery_OnClick(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMessage.Text)) return;
        try
        {
            foreach (var chr in txtMessage.Text)
            {
                var letter = new Letter(chr);
            }
        }
        catch (Exception)
        {
            lblErrorMessage.Text = string.Format("Invalid query '{0}'", txtMessage.Text);
            pnlError.Visible = true;
            txtMessage.Text = string.Empty;
            return;
        }
        var assistant = new Service();
        var keypad = assistant.Keypad(Session);
        _result = keypad.Calculate(txtMessage.Text);
        lblErrorMessage.Text = string.Empty;
        pnlError.Visible = false;
        txtMessage.Text = string.Empty;

    }
}