<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Keypad.ascx.cs" Inherits="KeyPad" %>
<%@ Import Namespace="Impact.TextMessaging.Model" %>
<%
    Func<PadKey, string> renderButton = (button) =>
    {
        var builder = new StringBuilder("<div class='padkey'>");
        builder.AppendLine(string.Format("<span class='digit'>{0}</span>", button.Digit));
        builder.Append("<div>");
        foreach (var slot in button.Slots)
        {
            builder.AppendLine(string.Format("<span class='letter'>{0}</span>", slot.Letter.ToString()));
        }
        builder.Append("</div>");
        builder.AppendLine("</div>");
        return builder.ToString();
    };
    var keypad = new Service().Keypad(Session);
    txtTypingTime.Text = keypad.TypingTime.ToString();
    txtFixingTime.Text = keypad.FixingTime.ToString();
%>

<div class="keypad">
    <div class="options">
        <fieldset>
            <label for="txtTypingTime">Typing Time</label>
            <asp:TextBox ID="txtTypingTime" runat="server" ClientIDMode="Static" ></asp:TextBox>
            <label for="txtFixingTime">Fixing Time</label>
            <asp:TextBox ID="txtFixingTime" runat="server" ClientIDMode="Static" ></asp:TextBox>
            <asp:Button ID="btnSetTimes" runat="server" Text="Update Keypad Times" ClientIDMode="Static" OnClick="btnSetTimes_OnClick" /> 
            <asp:Button  ID="btnGoToConfiguration" runat="server" ClientIDMode="Static" Text="Keypad Configuration" PostBackUrl="Config.aspx" ></asp:Button>
        </fieldset>
    </div>     
        <asp:Panel runat="server" ID="pnlError" ClientIDMode="Static" CssClass="error" Visible="False">
        <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
    </asp:Panel>
    <div class="keypadrow">
        <% for (var i = 1; i < 4; i++)
           { %>

        <%= renderButton(keypad.PadKeys[i])%>

        <%} %>
    </div>
    <div class="keypadrow row">
        <% for (var i = 4; i < 7; i++)
           { %>

        <%= renderButton(keypad.PadKeys[i])%>

        <%} %>
    </div>
    <div class="keypadrow row">
        <% for (var i = 7; i < 10; i++)
           { %>

        <%= renderButton( keypad.PadKeys[i])%>

        <%} %>
    </div>
    <div class="keypadrow row">
        <div class="zeropadkey">
            <span class="digit">0</span>
            <div>
                <span class="letter">_</span>
            </div>
        </div>
    </div>
</div>

