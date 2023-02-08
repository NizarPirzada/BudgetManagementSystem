using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

namespace OnlineFinanicalApplicaton
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user is logged in, redirect them to the Dashboard so they can't access the Login page. - MS
            if (Session["this_user"] == null)

            {

                Response.Redirect("Login.aspx");

            }


            //if (Session["this_budget"] == null)

            //{

            //    Response.Redirect("CreateBudget.aspx");

            //}

            if (Session["budgetId"] == null)

            {

                Response.Redirect("Budgets.aspx");

            }

            if (!IsPostBack)
            {
                GetChartTypes();
                GetChartData();
                GetChartData2();
                BindBudgetData();
                ChartDisplay();
            }

        }

        private void GetChartTypes()
        {
            foreach (int chartType in Enum.GetValues(typeof(SeriesChartType)))
            {
                ListItem li = new ListItem(Enum.GetName(typeof(SeriesChartType), chartType), chartType.ToString());
                DropDownList1.Items.Add(li);
                DropDownList2.Items.Add(li);
            }
        }


        private void GetChartData()
        {
            SqlDataAdapter adapterChartExpense = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
            SqlCommand ChartExpenseCommand = new SqlCommand("select CategoryID, SUM(TransactionAmount) AS TransactionAmount from userbudgettransaction where budgetID=@budgetid AND TransactionType = 2 group by CategoryID", conn);
            Series series = Chart1.Series["Series1"];
            ChartExpenseCommand.Parameters.AddWithValue("@budgetid", Session["budgetId"].ToString());
            adapterChartExpense.SelectCommand = ChartExpenseCommand;
            DataSet chartExpenseData = new DataSet();
            adapterChartExpense.Fill(chartExpenseData);
            conn.Open();
            SqlDataReader readExpense = ChartExpenseCommand.ExecuteReader();
            while (readExpense.Read())
            {

                series.Points.AddXY(readExpense["CategoryID"].ToString(), readExpense["TransactionAmount"]);
            }
            gvCategoryAmount.DataSource = chartExpenseData;
            gvCategoryAmount.DataBind();
        }

        private void GetChartData2()
        {
            SqlDataAdapter adapterChartExpense = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
            SqlCommand ChartExpenseCommand1 = new SqlCommand("select CategoryID, SUM(TransactionAmount) AS TransactionAmount from userbudgettransaction where budgetID=@budgetid AND TransactionType = 1 group by CategoryID", conn);
            Series series1 = Chart2.Series["Series2"];
            ChartExpenseCommand1.Parameters.AddWithValue("@budgetid", Session["budgetId"].ToString());
            adapterChartExpense.SelectCommand = ChartExpenseCommand1;
            DataSet chartExpenseData = new DataSet();
            adapterChartExpense.Fill(chartExpenseData);
            conn.Open();
            SqlDataReader readExpense = ChartExpenseCommand1.ExecuteReader();
            while (readExpense.Read())
            {

                series1.Points.AddXY(readExpense["CategoryID"].ToString(), readExpense["TransactionAmount"]);
            }
            GridView1.DataSource = chartExpenseData;
            GridView1.DataBind();
        }

        private void BindBudgetData()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Create the SelectCommand.
            SqlCommand command = new SqlCommand(@"select *,
                ub.BudgetAmount + isnull((select sum( case when ubt.TransactionType = 1 then ubt.TransactionAmount else -ubt.TransactionAmount end)
                from UserBudgetTransaction ubt where ubt.BudgetID = ub.BudgetID), 0) as TotalAmount
                from userbudget ub where ub.UserID = @userid and ub.IsDefault=1", conn);

            // Add the parameters for the SelectCommand.
            command.Parameters.AddWithValue("@userid", Session["userid"].ToString());

            adapter.SelectCommand = command;
            DataSet budgetData = new DataSet();
            adapter.Fill(budgetData);

            if (budgetData != null && budgetData.Tables.Count > 0 && budgetData.Tables[0].Rows.Count > 0)
            {
                lblBudgetName.Text = budgetData.Tables[0].Rows[0]["BudgetName"].ToString();
                lblTotalAmount.Text = budgetData.Tables[0].Rows[0]["TotalAmount"].ToString();
                lblDescription.Text = budgetData.Tables[0].Rows[0]["Description"].ToString();

                Session["budgetId"] = budgetData.Tables[0].Rows[0]["BudgetID"].ToString();
            }
            else
            {
                mainDiv.Visible = false;
            }
            //gvBudget.DataSource = budgetData;
            //gvBudget.DataBind();
        }

        protected void gvBudget_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteBudget")
            {
                int budgetId = Convert.ToInt32(e.CommandArgument);
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
                conn.Open();
                string Delete = "DELETE FROM dbo.UserBudget WHERE BudgetID = @budgetid";
                SqlCommand cmd = new SqlCommand(Delete, conn);
                cmd.Parameters.AddWithValue("@budgetid", budgetId);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindBudgetData();
            }
            else if (e.CommandName == "AddTransaction")
            {
                int budgetId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("CreateTransaction.aspx?bid=" + budgetId);
            }
            else if (e.CommandName == "ViewSummary")
            {
                int budgetId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("BudgetSummary.aspx?bid=" + budgetId);
            }
        }

        protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int cellNumber = 0;
                int trType = Convert.ToInt32(e.Row.Cells[cellNumber].Text);
                if (trType == 1)
                    e.Row.Cells[cellNumber].Text = "Housing = 1";
                if (trType == 2)
                    e.Row.Cells[cellNumber].Text = "Consumer Debt = 2";
                if (trType == 3)
                    e.Row.Cells[cellNumber].Text = "Food and Groceries = 3";
                if (trType == 4)
                    e.Row.Cells[cellNumber].Text = "Entertainment = 4";
                if (trType == 5)
                    e.Row.Cells[cellNumber].Text = "Clothing = 5";
                if (trType == 6)
                    e.Row.Cells[cellNumber].Text = "Transport = 6";
                if (trType == 7)
                    e.Row.Cells[cellNumber].Text = "Other = 7";
            }
        }

        protected void btnAddTransaction_Click(object sender, EventArgs e)
        {
            int budgetId = Convert.ToInt32(Session["budgetId"]);
            Response.Redirect("CreateTransaction.aspx?bid=" + budgetId);
        }

        protected void btnViewSummary_Click(object sender, EventArgs e)
        {
            int budgetId = Convert.ToInt32(Session["budgetId"]);
            Response.Redirect("BudgetSummary.aspx?bid=" + budgetId);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChartData();
            Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), DropDownList1.SelectedValue);
            GetChartData2();
            DropDownList2_SelectedIndexChanged();

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChartData2();
            Chart2.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), DropDownList2.SelectedValue);
            GetChartData();
            DropDownList1_SelectedIndexChanged();
        }

        private void DropDownList1_SelectedIndexChanged()
        {
            Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), DropDownList1.SelectedValue);
        }

        private void DropDownList2_SelectedIndexChanged()
        {
            Chart2.Series["Series2"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), DropDownList2.SelectedValue);
        }

        private void ChartDisplay()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Create the SelectCommand.
            SqlCommand command = new SqlCommand("select * from userbudgettransaction where budgetID=@budgetid AND TransactionType = 2", conn);
            // Add the parameters for the SelectCommand.
            command.Parameters.AddWithValue("@budgetid", Session["budgetId"].ToString());
            adapter.SelectCommand = command;
            DataSet budgetData = new DataSet();
            adapter.Fill(budgetData);

            SqlDataAdapter adapter1 = new SqlDataAdapter();
            // Create the SelectCommand.
            SqlCommand command1 = new SqlCommand("select * from userbudgettransaction where budgetID=@budgetid AND TransactionType = 1", conn);
            // Add the parameters for the SelectCommand.
            command1.Parameters.AddWithValue("@budgetid", Session["budgetId"].ToString());
            adapter1.SelectCommand = command1;
            DataSet budgetData1 = new DataSet();
            adapter1.Fill(budgetData1);


            if (budgetData.Tables[0].Rows.Count < 1)
            {
                Chart1.Visible = false;
                DropDownList1.Visible = false;
                lblNoData.Visible = true;
            }

            if (budgetData1.Tables[0].Rows.Count < 1)
            {
                Chart2.Visible = false;
                DropDownList2.Visible = false;
                lblNoData2.Visible = true;
            }
        }
    }
}