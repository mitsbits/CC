<%@ Control Language="C#" AutoEventWireup="true" CodeFile="KeypadConfig.ascx.cs" Inherits="KeypadConfig" %>
                
<div class="keypad config">
    <div class="options">
        <fieldset>
            <label for="txtTypingTime">Typing Time</label>
            <asp:TextBox ID="txtTypingTime" runat="server" ClientIDMode="Static"></asp:TextBox>
            <label for="txtFixingTime">Fixing Time</label>
            <asp:TextBox ID="txtFixingTime" runat="server" ClientIDMode="Static"></asp:TextBox>
            <asp:Button ID="btnSave" runat="server" Text="Save" ClientIDMode="Static" OnClick="btnSave_OnClick" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_OnClick" />
            <asp:Button ID="btnRandomize" runat="server" Text="Randomize" OnClick="btnRandomize_OnClick" />
            <asp:Button ID="btnGoToQuery" runat="server" Text="Back to Query" PostBackUrl="~/Default.aspx" />
        </fieldset>
    </div>
    <asp:Panel runat="server" ID="pnlError" ClientIDMode="Static" CssClass="error" Visible="False">
        <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
    </asp:Panel>
    <asp:Repeater runat="server" ID="rptrConfig">
        <ItemTemplate>
            <div class="keypadrow">
                <div class="digit"><span><%# Container.ItemIndex + 1 %></span></div>
                <div class="configline">
                    <asp:TextBox runat="server" ClientIDMode="Static" Text="<%# Container.DataItem %>" CssClass="config"></asp:TextBox>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
