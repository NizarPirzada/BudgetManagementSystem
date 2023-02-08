using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using OnlineFinanicalApplicaton.Core;

namespace OnlineFinanicalApplicaton
{
    public partial class CreateTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            calNotSelected.Visible = false;
            if (Session["this_user"] == null)
            {
                Response.Redirect("Login.aspx");

            }
        }

        protected void CreateTransactionButton_Click(object sender, EventArgs e)
        {
            string TransactionDescription = txtDescription.Text;
            string TransactionAmount = txtAmount.Text;
            string TransactionType = rdlTransactionType.SelectedValue;
            DateTime TransactionDate = TransactionStartDate.SelectedDate;
            string TransactionNote = TransactionNotes.Text;
            string CategoryId = drpCategory.SelectedValue;
            string BudgetId = Request.QueryString["bid"].ToString();
            string message = "";

            TransactionProcess transactionProcess = new TransactionProcess();
            int transactionStatus =  transactionProcess.CreateTransaction(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString, TransactionDescription, TransactionAmount,
                TransactionType, BudgetId, TransactionNote, CategoryId, TransactionDate, out message);

            if (transactionStatus == 1)
            {
                Response.Redirect("Dashboard.aspx");
            }
            else if (transactionStatus == 2)
            {
                calNotSelected.Visible = true;
            }            
        }

        // This back button will redirect you back to the Dashboard.aspx page. - LS
        protected void BackButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("Dashboard.aspx");
        }

        // This method is used to prevent a user from selecting a date in the past. -LS
        protected void StartDate_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime pastday = e.Day.Date;
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            DateTime today = new DateTime(year, month, day);
            if (pastday.CompareTo(today) < 0)
            {
                e.Cell.BackColor = System.Drawing.Color.Gray;
                e.Day.IsSelectable = false;
            }
        }

      
    }



}