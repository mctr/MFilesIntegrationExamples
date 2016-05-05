<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MFilesExamples.Streaming._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">

        <p class="lead">Video izlemeye başlamak için M-Files içerisinden bir Object Id Belirtiniz.</p>
        <p class="lead">Desteklenen video formatı: mp4</p>

        <asp:TextBox ID="txtDocId" runat="server"></asp:TextBox>
        <asp:Button ID="btnSend" runat="server" Text="Görüntüle" OnClick="btnSend_Click" />

        <br />
        <asp:Panel ID="videoPanel" runat="server" Visible="false">
            <video width="480" height="320" controls="controls" autoplay="autoplay" runat="server" name="videoPlayer" id="videoPlayer">
                <source src="" type="video/mp4">
            </video>
        </asp:Panel>
    </div>
</asp:Content>
