<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Budgets.aspx.cs" Inherits="OnlineFinanicalApplicaton.Budgets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }

        .Grid td {
            padding: 2px;
            border: solid 1px #c1c1c1;
        }

        .Grid th {
            padding: 4px 1px;
            font-weight:bold;
            background: #363670 url(Content/Images/grid-header.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 0.9em;
            color: #474747;            
        }

        .Grid .alt {
            background: #fcfcfc url(Content/Images/grid-alt.png) repeat-x top;
        }

        .Grid .pgr {
            background: #363670 url(Content/Images/grid-pgr.png) repeat-x top;
        }

            .Grid .pgr table {
                margin: 3px 0;
            }

            .Grid .pgr td {
                border-width: 0;
                padding: 0 6px;
                border-left: solid 1px #666;
                font-weight: bold;
                color: #fff;
                line-height: 12px;
            }

            .Grid .pgr a {
                color: Gray;
                text-decoration: none;
            }

                .Grid .pgr a:hover {
                    color: #000;
                    text-decoration: none;
                }
    </style>
    <div>
    <br /><br />
    <div class="col-4" style="text-align:left">

        <asp:Button ID="createBudget" class="btn btn-primary" runat="server" Text="Create New Budget" Style="text-align: center; cursor: pointer" UseSubmitBehavior="false" OnClick="CreateBudget_Click" ToolTip="Choose this button to create a new budget to manage" />        
    </div>
    <br />
    <div>
        <asp:GridView ID="gvBudget" CssClass="Grid" runat="server"
            AutoGenerateColumns="false"  AlternatingRowStyle-CssClass="alt" OnRowCommand="gvBudget_OnRowCommand" Width="80%" 
            OnRowDataBound="gvBudget_RowDataBound" >
            <Columns>
                <asp:BoundField DataField="BudgetID" HeaderText="BudgetID" />
                <asp:BoundField DataField="BudgetName" HeaderText="BudgetName" />
                <asp:BoundField DataField="TotalAmount" HeaderText="BudgetAmount" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" class="btn-info btn" OnClientClick="return confirm('Please confirm if you want to delete.')"  CausesValidation="false" CommandName="DeleteBudget" ToolTip="Choosing this button will permanently delete the budget"
                            Text="Delete Budget" CommandArgument='<%# Eval("BudgetID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnAddTransaction" runat="server" class="btn-info btn" CausesValidation="false" CommandName="AddTransaction" ToolTip="Choose this button to add a new transaction to the budget"
                            Text="Add Transaction" CommandArgument='<%# Eval("BudgetID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnViewSummary" runat="server" class="btn-info btn" CausesValidation="false" CommandName="ViewSummary" ToolTip="Choose this button to view the full summary of the budget"
                            Text="View Summary" CommandArgument='<%# Eval("BudgetID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnSelectBudget" runat="server" class="btn-info btn" CausesValidation="false" CommandName="SelectBudget" ToolTip="Choose this button to select the budget, so it can be viewed in your Dashboard"
                            Text="Select Budget" CommandArgument='<%# Eval("BudgetID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </div>

</asp:Content>
