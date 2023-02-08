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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["this_user"] != null)

            {

                Response.Redirect("Home.aspx");

            }

            userNoExist.Visible = false;
        }

        // Back button return to Login page - CR
        protected void BackToLoginButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void loginSubmit_Click(object sender, EventArgs e)

        {
            string Forename = fName.Text;
            string Surname = lName.Text;
            string Email = emailInput.Text;
            string Password = passwordInput.Text;
            string CheckPassword = passwordCheck.Text;
            string message = "";
            string userId = "";

            UserProcess userProcess = new UserProcess();
            int userStatus = userProcess.CreateUser(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString,Forename, Surname, Email, Password, out message,out userId);
            if (userStatus == 1)
            {
                Session["this_user"] = emailInput.Text;
                Session["userId"] = userId;
                Response.Redirect("CreateBudget.aspx");
            }
            else
                userNoExist.Visible = true;


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString);
            conn.Open();
            string Insert = "INSERT INTO dbo.Users (Forename, Surname, Email, Password) VALUES (@fname, @lname, @email, @password)";

            //This query is for checking if the e-mail exists on the database.
            SqlCommand cmd_user_exists = new SqlCommand(@"SELECT * FROM dbo.Users WHERE Email = @email", conn);

            SqlCommand cmd = new SqlCommand(Insert, conn);
            cmd.Parameters.AddWithValue("@fname", fName.Text);
            cmd.Parameters.AddWithValue("@lname", lName.Text);
            cmd.Parameters.AddWithValue("@email", emailInput.Text);
            cmd.Parameters.AddWithValue("@password", passwordInput.Text);
            cmd.ExecuteNonQuery();

            cmd_user_exists.Parameters.AddWithValue("@email", emailInput.Text);

            SqlDataReader reader_user_exists = cmd_user_exists.ExecuteReader();

            //// If there returns values that a user exists in the database, view some text and end code. - CR
            if (reader_user_exists.HasRows)

            {
                reader_user_exists.Close();
               SqlDataReader reader = cmd.ExecuteReader();

            //    //Set the session variable and redirect the user to the homepage.
                Session["this_user"] = emailInput.Text;
                Response.Redirect("Home.aspx");
                reader_user_exists.Close();

                conn.Close();


            }
            //// If there ISNT any values returned from the "User exists" database SQL query, then run the rest of this code - CR
            else

            {
                userNoExist.Visible = true;
                reader_user_exists.Close();

            }

        }
    }
}