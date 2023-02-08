using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace OnlineFinanicalApplicaton
{
    public partial class Budgets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user is logged in, redirect them to the Dashboard so they can't access the Login page. - MS
            if (Session["this_user"] == null)
            {
                Response.Redirect("Login.aspx");
            }            
            if (!IsPostBack)
            {
                BindBudgetData();
            }
        }
        /// <summary>
        /// Getting Data from Db and Binding in Grid
        /// </summary>
        private void BindBudgetData()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Create the SelectCommand.
            SqlCommand command = new SqlCommand(@"select *,
                ub.BudgetAmount + isnull((select sum( case when ubt.TransactionType = 1 then ubt.TransactionAmount else -ubt.TransactionAmount end)
                from UserBudgetTransaction ubt where ubt.BudgetID = ub.BudgetID),0) as TotalAmount
                from userbudget ub where ub.UserID = @userid", conn);
            // Add the parameters for the SelectCommand.
            command.Parameters.AddWithValue("@userid", Session["userid"].ToString());
            adapter.SelectCommand = command;
            DataSet budgetData = new DataSet();
            adapter.Fill(budgetData);
          
            gvBudget.DataSource = budgetData;
            gvBudget.DataBind();
        }

        protected void CreateBudget_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateBudget.aspx");

        }

        protected void gvBudget_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Process for delete budget
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
            //Process for Add Transaction
            else if (e.CommandName == "AddTransaction")
            {
                int budgetId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("CreateTransaction.aspx?bid=" + budgetId);
            }
            //Process for View Summary
            else if (e.CommandName == "ViewSummary")
            {
                int budgetId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("BudgetSummary.aspx?bid=" + budgetId);
            }
            else if (e.CommandName == "SelectBudget")
            {
                int budgetId = Convert.ToInt32(e.CommandArgument);

                //Db Operation to mark default budget                
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
                conn.Open();
                string Delete = @"update UserBudget
                            set IsDefault = 0;
                            update UserBudget
                            set IsDefault = 1
                            where budgetid = @budgetid;";
                SqlCommand cmd = new SqlCommand(Delete, conn);
                cmd.Parameters.AddWithValue("@budgetid", budgetId);
                Session["budgetId"] = budgetId;
                cmd.ExecuteNonQuery();
                conn.Close();
                BindBudgetData();
            }

        }
        protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int cellNumber = 4;
                int trType = Convert.ToInt32(e.Row.Cells[cellNumber].Text);
                if (trType == 1)
                    e.Row.Cells[cellNumber].Text = "Housing";
                if (trType == 2)
                    e.Row.Cells[cellNumber].Text = "Consumer Debt";
                if (trType == 3)
                    e.Row.Cells[cellNumber].Text = "Food and Groceries";
                if (trType == 4)
                    e.Row.Cells[cellNumber].Text = "Entertainment";
                if (trType == 5)
                    e.Row.Cells[cellNumber].Text = "Clothing";
                if (trType == 6)
                    e.Row.Cells[cellNumber].Text = "Transport";
                if (trType == 7)
                    e.Row.Cells[cellNumber].Text = "Other";                
            }
        }

    }
}