/********************************************************************
 * Beds.aspx.cs                                          v1.2 09/2016
 * Sacred Heart Hospital                                Robert Willis
 *
 * Code Behind File for Beds.aspx.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SacredHeartHospital.Utility;

namespace SacredHeartHospital
{
    public partial class Beds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide database error on page load
            DataBaseError.Visible = false;

            // Hide gridview on page load
            BedGridView.Enabled = false;

            // Redirect to login page if not logged in
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");

            // If logged in attempt to pull data from database
            try
            {
                // Bind bed data to gridview
                List<Bed> beds = BedUtility.GetBeds();
                BedGridView.Enabled = true;
                BedGridView.DataSource = beds;
                BedGridView.DataBind();
            }
            // If exception caught show database error
            // and hide gridview
            catch(Exception)
            {
                DataBaseError.Visible = true;
                BedGridView.Enabled = false;
            }
        }
    }
}