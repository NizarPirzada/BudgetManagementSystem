using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineFinanicalApplicaton
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If the user isn't logged in, kick them back to the Login page. - E
            {

                if (Session["this_user"] == null)

                {

                    Response.Redirect("Login.aspx");

                }

            }
        }
    }
}