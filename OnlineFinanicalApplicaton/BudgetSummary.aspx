<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BudgetSummary.aspx.cs" Inherits="OnlineFinanicalApplicaton.BudgetSummary" %>

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
            padding: 4px 2px;
            color: #fff;
            background: #363670 url(Content/Images/grid-header.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 0.9em;
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
        <br />
        <br />
        <h2 style="font-weight: bold;">Budget Summary</h2>
        <asp:Label runat="server" ID="PercentBar" Text=""></asp:Label>
        <br />
        <br />
        <div style="text-align:center">
            <asp:Label runat="server" style="font-weight:bold;color:red" ID="lblNoData" Visible="false" Text="There are No Transactions in this Budget."></asp:Label>
            <br />
            <br />

        </div>
        <div>
            <div>
                <asp:Calendar Width="1000px" Height="500px" class="cal" ID="TransactionStartDate" runat="server" OnDayRender="StartDate_DayRender">
                    <SelectedDayStyle BackColor="DodgerBlue" Font-Bold="True" ForeColor="White" />

                </asp:Calendar>
            </div>
        </div>
        <br />

        <br />
        <br />
        <div>
            <asp:GridView ID="gvBudget" CssClass="Grid" runat="server"
                AutoGenerateColumns="false"
                OnRowDataBound="gvBudget_RowDataBound"
                Width="80%" ShowFooter="true">
                <Columns>
                    <asp:BoundField DataField="UserBudgetTransactionID" HeaderText="UserBudgetTransactionID" />
                    <asp:BoundField DataField="TransactionDescription" HeaderText="TransactionDescription" />
                    <asp:BoundField DataField="TransactionAmount" HeaderText="TransactionAmount" />
                    <asp:BoundField DataField="TransactionType" HeaderText="TransactionType" />
                    <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" />
                    <asp:BoundField DataField="Note" HeaderText="Note" />
                </Columns>

            </asp:GridView>
        </div>
    </div>

</asp:Content>
