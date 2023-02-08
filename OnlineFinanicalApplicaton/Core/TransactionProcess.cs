using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineFinanicalApplicaton.Core
{
    public class TransactionProcess
    {
        public int CreateTransaction(string connString, string transactionDescription, string transactionAmount, string transactionType, string budgetId, 
                                    string transactionNote, string categoryId, DateTime transactionDate, out string message)
        {
            message = "";
            try
            {
                if (transactionDate.Date >= DateTime.Now.Date)
                {
                    SqlConnection conn = new SqlConnection(connString);
                    conn.Open();

                    string Insert = "INSERT INTO dbo.UserBudgetTransaction (TransactionDescription, TransactionDate, TransactionAmount, TransactionType, Note,BudgetID,CategoryID) " +
                        "VALUES (@tdesc, @tdate, @tamount, @ttype, @note,@budgetid,@categoryId)";

                    SqlCommand cmd = new SqlCommand(Insert, conn);
                    cmd.Parameters.AddWithValue("@tdesc", transactionDescription);
                    cmd.Parameters.AddWithValue("@tdate", transactionDate);
                    cmd.Parameters.AddWithValue("@tamount", transactionAmount);
                    cmd.Parameters.AddWithValue("@ttype", transactionType);
                    cmd.Parameters.AddWithValue("@note", transactionNote);   // Default no Note
                    cmd.Parameters.AddWithValue("@budgetId", budgetId);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                else
                {
                    message = "Please select Valid Date";
                    return 2;
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
                return 3;
            }
            
        }
    }
}