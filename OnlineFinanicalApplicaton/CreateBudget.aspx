<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBudget.aspx.cs" Inherits="OnlineFinanicalApplicaton.CreateBudget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <link href="Content/custom/budget.css" rel="stylesheet" />

    <img class="logo" src="Content/Images/ebiaclogo.jpg">
    <div class="jumbotron">
        <!-- Back button which will return the user to the dashboard page. - LS -->
        <asp:Button ID="backButton" CausesValidation="false" runat="server" Style="text-align: left; cursor: pointer;" OnClick="BackButton_Click" Text="Back" ToolTip="Use this button to navigate to the previous page" />
        <br />
        <br />
        <h2 style="font-weight: bold;">Create Budget</h2>
        <br />
        <br />
        <div class="row">


            <!-- My Markup -->

            <div>
                <div>
                    <asp:Label runat="server" ID="lblBudget" Text="BudgetName:"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="txtBudgetName" Class="loginTextBoxes" runat="server" ToolTip="Choose a budget name that is appropriate. For example if this budget is being used to save for electric, heating etc, name the budget Utilities"></asp:TextBox>
                </div>
            </div>
            <div>
                <div>
                    <asp:Label runat="server" ID="lblBalance" Text="Balance:"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="txtBalanace" Class="loginTextBoxes" runat="server" ToolTip="Input the starting balance of the new budget"></asp:TextBox>
                </div>
            </div>
            <div>
                <div>
                    <asp:Label runat="server" ID="lblDescription" Text="Description:"></asp:Label>
                </div>
                <div>
                    <asp:TextBox ID="txtDescription" Class="loginTextBoxes" TextMode="MultiLine" runat="server" ToolTip="Input a description about the budget, this could be used to remind yourself of recurring bills, explain what the budget is, end goals etc"></asp:TextBox>
                </div>
            </div>
            <div>
                <div>
                    <asp:Label runat="server" ID="lblCategory" Text="Category:"></asp:Label>
                </div>
                <div>
                    <asp:DropDownList runat="server" ID="drpCategory" Width="50%" ToolTip="Choose a category for the budget for example: if you are saving for housing bills or goods choose 'Housing'">
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
                    <asp:Label runat="server" ID="lblStartDate" Text="Start Date:"></asp:Label>
                </div>
                <div>
                    <asp:Calendar Width="280px" Height="280px" class="cal" ID="startDate" runat="server" OnDayRender="StartDate_DayRender">
                        <SelectedDayStyle BackColor="DodgerBlue" Font-Bold="True" ForeColor="White" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    </asp:Calendar>
                </div>
            </div>
            <br />
           
        </div>
        <br />
        <br />
        <!-- Once button is selected it will run 'CreateProjectButton_Click' method, this will create a project with the information inputted by user - LS -->
        <asp:Button ID="createBudgetButton" CssClass="btn btn-primary" runat="server" Text="Create Budget" Style="margin-left: 0px; cursor: pointer" UseSubmitBehavior="False" OnClick="CreateBudgetButton_Click" ToolTip="Clicking this button will create the budget, ensure you have entered the correct details" /><br />
        <br />
        <!-- Validators that ensure fields for create project are not null, if so a error message will appear. - LS -->
        <asp:RequiredFieldValidator class="validation" runat="server" ControlToValidate="txtBudgetName" ErrorMessage="Budget name is required."></asp:RequiredFieldValidator><br />
        <asp:RequiredFieldValidator class="validation" runat="server" ControlToValidate="txtBalanace" ErrorMessage="Budget starting amount is required."></asp:RequiredFieldValidator><br />
        <asp:CompareValidator class="validation" runat="server" Operator="DataTypeCheck" Type="Currency"
            ControlToValidate="txtBalanace" ErrorMessage="Value must be a number." /><br />
        <asp:RequiredFieldValidator class="validation" runat="server" ControlToValidate="txtDescription" ErrorMessage="Budget description is required."></asp:RequiredFieldValidator><br />
        <p runat="server" id="calNotSelected" style="display: inline" class="validation">You didn't select a date.</p>
    </div>

</asp:Content>
