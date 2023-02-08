using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using OnlineFinanicalApplicaton.Core;

namespace OnlineFinanicalApplicaton
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user is logged in, redirect them to the Dashboard so they can't access the Login page. - MS
            if (Session["this_user"] != null)

            {

                Response.Redirect("Home.aspx");

            }

            invalidUser.Visible = false;
        }

        protected void LoginSubmit_Click(object sender, EventArgs e)
        {
            string Email = emailInput.Text;
            string Password = passwordInput.Text;
            string userId = "0";

            UserProcess userProcess = new UserProcess();
            int userStatus = userProcess.LoginUser(ConfigurationManager.ConnectionStrings["dbconnect"].ConnectionString,Email, Password, out userId);
            if (userStatus == 1)
            {
                Session["this_user"] = emailInput.Text;
                Session["userId"] = userId;
                Response.Redirect("Budgets.aspx");
            }
            else
                invalidUser.Visible = true;            
        }

    }

}