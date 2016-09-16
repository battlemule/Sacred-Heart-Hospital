/********************************************************************
 * Visits.aspx.cs                                        v1.0 11/2014
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
        // On page load redirect if not logged in else
        // load data into gridview
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");
            else
            {
                List<Visit> visits = VisitUtility.GetVisits();
                GridView1.DataSource = visits;
                GridView1.DataBind();
            }
        }

        // Find and bind search results to grid view
        protected void SearchClick(object sender, EventArgs e)
        {
            List<Visit> visits = VisitUtility.GetVisits();
            List<Visit> searchReturn = new List<Visit>();
            string choice = radiolist1.SelectedItem.Text;

            if (choice == "Name")
                foreach (Visit visit in visits)
                    if (PatientUtility.GetPatient(visit.patientId).
                        name.ToString().Contains(Search.Text))
                        searchReturn.Add(visit);

            if (choice == "Date of Visit")
                foreach (Visit visit in visits)
                    if (visit.date.ToString().Contains(Search.Text))
                        searchReturn.Add(visit);

            if (choice == "Date of Discharge")
                foreach (Visit visit in visits)
                    if (visit.discharge.ToString().Contains(Search.Text))
                        searchReturn.Add(visit);

            if (searchReturn.Any())
            {
                GridView1.DataSource = searchReturn;
                GridView1.DataBind();
            }
            else
                ErrorMessage.Visible = true;

        }

    }

}