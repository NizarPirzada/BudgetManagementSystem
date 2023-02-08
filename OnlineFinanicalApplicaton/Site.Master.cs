using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineFinanicalApplicaton
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userid"] == null)
            {
                dvLogin.Visible = false;
            }
            else
                dvLogin.Visible = true;
        }

        protected void Logout_Click(object sender, EventArgs e)

        {
            //Destroy the session variable and redirect the user to the homepage.
            Session.Abandon();
            Response.Redirect("Login.aspx");

        }

    }
}