/********************************************************************
 * Patients.aspx.cs                                      v1.2 09/2016
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
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide database error on page load
            DataBaseError.Visible = false;

            // Enable submit button on page load
            SubmitButton.Enabled = true;

            // Redirect to login if not logged in
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");

            // If logged in attempt to pull data from database
            try
            {
                // Bind patient data to gridview
                List<Patient> patients = PatientUtility.GetPatients();
                PatientGridView.DataSource = patients;
                PatientGridView.DataBind();
            }
            // If exception caught show database error and
            // disable submit button
            catch(Exception)
            {
                DataBaseError.Visible = true;
                SubmitButton.Enabled = false;
            }

        }

        // Find and bind search results to grid view
        protected void SearchClick(object sender, EventArgs e)
        {

            List<Patient> patients = PatientUtility.GetPatients();
            List<Patient> searchReturn = new List<Patient>();

            // Hide result not found error message
            NotFoundError.Visible = false;

            // Check input name against list of patients
            foreach (Patient patient in patients)
                if (patient.name.ToString().Contains(Search.Text))
                    searchReturn.Add(patient);
            
            // Bind to grid view if any result found
            if (searchReturn.Any())
            {
                PatientGridView.DataSource = searchReturn;
                PatientGridView.DataBind();
            }
            // Show error message if no results found
            else
                NotFoundError.Visible = true;

        }
    }
}