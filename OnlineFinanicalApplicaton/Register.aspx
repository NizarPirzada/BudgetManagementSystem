<%@ Page Title="Registration Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnlineFinanicalApplicaton.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style type="text/css">
        .loginVal {
            color: red;
            text-align: center;
        }

        .loginRegisterText {
            color: blue;
        }

        .jumbotron {
            max-width: 700px;
            margin: 0 auto;
            text-align: center;
        }

        .loginTextBoxes {
            width: 100%;
            height: 27px;
            display: inline-block;
            text-align: left;
        }

        .loginText {
            text-align: right;
        }

        .loginSubmitButton {
            height: 40px;
            width: 40%
        }

        .logo {
            display: block;
            width: 28vw;
            height: 22vh;
            margin: auto;
        }

        .validation {
            color: red;
            font-weight: bold;
            text-align: center;
        }
    </style>

    <img class="logo" src="Content/Images/ebiaclogo.jpg">

    <div class="jumbotron">

        <%-- Simple form below filled with text boxes, input boxes and validation field checks -CR --%>
        <asp:Button ID="backToLoginButton" CausesValidation="false" runat="server" Style="text-align: left; cursor: pointer;" OnClick="BackToLoginButton_Click" Text="Back" />
        <br />
        <br />
        <h2 style="font-weight: bold;">Registration</h2>
        <br />
        <br />
        <div class="row">
            <div class="col-md-4">
                <p class="loginText">Forename:</p>

                <p class="loginText">Surname:</p>

                <p class="loginText">Email:</p>

                <p class="loginText">Password:</p>
                <br />
                <p class="loginText">Confirm Password:</p>
                <br />

            </div>
            <div class="col-md-8">
                <asp:TextBox ID="fName" Class="loginTextBoxes" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="lName" Class="loginTextBoxes" runat="server"></asp:TextBox>
                <br />
                <br />
                <br />
                <asp:TextBox ID="emailInput" Class="loginTextBoxes" runat="server"></asp:TextBox>
                <br />
                <br />
                <br />
                <asp:TextBox ID="passwordInput" Class="loginTextBoxes" TextMode="Password" runat="server"></asp:TextBox>
                <br />
                <br />
                <br />
                <br />
                <asp:TextBox ID="passwordCheck" Class="loginTextBoxes" TextMode="Password" runat="server"></asp:TextBox>
                <br />
                <br />
            </div>
        </div>
        <br />
        <br />
        <asp:Button ID="loginSubmit" Class="loginSubmitButton" runat="server" Text="Register" Style="margin-left: 0px; cursor: pointer" UseSubmitBehavior="False" OnClick="loginSubmit_Click" /><br />
        <p runat="server" id="userNoExist" class="loginVal">This user already exists, enter another email address</p>
        <br />

        <!--Email validation - AJ -->
        <asp:RegularExpressionValidator class="validation" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="emailInput" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator><br />

        <!--First name validator -->
        <asp:RequiredFieldValidator class="validation" runat="server" ControlToValidate="fName" ErrorMessage="Forename is required."></asp:RequiredFieldValidator><br />

        <!--Last name validator - AJ -->
        <asp:RequiredFieldValidator class="validation" runat="server" ControlToValidate="lName" ErrorMessage="Surname is required."></asp:RequiredFieldValidator><br />

        <!--After looking for bugs we discovered that an email validation text did not appear when email was not inserted. This has been fixed - AJ -->
        <!--Email address validator - AJ -->
        <asp:RequiredFieldValidator class="validation" runat="server" ControlToValidate="emailInput" ErrorMessage="Email is required."></asp:RequiredFieldValidator><br />

        <!--Password field validator - AJ -->
        <asp:RequiredFieldValidator class="validation" runat="server" ControlToValidate="passwordInput" ErrorMessage="User Password is required."></asp:RequiredFieldValidator><br />

        <!--Password check validator - AJ -->
        <asp:CompareValidator class="validation" runat="server" ControlToValidate="passwordCheck" ControlToCompare="passwordInput" ErrorMessage="Password does not match" ToolTip="Password must be the same" /><br />

        <!--Password check match validator - AJ -->
        <asp:RequiredFieldValidator class="validation" ID="passwordConfirmedVal" runat="server" ErrorMessage="User Password confirmation is required." ControlToValidate="passwordCheck" ToolTip="Compare Password is a REQUIRED field"> </asp:RequiredFieldValidator> <br />


    </div>

</asp:Content>
