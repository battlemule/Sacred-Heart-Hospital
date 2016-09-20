/********************************************************************
 * Visits.aspx.cs                                        v1.2 09/2016
 * Sacred Heart Hospital                                Robert Willis
 *
 * Code Behind File for Visits.aspx.
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
    public partial class Visits : System.Web.UI.Page
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
                List<Visit> visits = VisitUtility.GetVisits();
                VisitGridView.DataSource = visits;
                VisitGridView.DataBind();
            }
            // If exception caught show database error and
            // disable submit button
            catch (Exception)
            {
                DataBaseError.Visible = true;
                SubmitButton.Enabled = false;
            }

        }

        // Find and bind search results to grid view based
        // on radio button choice
        protected void SearchClick(object sender, EventArgs e)
        {
            List<Visit> visits = VisitUtility.GetVisits();
            List<Visit> searchReturn = new List<Visit>();
            string choice = radiolist1.SelectedItem.Text;

            // Hide error message
            NotFoundError.Visible = false;

            // If name selected search for name
            if (choice == "Name")
                foreach (Visit visit in visits)
                    if (PatientUtility.GetPatient(visit.patientId).
                        name.ToString().Contains(Search.Text))
                        searchReturn.Add(visit);

            // If date of visit selected search for date
            if (choice == "Date of Visit")
                foreach (Visit visit in visits)
                    if (visit.date.ToString().Contains(Search.Text))
                        searchReturn.Add(visit);

            // If date of discharge selected search for date
            if (choice == "Date of Discharge")
                foreach (Visit visit in visits)
                    if (visit.discharge.ToString().Contains(Search.Text))
                        searchReturn.Add(visit);

            // If results found bind to grid view
            if (searchReturn.Any())
            {
                VisitGridView.DataSource = searchReturn;
                VisitGridView.DataBind();
            }
            // If no results found show error
            else
                NotFoundError.Visible = true;

        }

        // Return discharge date for gridview
        protected string GetDischargeDate(object date)
        {
            // If database has null discharge date return
            // empty string
            if (object.ReferenceEquals(date, DBNull.Value) ||
                object.ReferenceEquals(date, null))
                return "";
            // Else return date as string
            else
                return date.ToString();

        }

        // Return patient type string from int for gridview
        protected string GetPatientType(object patientType)
        {
            // If patient type is 1 return as Outpatient
            if (Convert.ToInt32(patientType.ToString()) == 1)
                return "Outpatient";
            // If patient type is 0 return as Inpatient
            else
                return "Inpatient";

        }

        // Get patient name for gridview
        protected string GetPatientName(object patientId)
        {
            // Return patient name as string
            return PatientUtility.GetPatient(Convert.ToInt32(
                patientId.ToString())).name;
        }

        // Get doctor name for gridview
        protected string GetDoctorName(object doctorId)
        {
            // Return Doctors name as string
            return DoctorUtility.GetDoctor(Convert.ToInt32(
                doctorId.ToString())).name;
        }

    }

}