/********************************************************************
 * Login.aspx.cs                                         v1.0 11/2014
 * Sacred Heart Hospital                                Robert Willis
 *
 * Code Behind File for Login.aspx.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WDAssignment2.Utility;

namespace WDAssignment2
{
    public partial class Login : System.Web.UI.Page
    {
        // Error message is hidden on page load
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
        }

        // Attempt to log user in, show error message if fail
        protected void LoginClick(object sender, EventArgs e)
        {
            // Get list of users from stored procedure
            List<User> users = UserUtility.GetUsers();
            User found = null;

            // Check each user in list against input
            foreach (User user in users)
            {
                if (user.username == Username.Text)
                    if (user.password == Password.Text)
                        found = user;
            }

            // If user is found set gloabl user and
            // redirect to sitemap
            if (found != null)
            {
                Session[Global.user] = found;
                Response.Redirect("Sitemap.aspx");
            }
            // Show error message if user not found
            else
                ErrorMessage.Visible = true;

        }

    }
}