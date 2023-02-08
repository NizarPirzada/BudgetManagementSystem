using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineFinanicalApplicaton.Core
{
    public class BudgetProcess
    {

        public int CreateBudget(string connString, string budgetName, string budgetAmount, string budgetDescription, string categoryId, string userEmail, string userId, DateTime selectedDate, out string budgetId, out string message)
        {
            message = "";
            budgetId = "0";
            try
            {
                if (selectedDate.Date >= DateTime.Now.Date)
                {
                    SqlConnection conn = new SqlConnection(connString);
                    conn.Open();

                    string Insert = @"INSERT INTO dbo.UserBudget (BudgetName, BudgetAmount, Description, StartDate, Email,CategoryId,UserId) VALUES (@bname, @bamount, @bdescription, @bstartdate, @user,@categoryid,@userid);" +
                        " SELECT @@Identity;";

                    SqlCommand cmd = new SqlCommand(Insert, conn);
                    cmd.Parameters.AddWithValue("@bname", budgetName);
                    cmd.Parameters.AddWithValue("@bamount", budgetAmount);
                    cmd.Parameters.AddWithValue("@bdescription", budgetDescription);
                    cmd.Parameters.AddWithValue("@bstartdate", selectedDate);
                    cmd.Parameters.AddWithValue("@user", userEmail);
                    cmd.Parameters.AddWithValue("@categoryid", categoryId);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    budgetId  = Convert.ToString(cmd.ExecuteScalar());
                    conn.Close();
                    return 1;
                }
                else
                {
                    message = "Select Valid Date";
                    return 2;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return 3;
            }            
        }        

    }
}