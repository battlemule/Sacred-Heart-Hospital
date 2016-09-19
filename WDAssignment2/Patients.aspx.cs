/********************************************************************
 * Patients.aspx.cs                                      v1.0 11/2014
 * Sacred Heart Hospital                                Robert Willis
 *
 * Code Behind File for Patients.aspx.
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
    public partial class Patients : System.Web.UI.Page
    {
        // On page load redirect if not logged in
        // else load data into gridview
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");
            else
            {
                List<Patient> patients = PatientUtility.GetPatients();
                GridView1.DataSource = patients;
                GridView1.DataBind();
            }
        }

        // Find and bind search results to grid view
        protected void SearchClick(object sender, EventArgs e)
        {
            List<Patient> patients = PatientUtility.GetPatients();
            List<Patient> searchReturn = new List<Patient>();

            // Check input name against list of patients
            foreach (Patient patient in patients)
                if (patient.name.ToString().Contains(Search.Text))
                    searchReturn.Add(patient);
            
            // Bind to grid view if any result found
            if (searchReturn.Any())
            {
                GridView1.DataSource = searchReturn;
                GridView1.DataBind();
            }
            // Show error message if no results found
            else
                ErrorMessage.Visible = true;

        }
    }
}