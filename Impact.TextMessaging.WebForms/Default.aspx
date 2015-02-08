<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/DefaultMasterPage.master" %>

<%@ Register Src="~/KeyPad.ascx" TagPrefix="uc1" TagName="KeyPad" %>
<%@ Register Src="~/Query.ascx" TagPrefix="uc1" TagName="Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainplaceholder" runat="Server">
    <uc1:Query runat="server" ID="Query" />
    <uc1:KeyPad runat="server" ID="KeyPad" />
</asp:Content>
