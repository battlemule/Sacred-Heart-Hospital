/********************************************************************
 * DischargeInPatient.aspx.cs                            v1.2 09/2016
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
using SacredHeartHospital.Utility;

namespace SacredHeartHospital
{
    public partial class DischargeInPatient : System.Web.UI.Page
    {
        // On page load redirect if not logged in
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");
        }

        // Validate Patient ID exists in database
        protected void PatientIdValidate(object sender,
            ServerValidateEventArgs e)
        {
            try
            {
                // User input
                int id;

                // If input is an int
                if (int.TryParse(e.Value, out id))
                {
                    // Get patient from id
                    Patient patient = PatientUtility.GetPatient(id);
                    // If not found throw exception
                    if (patient == null)
                        throw new Exception();
                }
                // Validation fails if input not an int
                else
                {
                    throw new Exception();
                }

                // If patient found input is valid
                e.IsValid = true;

            }
            // Input invalid if any exception is caught
            catch (Exception)
            {
                e.IsValid = false;
                return;
            }
        }

        // Validate patient ID has current invisit
        protected void InpatientValidate(object sender,
            ServerValidateEventArgs e)
        {
            try
            {
                int id;

                // // If input is an int
                if (int.TryParse(e.Value, out id))
                {
                    // Create patient from id
                    Patient patient = PatientUtility.GetPatient(id);

                    // If not found fails PatientIdValidate
                    // and invisit validation can halt
                    if (patient == null)
                        return;

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
                }

                // If no valid visit is found throw exception
                throw new Exception();
            }
            // Any exceptions caught input is invalid
            catch(Exception)
            {
                e.IsValid = false;
                return;
            }

        }
        // Return amount Patient owes for current visit if exists
        protected void InfoClick(object sender, EventArgs e)
        {
            // Reset panel
            InpatientGridView.Visible = false;
            DischargePanel.Visible = false;
            AmountOwingLabel.Visible = false;
            PayButton.Visible = false;

            try
            {
                int id;

                // Parse input to int
                int.TryParse(PatientId.Text, out id);

                // Get patient with provided ID
                Patient patient = PatientUtility.GetPatient(id);

                // Get all visits
                List<Visit> visits = VisitUtility.GetVisits();

                // Id of valid visit
                int resultId = 0;

                // Get ID of valid visit
                foreach (Visit visit in visits)
                    if (visit.type == 0 && visit.patientId == id &&
                        visit.discharge == null)
                        resultId = visit.id;

                // If a valid visit is found
                if (resultId != 0)
                {
                    // Create new list to bind gridview to
                    List<InVisit> visit = new List<InVisit>();
                    // Add found visit to list from id
                    visit.Add(VisitUtility.GetInVisit(resultId));

                    // Grey out ID textbox
                    PatientId.Enabled = false;
                    // And info submit button
                    InfoSubmitButton.Enabled = false;

                    // Bind visit to gridview
                    InpatientGridView.DataSource = visit;
                    InpatientGridView.DataBind();
                    // Show gridview
                    InpatientGridView.Visible = true;

                    // Show discharge panel
                    DischargePanel.Visible = true;
                    AmountOwingLabel.Text = String.Format(
                        "Total Price = ${0}", VisitUtility.GetPrice(visit[0]));
                    AmountOwingLabel.Visible = true;

                    // Enable pay button
                    PayButton.Visible = true;
                }
                // If not visit found throw exception
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                // If anything went wrong
                // hide panels, error message
                // will be displayed by validators
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

                // Convert input to int
                int id = Convert.ToInt32(PatientId.Text);

                // Get list of visits
                List<Visit> visits = VisitUtility.GetVisits();
                int visitId = 0;

                // Get ID of valid visit
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
                    // Show success message
                    PayErrorMessage.Visible = false;
                    PaySuccessMessage.Visible = true;
                    // Disable paybutton
                    PayButton.Enabled = false;
                }
                else
                {
                    // Show error if not successful
                    PaySuccessMessage.Visible = false;
                    PayErrorMessage.Visible = true;
                }
            }
            catch (Exception)
            {
                // Show error message if exception caught
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