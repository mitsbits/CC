using Impact.TextMessaging;
using Impact.TextMessaging.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class KeypadConfig : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var keypad = new Service().Keypad(Session);
            var config = keypad.ToConfig();
            txtTypingTime.Text = keypad.TypingTime.ToString();
            txtFixingTime.Text = keypad.FixingTime.ToString();
            rptrConfig.DataSource = config;
            rptrConfig.DataBind();
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {

        var keypad = ValidateConfig();
        if (keypad != null)
        {
            new Service().SetKeyPad(Session, keypad);
            Response.Redirect(Request.RawUrl);
        }

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        var assistant = new Service();
        assistant.SetKeyPad(Session, null);
        var keypad = assistant.Keypad(Session);
        var config = keypad.ToConfig();
        txtTypingTime.Text = keypad.TypingTime.ToString();
        txtFixingTime.Text = keypad.FixingTime.ToString();
        rptrConfig.DataSource = config;
        rptrConfig.DataBind();
        pnlError.Visible = false;
        lblErrorMessage.Text = string.Empty;
    }

    protected void btnRandomize_OnClick(object sender, EventArgs e)
    {
        var defKeyPad = new Keypad(1, 1);
        var alphabet = string.Join("", defKeyPad.ToConfig().ToArray());
        var randomChars = alphabet.ToList().Shuffle();
        var randConfig = new Dictionary<int, string>();
        var rand = new Random();
        foreach (var randomChar in randomChars)
        {
            var key = rand.Next(1, 10);
            if (randConfig.ContainsKey(key))
            {
                var line = randConfig[key];
                line += randomChar;
                randConfig[key] = line;
            }
            else
            {
                randConfig[key] = randomChar.ToString();
            }
        }
        var config = new List<string>();
        for (var key = 1; key <= 9; key++)
        {
            config.Add(randConfig.ContainsKey(key) ? randConfig[key] : string.Empty);
        }
        var assistant = new Service();
        var currentKeypad = assistant.Keypad(Session);
        var keypad = new Keypad(currentKeypad.TypingTime, currentKeypad.FixingTime, config.ToArray());
        assistant.SetKeyPad(Session, keypad);
        rptrConfig.DataSource = config;
        rptrConfig.DataBind();
        pnlError.Visible = false;
        lblErrorMessage.Text = string.Empty;
    }

    private Keypad ValidateConfig()
    {
        pnlError.Visible = false;
        lblErrorMessage.Text = string.Empty;

        var bucket = new List<string>();
        foreach (RepeaterItem item in rptrConfig.Items)
        {
            foreach (var control in item.Controls)
            {
                var txt = control as TextBox;
                if (txt != null)
                {
                    bucket.Add(txt.Text.Trim().ToUpper());
                }
            }
        }
        var bucketValue = string.Join("", bucket.ToArray());
        try
        {
            foreach (var chr in bucketValue)
            {
                var letter = new Letter(chr);
                if (letter.IsEmpty || letter.IsWhitespace)
                {
                    pnlError.Visible = true;
                    lblErrorMessage.Text = "Invalid characters detected. Only english upper case letters are allowed.";
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            pnlError.Visible = true;
            lblErrorMessage.Text = "Invalid characters detected. Only english upper case letters are allowed.";
            return null;
        }
        if (bucketValue.Length > 26)
        {
            pnlError.Visible = true;
            lblErrorMessage.Text = "Duplicate characters detected. A letter can be assigned to a key only once.";
            return null;
        }
        if (bucketValue.Length < 26)
        {
            pnlError.Visible = true;
            lblErrorMessage.Text = "Missing characters detected. All letters of the alphabet must be assigned to keys.";
            return null;
        }
        var defKeyPad = new Keypad(1, 1);
        var alphabet = string.Join("", defKeyPad.ToConfig().ToArray());

        if (alphabet.Except(bucketValue).Any())
        {
            pnlError.Visible = true;
            lblErrorMessage.Text = "Missing characters detected. All letters of the alphabet must be assigned to keys.";
            lblErrorMessage.Text += "<br/>";
            lblErrorMessage.Text += "Duplicate characters detected. A letter can be assigned to a key only once.";
            return null; 
        }

        var typingTime = 0;
        var fixingTime = 0;
        if ((!int.TryParse(txtTypingTime.Text, out typingTime)
             || !int.TryParse(txtFixingTime.Text, out fixingTime)) ||
            (typingTime <= 0 || fixingTime <= 0))
        {
            pnlError.Visible = true;
            lblErrorMessage.Text = "Invalid time detected. Only possitive integers are allowed.";
            return null;
        }

        var keypad = new Keypad(typingTime, fixingTime, bucket.ToArray());
        return keypad;

    }


}