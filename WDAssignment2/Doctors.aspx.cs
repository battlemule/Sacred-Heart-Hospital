/********************************************************************
 * Doctors.aspx.cs                                       v1.0 11/2014
 * Sacred Heart Hospital                                Robert Willis
 *
 * Code Behind File for Doctors.aspx.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using WDAssignment2.Utility;

namespace WDAssignment2
{
    public partial class Doctors : System.Web.UI.Page
    {
        // On page load redirect if not logged in else
        // load data into gridview
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");
            else
            {
                List<Doctor> doctors = DoctorUtility.GetDoctors();
                GridView1.DataSource = doctors;
                GridView1.DataBind();
            }
        }
    }
}