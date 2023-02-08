using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using OnlineFinanicalApplicaton.Core;

namespace OnlineFinanicalApplicaton
{
    public partial class CreateBudget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            calNotSelected.Visible = false;

            if (Session["this_user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void CreateBudgetButton_Click(object sender, EventArgs e)
        {
            string UserId = Session["userid"].ToString();
            string UserEmail = Session["this_user"].ToString();            
            string BudgetName = txtBudgetName.Text;
            string BudgetAmount = txtBalanace.Text;
            string BudgetDescription = txtDescription.Text;
            string CategoryId = drpCategory.SelectedValue;
            DateTime SelectedDate = startDate.SelectedDate;
            string message = "";
            string BudgetId = "0";

            BudgetProcess budgetProcess = new BudgetProcess();
            int budgetStatus = budgetProcess.CreateBudget(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString,BudgetName,
                BudgetAmount,BudgetDescription,CategoryId,UserEmail,UserId,SelectedDate, out BudgetId,out message);
            
            if(budgetStatus == 1)
            {
                Session["budgetId"] = BudgetId;
                Response.Redirect("Budgets.aspx");
            }
            else if (budgetStatus == 2)
            {
                calNotSelected.Visible = true;
            }
           

            //DateTime todaysDate = DateTime.Now;
            //if (startDate.SelectedDate >= todaysDate.Date)
            //{

                //    
                //    conn.Open();

                //    //Third SQL command to find the User ID of the logged in user - CR
                //    SqlCommand cmdPull2 = new SqlCommand(@"SELECT * FROM dbo.Users WHERE Email = @user", conn);
                //    cmdPull2.Parameters.AddWithValue("@user", (Session["this_user"].ToString()));
                //    SqlDataReader readerPull2 = cmdPull2.ExecuteReader();
                //    int userID = 0;
                //    string emailTemp = "";
                //    while (readerPull2.Read())
                //    {
                //        string idTemp2 = readerPull2["User_ID"].ToString();
                //        emailTemp = readerPull2["Email"].ToString();
                //        userID = Int32.Parse(idTemp2);
                //    }
                //    readerPull2.Close();

                //    string BudgetName = txtBudgetName.Text;
                //    string BudgetAmount = txtBalanace.Text;
                //    string BudgetDescription = txtDescription.Text;
                //    string CategoryId = drpCategory.SelectedValue;

                //    string Insert = "INSERT INTO dbo.UserBudget (BudgetName, BudgetAmount, Description, StartDate, Email,CategoryId,UserId) VALUES (@bname, @bamount, @bdescription, @bstartdate, @user,@categoryid,@userid)";

                //    SqlCommand cmd = new SqlCommand(Insert, conn);
                //    cmd.Parameters.AddWithValue("@bname", BudgetName);
                //    cmd.Parameters.AddWithValue("@bamount", BudgetAmount);
                //    cmd.Parameters.AddWithValue("@bdescription", BudgetDescription);
                //    cmd.Parameters.AddWithValue("@bstartdate", startDate.SelectedDate);
                //    cmd.Parameters.AddWithValue("@user", Session["this_user"].ToString());
                //    cmd.Parameters.AddWithValue("@categoryid", CategoryId);
                //    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                //    cmd.ExecuteNonQuery();
                //    conn.Close();
                //    Session["this_budget"] = userID;
                //    Response.Redirect("Dashboard.aspx");
                //}
                //else
                //{
                //    calNotSelected.Visible = true;
                //}

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