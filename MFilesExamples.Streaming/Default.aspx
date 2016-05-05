<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MFilesExamples.Streaming._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>M-FILES</h1>
        <p class="lead">Doküman indirmeye başlamak için bir Doküman ID belirtin.</p>

        <asp:TextBox ID="txtDocumentId" runat="server"></asp:TextBox>


        <asp:Button ID="btnSend" runat="server" Text="İndir" OnClick="btnSend_Click" />
    </div>


</asp:Content>
