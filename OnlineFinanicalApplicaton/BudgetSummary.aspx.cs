using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;

namespace OnlineFinanicalApplicaton
{
    public partial class BudgetSummary : System.Web.UI.Page
    {
        /// <summary>
        /// Variables to be used publically
        /// </summary>
        #region Load Variables
        public DataTable transactionTable = null;
        public List<Notes> notesList = new List<Notes>();   // Initializing Notes List to be used at bottom
        public class Notes
        {
            public DateTime TransactionDate { get; set; }
            public string Note { get; set; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user is logged in, redirect them to the Dashboard so they can't access the Login page. - MS
            if (Session["this_user"] == null)

            {

                Response.Redirect("Login.aspx");

            }
            BindBudgetData();
            PercentageRemaining();
        }

        /// <summary>
        /// Seperate Function to Load Data 
        /// </summary>
        private void BindBudgetData()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Create the SelectCommand.
            SqlCommand command = new SqlCommand("select * from userbudgettransaction where budgetID=@budgetid", conn);
            // Add the parameters for the SelectCommand.
            command.Parameters.AddWithValue("@budgetid", Request.QueryString["bid"].ToString());
            adapter.SelectCommand = command;
            DataSet budgetData = new DataSet();
            adapter.Fill(budgetData);

            //lblNoData
            if (budgetData != null && budgetData.Tables.Count > 0 && budgetData.Tables[0].Rows.Count > 0)
            {
                gvBudget.DataSource = budgetData;
                gvBudget.DataBind();



                // Populating Total Row at bottom
                decimal totalIncome = 0;
                decimal totalExpense = 0;
                DataTable dt = budgetData.Tables[0];
                ProcessNotes(budgetData);

                gvBudget.FooterRow.Cells[0].Text = "Total";
                gvBudget.FooterRow.Cells[1].Font.Bold = true;
                gvBudget.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                for (int k = 1; k < dt.Columns.Count - 1; k++)
                {
                    if (dt.Columns[k].ToString() == "TransactionAmount")
                    {
                        totalIncome = dt.AsEnumerable().Where(row => row.Field<int>("TransactionType") == 1).Sum(row =>
                        row.Field<decimal>(dt.Columns[k].ToString())
                        );

                        totalExpense = dt.AsEnumerable().Where(row => row.Field<int>("TransactionType") == 2).Sum(row =>
                        row.Field<decimal>(dt.Columns[k].ToString())
                        );

                        gvBudget.FooterRow.Cells[k - 1].Text = (totalIncome - totalExpense).ToString();
                        gvBudget.FooterRow.Cells[k - 1].Font.Bold = true;
                        gvBudget.FooterRow.BackColor = System.Drawing.Color.Beige;
                    }
                }
            }
            else
            {
                lblNoData.Visible = true;
            }

        }
        /// <summary>
        /// Here db data is process and only notes info is added to object used in calender day render method
        /// </summary>
        /// <param name="budgetData"></param>
        private void ProcessNotes(DataSet budgetData)
        {
            foreach (DataRow row in budgetData.Tables[0].Rows)
            {
                Notes notes = new Notes();
                notes.Note = row["Note"].ToString();
                notes.TransactionDate = Convert.ToDateTime(row["TransactionDate"].ToString());
                if (notes.Note.Trim().Length > 0)
                    notesList.Add(notes);
            }

        }
        /// <summary>
        /// Map Transaction Type against Transaction Id    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int trType = Convert.ToInt32(e.Row.Cells[3].Text);
                if (trType == 2)
                    e.Row.Cells[3].Text = "Expense";
                else
                    e.Row.Cells[3].Text = "Income";
            }

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

        protected void CreateBudget_Click(object sender, EventArgs e)

        {
            Response.Redirect("~/CreateBudget.aspx");

        }

        private Random rnd = new Random();  // random number generator
        // This method is used to prevent a user from selecting a date in the past. -LS
        protected void StartDate_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime pastday = e.Day.Date;
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            DateTime today = new DateTime(year, month, day);
            e.Day.IsSelectable = false;
            Notes tmpNotes = notesList.Where(x => x.TransactionDate == e.Day.Date).FirstOrDefault();    //checking if date has a note            
            if (tmpNotes != null)
            {
                e.Cell.Text = tmpNotes.Note;
                e.Cell.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)); //Applying Beautifull Random Colors

            }
        }

        private void PercentageRemaining()
        {
         //   SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
            // Create the SelectCommand
         //   SqlCommand percentage = new SqlCommand("SELECT SUM(TransactionAmount) / BudgetAmount as Percentage FROM UserBudget JOIN UserBudgetTransaction ON UserBudget.BudgetID = UserBudgetTransaction.BudgetID WHERE UserBudget.BudgetID = @budgetid AND TransactionType = 2 group by UserBudget.BudgetID, BudgetAmount", conn);
         //   percentage.Parameters.AddWithValue("@budgetid", Request.QueryString["bid"].ToString());
          //  conn.Open();
          //  decimal Percent = (decimal)percentage.ExecuteScalar();
          //  PercentBar.Text = Percent.ToString();

            var sql = @"SELECT (SUM(TransactionAmount) / BudgetAmount) * 100 as Percentage 
             FROM UserBudget JOIN UserBudgetTransaction ON UserBudget.BudgetID = UserBudgetTransaction.BudgetID
            WHERE UserBudget.BudgetID = @budgetid AND TransactionType = 2
            group by UserBudget.BudgetID, BudgetAmount";

            decimal result;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@budgetid", Request.QueryString["bid"].ToString());
                    conn.Open();

                    result = (decimal)cmd.ExecuteScalar();
                }
            }

            PercentBar.Text = result.ToString();

        }

    }
}