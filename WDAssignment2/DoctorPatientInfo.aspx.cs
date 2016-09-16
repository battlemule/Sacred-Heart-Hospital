/********************************************************************
 * DoctorPatientInfo.aspx.cs                             v1.0 11/2014
 * Sacred Heart Hospital                                Robert Willis
 *
 * Code Behind File for DoctorPatientInfo.aspx.cs.
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
    public partial class DoctorPatientInfo : System.Web.UI.Page
    {
        // On page load redirect if not logged in
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");
        }

        // Toggle visibility of panels based of radio button selection
        protected void SelectionToggle(object sender, EventArgs e)
        {
            if (SelectionRadioButtonList.SelectedItem.ToString() ==
                "Assign Doctor")
            {
                AssignDoctorPanel.Visible = true;
                DoctorInfoPanel.Visible = false;
            }

            if (SelectionRadioButtonList.SelectedItem.ToString() ==
                "Treatment History")
            {
                DoctorInfoPanel.Visible = true;
                AssignDoctorPanel.Visible = false;
            }
        }

        // Toggle visibility of inpatient textboxes based of 
        // patient type selection
        protected void InpatientToggle(object sender, EventArgs e)
        {

            if (PatientTypeRadioButtonList.SelectedItem.ToString() ==
                "Inpatient")
                InpatientPanel.Visible = true;

            if (PatientTypeRadioButtonList.SelectedItem.ToString() ==
                "Outpatient")
                InpatientPanel.Visible = false;

        }

        // Validate provided doctor ID exists in database
        protected void DoctorIdValidate(object sender,
            ServerValidateEventArgs e)
        {
            try
            {
                int id;
                if (int.TryParse(e.Value, out id))
                {
                    Doctor doctor = DoctorUtility.GetDoctor(id);
                    if (doctor != null)
                        e.IsValid = true;
                    else
                        e.IsValid = false;
                }
                else
                    e.IsValid = false;
            }
            catch (Exception)
            {
                e.IsValid = false;
            }
        }

        // Validate Patient Id exists in Database
        protected void PatientIdValidate(object sender,
            ServerValidateEventArgs e)
        {
            try
            {
                int id;
                if (int.TryParse(e.Value, out id))
                {
                    Patient patient = PatientUtility.GetPatient(id);
                    if (patient != null)
                        e.IsValid = true;
                    else
                        e.IsValid = false;
                }
                else
                    e.IsValid = false;
            }
            catch (Exception)
            {
                e.IsValid = false;
            }
        }

        // Check if patient is a current inpatient
        protected void InpatientValidate(object sender,
            ServerValidateEventArgs e)
        {
            int id;
            bool valid = true;
            // If valid input
            if (int.TryParse(e.Value, out id))
            {
                Patient patient = PatientUtility.GetPatient(id);
                // And patient found
                if (patient != null)
                {
                    // Get visits
                    List<Visit> visits = VisitUtility.GetVisits();
                    // Check visits for patient
                    foreach (Visit visit in visits)
                    {
                        // If found
                        if (visit.patientId == id)
                            // And invisit
                            if (visit.type == 0)
                                // without a discharge date
                                if (visit.discharge == null)
                                {
                                    // patient is busy and fails validation
                                    e.IsValid = false;
                                    valid = false;
                                }
                    }
                    // Valid if patient not busy
                    if (valid)
                        e.IsValid = true;
                }
                // Or doesnt exist (caught elsewhere)
                else
                {
                    e.IsValid = true;
                }
            }
            // Or input was wrong (caught elsewhere)
            else
            {
                e.IsValid = true;
            }
        }

        // Validate Bed Id exists in Database
        protected void BedIdValidate(object sender, ServerValidateEventArgs e)
        {
            try
            {
                int id;
                if (int.TryParse(e.Value, out id))
                {
                    Bed bed = BedUtility.GetBed(id);
                    if (bed != null)
                        e.IsValid = true;
                    else
                        e.IsValid = false;
                }
                else
                    e.IsValid = false;
            }
            catch (Exception)
            {
                e.IsValid = false;
            }
        }

        // Add new visit submit
        protected void AssignClick(object sender, EventArgs e)
        {
            // Get patient and doctor
            Patient patient = PatientUtility.GetPatient(
                int.Parse(PatientId.Text));
            Doctor doctor = DoctorUtility.GetDoctor(
                int.Parse(DoctorId.Text));

            // Break if either not found
            if (patient == null || doctor == null)
                return;

            // Generate pseudo id for object creation 
            // (real id assigned by database)
            int visitId = VisitUtility.GetNewId();
            // Get system date
            string fullDate = DateTime.Now.ToString();
            // Split date from time
            string[] fullDateSplit = fullDate.Split(' ');
            // Split date into 3 parts (D,M,Y)
            string[] dateArray = fullDateSplit[0].Split('/');
            // Recreate date in MM/DD/YYYY format for storing
            string date = String.Format("{0}/{1}/{2} {3}", dateArray[1],
                dateArray[0], dateArray[2], fullDateSplit[1]);
            // Set type to outpatient
            int visitType = 1;

            // If inpatient
            if (PatientTypeRadioButtonList.SelectedItem.ToString() ==
                "Inpatient")
            {
                // Change type
                visitType = 0;
                // Get Bed
                Bed bed = BedUtility.GetBed(int.Parse(Bed.Text));
                // Create new invisit
                InVisit inVisit = new InVisit(visitId, patient.id, visitType,
                    doctor.id, date, "", bed.id);

                // Attempt to write visit to database
                try
                {
                    VisitUtility.AddVisit(inVisit);
                    AssignFail.Visible = false;
                    AssignSuccess.Visible = true;
                    AssignSubmit.Enabled = false;
                }
                catch (Exception)
                {
                    AssignSuccess.Visible = false;
                    AssignFail.Visible = true;
                }

            }
            // If outpatient
            else
            {
                // Set discharge date to visit date
                string discharge = date;
                // Create new outvisit object
                OutVisit outVisit = new OutVisit(visitId, patient.id,
                    visitType, doctor.id, date, discharge);

                // Attempt to add object to database
                try
                {
                    VisitUtility.AddVisit(outVisit);
                    AssignFail.Visible = false;
                    AssignSuccess.Visible = true;
                }
                catch (Exception)
                {
                    AssignSuccess.Visible = false;
                    AssignFail.Visible = true;
                }
            }
        }

        // Search doctor patient history submit
        protected void InfoClick(object sender, EventArgs e)
        {
            // Hide grid and error message each time submit is clicked
            InfoErrorMessage.Visible = false;
            DoctorPatientGridView.Visible = false;

            // Get all visits and instantiate return list
            List<Visit> visits = VisitUtility.GetVisits();
            List<Visit> searchReturn = new List<Visit>();

            // Fill return list with visits provided doctor number
            // attended
            foreach (Visit visit in visits)
                if (visit.doctor.ToString().Contains(DoctorId1.Text))
                    searchReturn.Add(visit);

            // Bind and show grid view if results are found
            if (searchReturn.Any())
            {
                DoctorPatientGridView.DataSource = searchReturn;
                DoctorPatientGridView.DataBind();
                DoctorPatientGridView.Visible = true;
            }
            // Hide grid view and show error message if not
            else
            {
                DoctorPatientGridView.Visible = false;
                InfoErrorMessage.Visible = true;
            }

        }

        // Return discharge date for gridview
        protected string GetDischargeDate(object date)
        {

            if (object.ReferenceEquals(date, DBNull.Value) ||
                object.ReferenceEquals(date, null))
                return "";
            else
                return date.ToString();

        }

        // Return bed name from id for gridview
        protected string GetBedId(object bed)
        {
            if (bed.ToString() == "Null")
                return "";
            else
                return BedUtility.GetBed(Convert.ToInt32(
                    bed.ToString())).name;

        }

        // Return patient type string from int for gridview
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
            return PatientUtility.GetPatient(Convert.ToInt32(
                patientId.ToString())).name;
        }

        // Get doctor name for gridview
        protected string GetDoctorName(object doctorId)
        {
            return DoctorUtility.GetDoctor(Convert.ToInt32(
                doctorId.ToString())).name;
        }

    }
}