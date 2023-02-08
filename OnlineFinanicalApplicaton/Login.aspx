<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineFinanicalApplicaton.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        
    <link href="Content/custom/login.css" rel="stylesheet" />

    <img class="Logo" src="Content/Images/ebiaclogo.jpg">

    <div class="jumbotron">

        <asp:Panel runat="server" DefaultButton="LoginSubmit">

            <div class="LoginCenter">

                <h2 style="font-weight: bold;">Please log in:</h2>

                <br />

                <p>Email:</p>

                <asp:TextBox ID="emailInput" Class="LoginTextBoxes" runat="server"></asp:TextBox>

                <br /><br />

                <p>Password:</p>

                <asp:TextBox ID="passwordInput" Class="LoginTextBoxes" TextMode="Password" runat="server"></asp:TextBox>

                <br /><br /><br />

                <asp:Button ID="loginSubmit" Class="LoginSubmitButton" runat="server" Text="Login" style="margin-left: 0px; cursor:pointer" UseSubmitBehavior="False" OnClick="LoginSubmit_Click" />

                <br /><br />

                <!--After looking for bugs we discovered the previous text suggested that the email was used. So made it ambigous - AJ -->
                <p runat="server" id="invalidUser" style="display:inline" class="LoginVal">Login failed. Invalid login details</p>
                
                <br /><br />

                <a style="font-weight: bold;" href="Register.aspx">Don't have an account? Click here to register.</a>

            </div>

          </asp:Panel>

    </div>
</asp:Content>
