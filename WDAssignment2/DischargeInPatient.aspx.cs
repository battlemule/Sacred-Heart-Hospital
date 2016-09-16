/********************************************************************
 * DischargeInPatient.aspx.cs                            v1.0 11/2014
 * Sacred Heart Hospital                                Robert Willis
 *
 * Code Behind File for DischargeInPatient.aspx.
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
    public partial class DischargeInPatient : System.Web.UI.Page
    {
        // On page load redirect if not logged in
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");
        }

        // Validate Patient ID exists in database and has current
        // inpatient visit
        protected void PatientIdValidate(object sender,
            ServerValidateEventArgs e)
        {
            try
            {
                // User input
                int id;

                // If input is not an int throw exception
                if (int.TryParse(e.Value, out id))
                    throw new Exception();

                // Create patient from id
                Patient patient = PatientUtility.GetPatient(id);
                // If failed throw exception
                if (patient == null)
                    throw new Exception();

                // Create list of all visits
                List<Visit> visits = VisitUtility.GetVisits();
                // For each visit in list
                foreach (Visit visit in visits)
                    // If patient id matches and patient
                    // has current invisit
                    if (visit.patientId == patient.id &&
                        visit.type == 0 && visit.discharge == null)
                    {
                        // e is valid
                        e.IsValid = true;
                        return;
                    }

                // Else e is not valid
                throw new Exception();

            }
            catch (Exception)
            {
                e.IsValid = false;
            }
        }

        // Return amount Patient owes for current visit
        protected void InfoClick(object sender, EventArgs e)
        {
            // Reset panel
            InpatientGridView.Visible = false;
            DischargePanel.Visible = false;
            InfoErrorMessage.Visible = false;
            AmountOwingLabel.Visible = false;
            PayButton.Visible = false;

            try
            {
                int id;
                // Previously validated that ID is correct and exists
                int.TryParse(PatientId.Text, out id);
                // Get patient with provided ID
                Patient patient = PatientUtility.GetPatient(id);
                // Get all visits
                List<Visit> visits = VisitUtility.GetVisits();
                // Instantiate valid results list
                // Only inpatients valid
                int resultId = 0;

                // Fill return array with valid visits (guaranteed 1)
                foreach (Visit visit in visits)
                    if (visit.type == 0 && visit.patientId == id &&
                        visit.discharge == null)
                        resultId = visit.id;

                if (resultId != 0)
                {
                    // Bind result to gridview and display price
                    List<InVisit> visit = new List<InVisit>();
                    visit.Add(VisitUtility.GetInVisit(resultId));
                    InfoErrorMessage.Visible = false;
                    // Grey out ID textbox
                    PatientId.Enabled = false;
                    // And info submit button
                    InfoSubmitButton.Enabled = false;
                    InpatientGridView.DataSource = visit;
                    InpatientGridView.DataBind();
                    InpatientGridView.Visible = true;
                    DischargePanel.Visible = true;
                    AmountOwingLabel.Text = String.Format(
                        "Total Price = ${0}", VisitUtility.GetPrice(visit[0]));
                    AmountOwingLabel.Visible = true;
                    PayButton.Visible = true;
                }
                else
                {
                    InfoErrorMessage.Visible = true;
                    InpatientGridView.Visible = false;
                    DischargePanel.Visible = false;
                    PayButton.Visible = false;
                }
            }
            catch (Exception)
            {
                InfoErrorMessage.Visible = true;
                InpatientGridView.Visible = false;
                DischargePanel.Visible = false;
                PayButton.Visible = false;
            }

        }

        // Discharge inpatient
        protected void PayClick(object sender, EventArgs e)
        {
            try
            {
                // Reset messages
                PayErrorMessage.Visible = false;
                PaySuccessMessage.Visible = false;

                int id = Convert.ToInt32(PatientId.Text);
                List<Visit> visits = VisitUtility.GetVisits();
                int visitId = 0;

                // Get Visit ID
                foreach (Visit visit in visits)
                    if (visit.patientId == id && visit.discharge == null)
                        visitId = visit.id;

                // Get Visit from ID
                InVisit thisVisit = VisitUtility.GetInVisit(visitId);

                // Get time and split to Database datetime format
                string dischargeTime = DateTime.Now.ToString();
                string[] timeSplit = dischargeTime.Split(' ');
                string[] dateSplit = timeSplit[0].Split('/');
                string dischargeDate = String.Format("{0}/{1}/{2} {3}",
                    dateSplit[2], dateSplit[1], dateSplit[0], timeSplit[1]);
                // Create discharged patient to update database with
                InVisit update = new InVisit(thisVisit.id, thisVisit.patientId,
                    thisVisit.type, thisVisit.doctor, thisVisit.date,
                    dischargeDate, thisVisit.bed);

                // If database update successfull
                if (VisitUtility.UpdateVisit(update))
                {
                    PayErrorMessage.Visible = false;
                    PaySuccessMessage.Visible = true;
                    // Disable paybutton
                    PayButton.Enabled = false;
                }
                else
                {
                    PaySuccessMessage.Visible = false;
                    PayErrorMessage.Visible = true;
                }
            }
            catch (Exception)
            {
                PaySuccessMessage.Visible = false;
                PayErrorMessage.Visible = true;
            }
        }

        // Show discharge time as now on GridView
        protected string GetDischargeDate(object date)
        {
            return DateTime.Now.ToString();
        }

        // Get bed ID if exists or convert DBNull to Null string
        // for gridview
        protected string GetBedId(object bed)
        {
            if (object.ReferenceEquals(bed, DBNull.Value))
                return "Null";
            else
                return BedUtility.GetBed(Convert.ToInt32(
                    bed.ToString())).name;
        }

        // Get price of visit for GridView
        protected string GetPrice(object bed)
        {
            if (object.ReferenceEquals(bed, DBNull.Value))
                return "Null";
            else
            {
                int price = BedUtility.GetBed(Convert.ToInt32(
                    bed.ToString())).rate;
                return String.Format("${0}", price);
            }

        }

        // Update gridview with patient type name instead of int
        protected string GetPatientType(object patientType)
        {
            if (Convert.ToInt32(patientType.ToString()) == 1)
                return "Outpatient";
            else
                return "Inpatient";
        }

        // Get patient name for gridview
        protected string GetPatientName(object patientId)
        {
            return PatientUtility.GetPatient(
                Convert.ToInt32(patientId.ToString())).name;
        }

        // Get doctor name for gridview
        protected string GetDoctorName(object doctorId)
        {
            return DoctorUtility.GetDoctor(
                Convert.ToInt32(doctorId.ToString())).name;
        }

    }
}