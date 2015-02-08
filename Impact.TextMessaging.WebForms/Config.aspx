<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="Config.aspx.cs" Inherits="KayPadConfig" %>

<%@ Register Src="~/KeypadConfig.ascx" TagPrefix="uc1" TagName="KeypadConfig" %>



<asp:Content ID="Content2" ContentPlaceHolderID="mainplaceholder" Runat="Server">
    <uc1:KeypadConfig runat="server" ID="KeypadConfig" />
</asp:Content>

