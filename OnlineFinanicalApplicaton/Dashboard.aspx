<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="OnlineFinanicalApplicaton.Dashboard" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/custom/login.css" rel="stylesheet" />
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
            background: #363670 url(Images/grid-header.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 0.9em;
        }

        .Grid .alt {
            background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
        }

        .Grid .pgr {
            background: #363670 url(Images/grid-pgr.png) repeat-x top;
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


        div.chart {
            float: left;
            width: 33.33%;
            padding: 10px;
            width: 350px;
            border: #eee;
            background-color: #eee;
            align-items: center;
            text-align: center;
            padding-right: 15px;
            padding-left: 15px;
            border-radius: 6px;
        }

        div.chartincome {
            float: right;
            width: 33.33%;
            padding: 10px;
            width: 350px;
            border: #eee;
            background-color: #eee;
            align-items: center;
            text-align: center;
            padding-right: 15px;
            padding-left: 15px;
            border-radius: 6px;
        }

        .jumbotron {
            float: none;
            width: 33.33%;
            padding: 10px;
            padding-top: 30px;
            padding-bottom: 30px;
            margin-bottom: 30px;
            color: inherit;
            background-color: #eee;
        }

        @media screen and (max-width: 600px) {
            .chart {
                width: 100%;
            }

            .chart1 {
                width: 100%;
            }
            .jumbotron{
                width: 100%;
            }
        }

    </style>
        <br />
        <br />
        <div class="col-4" style="text-align: left">

            <%--<asp:Button ID="deleteBudget" class="btn btn-primary" runat="server" Text="Delete Budget" Style="text-align: center; cursor: pointer" UseSubmitBehavior="false" OnClick="DeleteBudget_Click" />--%>
        </div>
        <br />

        <div class="chart" style="text-align: center"  align-items:"center">
            <asp:Label runat="server" style="font-weight:bold;color:red" ID="lblNoData" Visible="false" Text="There are No Expense Transactions in this Budget."></asp:Label>
            <h1 style="font-weight: bold;">Top spending category</h1>
            <br />

            <tr>
                <td>
                    <b>Select Chart Type:</b>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" ToolTip="Select what type of graph you would like, to visually display the top spending categories within the current budget"></asp:DropDownList>
                </td>
            </tr>

            <asp:Chart ID="Chart1" runat="server" BackColor="#eee" Style="max-width: 350px !important" OnRowDataBound="gvBudget_RowDataBound">
                <Titles>
                    <asp:Title Text="Total spending within each category">
                    </asp:Title>
                </Titles>
                <Series>
                    <asp:Series Name="Series1">
                        <Points>
                        </Points>
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX Title="Category">
                        </AxisX>
                        <AxisY Title="Total spending within category">
                        </AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>

            <asp:GridView ID="gvCategoryAmount" CssClass="Grid" runat="server" Style="text-align: center" HorizontalAlign="Center"
                AutoGenerateColumns="false"
                OnRowDataBound="gvBudget_RowDataBound"
                Width="80%" ShowFooter="true">
                <Columns>
                    <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" />
                    <asp:BoundField DataField="TransactionAmount" HeaderText="Total Spending" />
                </Columns>

            </asp:GridView>

        </div>

            <div class="chartincome" style="text-align: center"  align-items:"center">
                <asp:Label runat="server" style="font-weight:bold;color:red" ID="lblNoData2" Visible="false" Text="There are No Income Transactions in this Budget."></asp:Label>
                <h1 style="font-weight: bold;">Top income category</h1>
                <br />

                <tr>
                    <td>
                        <b>Select Chart Type:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" ToolTip="Select what type of graph you would like, to visually display the categories with the highest income within the current budget"></asp:DropDownList>
                    </td>
                </tr>

                <asp:Chart ID="Chart2" runat="server" BackColor="#eee" Style="max-width: 350px !important" OnRowDataBound="gvBudget_RowDataBound">
                    <Titles>
                        <asp:Title Text="Total income within each category">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series2">
                            <Points>
                            </Points>
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea2">
                            <AxisX Title="Category">
                            </AxisX>
                            <AxisY Title="Total income within category">
                            </AxisY>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>

                <asp:GridView ID="GridView1" CssClass="Grid" runat="server" Style="text-align: center" HorizontalAlign="Center"
                    AutoGenerateColumns="false"
                    OnRowDataBound="gvBudget_RowDataBound"
                    Width="80%" ShowFooter="true">
                    <Columns>
                        <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" />
                        <asp:BoundField DataField="TransactionAmount" HeaderText="Total income" />
                    </Columns>

                </asp:GridView>

            </div>

            <div class="jumbotron" style="max-width: 400px !important" runat="server" id="mainDiv">
                <div style="width: 100%; align-items: center; margin: 0 auto; text-align: center;">
                    <div>
                        <h1>Budget Details</h1>
                    </div>
                    <br />

                    <br />
                    <div>

                        <div>
                            <h2>
                                <asp:Label runat="server" ID="lblBudgetName" Text=""></asp:Label></h2>
                        </div>

                    </div>
                    <br /><br />
                    <div>

                        <div style="display:inline-block">
                            <p>Balance:</p>
                        </div>


                        <div style="display:inline-block;font-weight:700;padding-left:30px">
                            <asp:Label runat="server" ID="lblTotalAmount" Text=""></asp:Label>
                        </div>

                    </div>
                    <div>

                        <div style="display:inline-block">
                            <p>Description:</p>
                        </div>


                        <div style="display:inline-block;padding-left:30px">
                            <asp:Label runat="server" ID="lblDescription" Text=""></asp:Label>
                        </div>

                    </div>
                    
                </div>
                <br /><br /><br />
                <div>
                    <div style="display:inline-block">
                        <asp:Button ID="btnAddTransaction" runat="server" class="btn-info btn" CausesValidation="false" CommandName="AddTransaction"
                            Text="Add Transaction" OnClick="btnAddTransaction_Click" ToolTip="Clicking this button will redirect you to another page where you can create a new transaction within the current budget" />
                    </div>
                    <div style="display:inline-block">
                        <asp:Button ID="btnViewSummary" runat="server" class="btn-info btn" CausesValidation="false" CommandName="ViewSummary"
                            Text="View Summary" OnClick="btnViewSummary_Click" ToolTip="Clicking this button will allow you to view a full summary of the current budget" />
                    </div>
                </div>

            </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

</asp:Content>
