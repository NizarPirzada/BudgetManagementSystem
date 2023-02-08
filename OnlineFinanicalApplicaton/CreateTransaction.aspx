<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateTransaction.aspx.cs" Inherits="OnlineFinanicalApplicaton.CreateTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Content/custom/budget.css" rel="stylesheet" />
    <style>
        .rbl input[type="radio"]
            {
               margin-left: 10px;
               margin-right: 1px;
            }
    </style>

    <img class="logo" src="Content/Images/ebiaclogo.jpg">
    <div class="jumbotron">
        <!-- Back button which will return the user to the dashboard page. - LS -->
        <asp:Button ID="backButton" CausesValidation="false" runat="server" Style="text-align: left; cursor: pointer;" OnClick="BackButton_Click" Text="Back" ToolTip="Use this button to navigate to the previous page" />
        <br />
        <br />
        <h2 style="font-weight: bold;">Create Transaction</h2>
        <br />
        <br />
        <div class="row">
            <!-- My Markup -->
            <div>
                <div>
                    <asp:Label runat="server" ID="lblTransactionDesc" Text="Description:"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="txtDescription" Class="loginTextBoxes" runat="server" ToolTip="Input a description for the transaction that will be helpful and remind you what the particular transaction was for."></asp:TextBox>
                </div>
            </div>
            <br />
            <div>
                <div>
                    <asp:Label runat="server" ID="lblAmount" Text="Amount:"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="txtAmount" Class="loginTextBoxes" runat="server" ToolTip="Input the total amount of the transaction" ></asp:TextBox>
                </div>
            </div>
            <br />
            <div>
                <div>
                    <asp:Label runat="server" ID="lblDescription" Text="Transaction Type:" ToolTip="Choose if the new transaction was an expense or income"></asp:Label>
                </div>
                <div style="padding-left:220px">
                    <asp:RadioButtonList CssClass="rbl" ID="rdlTransactionType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">Income</asp:ListItem>
                        <asp:ListItem Value="2">Expense</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <br />
            <div>
                <div>
                    <asp:Label runat="server" ID="lblCategory" Text="Category:"></asp:Label>
                </div>
                <div>
                    <asp:DropDownList runat="server" ID="drpCategory" Width="50%" ToolTip="Choose which category the new transaction will fall under. It is important you choose the right category for the transaction as this information will be important when using the graphs on your Dashboard">
                        <asp:ListItem Value="1">Housing</asp:ListItem>
                        <asp:ListItem Value="2">Consumer Debt</asp:ListItem>
                        <asp:ListItem Value="3">Food and Groceries</asp:ListItem>
                        <asp:ListItem Value="4">Entertainment</asp:ListItem>
                        <asp:ListItem Value="5">Clothing</asp:ListItem>
                        <asp:ListItem Value="6">Transport</asp:ListItem>
                        <asp:ListItem Value="7" Selected="True">Other</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <div>
                <div>
                    <asp:Label runat="server" ID="lblStartDate" Text="Transaction Date:"></asp:Label>
                </div>
                <div>
                    <asp:Calendar Width="280px" Height="280px" class="cal" ID="TransactionStartDate" runat="server" OnDayRender="StartDate_DayRender">
                        <SelectedDayStyle BackColor="DodgerBlue" Font-Bold="True" ForeColor="White" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    </asp:Calendar>
                </div>
            </div>
            <br />
             <div>
                <div>
                    <asp:Label runat="server" ID="lblNote" Text="Attach Note:"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="TransactionNotes" Class="loginTextBoxes" runat="server" ToolTip="Input a small note which will be attached to the new transaction, you can then view the note within the budgets summary page"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <br />
        <!-- Once button is selected it will run 'CreateProjectButton_Click' method, this will create a project with the information inputted by user - LS -->
        <asp:Button ID="createBudgetButton" ValidationGroup="gp1" CssClass="loginSubmitButton" runat="server" Text="Create Transaction" Style="margin-left: 0px; cursor: pointer" UseSubmitBehavior="False" OnClick="CreateTransactionButton_Click" ToolTip="Clicking this button will create the new transaction, ensure you have inputted the correct details for the new transaction." />
        <br />
        <br />
        <!-- Validators that ensure fields for create project are not null, if so a error message will appear. - LS -->
        <asp:RequiredFieldValidator ValidationGroup="gp1" class="validation" runat="server" ControlToValidate="txtAmount" ErrorMessage="Transaction Amount is Required."></asp:RequiredFieldValidator><br />
        <asp:CompareValidator ValidationGroup="gp1" class="validation" runat="server" Operator="DataTypeCheck" Type="Currency"
            ControlToValidate="txtAmount" ErrorMessage="Amount must be a Number." /><br />
        <asp:RequiredFieldValidator ValidationGroup="gp1" class="validation" runat="server" ControlToValidate="txtDescription" ErrorMessage="Transaction Description is Required."></asp:RequiredFieldValidator><br />
          <p runat="server" id="calNotSelected" style="display: inline" class="validation">You didn't select a date.</p>
    </div>

</asp:Content>
