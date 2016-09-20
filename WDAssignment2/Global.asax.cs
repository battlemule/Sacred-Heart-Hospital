/********************************************************************
 * Global.asax.cs                                        v1.2 09/2016
 * Sacred Heart Hospital                                Robert Willis
 *
 * Global file for Sacred Heart Hospital system.
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WDAssignment2
{
    public class Global : System.Web.HttpApplication
    {
        public static readonly string user = null;
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}