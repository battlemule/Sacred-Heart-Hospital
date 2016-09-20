/********************************************************************
 * Doctors.aspx.cs                                       v1.2 09/2016
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
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide database error on page load
            DataBaseError.Visible = false;

            // Hide gridview on page load
            DoctorGridView.Enabled = false;

            // Redirect to login page if not logged in
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");

            // If logged in attempt to pull data from database
            try
            {
                // Bind doctor data to gridview
                List<Doctor> doctors = DoctorUtility.GetDoctors();
                DoctorGridView.DataSource = doctors;
                DoctorGridView.DataBind();
            }
            // If exception caught show database error
            // and hide gridview
            catch (Exception)
            {
                DataBaseError.Visible = true;
                DoctorGridView.Enabled = false;
            }

        }
    }
}