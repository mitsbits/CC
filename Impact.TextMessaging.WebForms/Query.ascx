<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Query.ascx.cs" Inherits="Query" %>


<asp:Literal ID="ltrlResult" runat="server"></asp:Literal>
<div class="query">
    <fieldset>
        <label for="txtMessage">Message</label>
        <asp:TextBox ID="txtMessage" runat="server" ClientIDMode="Static" CssClass="message" MaxLength="160"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" Text="Calculate" OnClick="btnQuery_OnClick" CssClass="btnquery" />
    </fieldset>  
    <asp:Panel runat="server" ID="pnlError" ClientIDMode="Static" CssClass="error" Visible="False">
        <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
    </asp:Panel>

    <% if (_result != null) { %>
    <fieldset>
        <span>Message</span> <span class="message"><%= _result.Message.ToUpper() %></span>
        <br />
        <span>Time </span><span class="total"><%= _result.Value %></span>
        <div class="taps">
            <% foreach (var tap in _result.Taps) { %>
            <span class="tap">
                <span class="letter">[<%= tap.Letter.ToString() %>]</span>
                <span class="value"><%= tap.Value.ToString() %></span>
            </span>
            <%} %>
        </div>
    </fieldset>
    <%} %>
</div>
