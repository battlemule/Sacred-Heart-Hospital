/********************************************************************
 * DoctorPatientInfo.aspx.cs                             v1.2 09/2016
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

        // Toggle visibility of panels based off radio button selection
        protected void SelectionToggle(object sender, EventArgs e)
        {
            //Hide gridview when radio button is toggled
            DoctorPatientGridView.Visible = false;

            // If Assign Doctor is chosen show Assign Doctor Panel
            if (SelectionRadioButtonList.SelectedItem.ToString() ==
                "Assign Doctor")
            {
                AssignDoctorPanel.Visible = true;
                DoctorInfoPanel.Visible = false;
            }

            // If History is chosen show DoctorInfo Panel
            if (SelectionRadioButtonList.SelectedItem.ToString() ==
                "Treatment History")
            {
                DoctorInfoPanel.Visible = true;
                AssignDoctorPanel.Visible = false;
            }
        }

        // Toggle visibility of inpatient textboxes based off 
        // patient type selection
        protected void InpatientToggle(object sender, EventArgs e)
        {
            // If Inpatient show Inpatient Panel
            if (PatientTypeRadioButtonList.SelectedItem.ToString() ==
                "Inpatient")
                InpatientPanel.Visible = true;

            // If Outpatient show Outpatient Panel
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
                // Try parse input to int
                if (int.TryParse(e.Value, out id))
                {
                    // Get doctor from database with provided id
                    // using stored procedure
                    Doctor doctor = DoctorUtility.GetDoctor(id);

                    // Valid if found
                    if (doctor != null)
                    {
                        e.IsValid = true;
                        return;
                    }
                }

                // Throw exception if no valid doctor found
                // or input not parsable to int
                throw new Exception();

            }
            // Invalid if any exception caught
            catch (Exception)
            {
                // Hide GridView on invalid search
                // clears any previous completed
                // search results to avoid confusion
                DoctorPatientGridView.Visible = false;
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
                // Try parse input to int
                if (int.TryParse(e.Value, out id))
                {
                    // Get patient from database with provided id
                    // using stored procedure
                    Patient patient = PatientUtility.GetPatient(id);

                    // Valid if found
                    if (patient != null)
                    {
                        e.IsValid = true;
                        return;
                    }
                }

                // Throw exception if no valid doctor found
                // or input not parsable to int
                throw new Exception();
            }
            // Invalid if any exception caught
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

            try
            {
                // Try parse input to int
                if (int.TryParse(e.Value, out id))
                {
                    // Try to get patient from database
                    Patient patient = PatientUtility.GetPatient(id);

                    // If no patient found throw exception
                    if (patient == null)
                        throw new Exception();

                    // Try to get visits from database
                    List<Visit> visits = VisitUtility.GetVisits();

                    // Check eacb visit for patient
                    foreach (Visit visit in visits)
                    {
                        // If patient is found
                        if (visit.patientId == id)
                            // And visit type is invisit
                            if (visit.type == 0)
                                // without a discharge date
                                if (visit.discharge == null)
                                    // patient is busy and fails validation
                                    throw new Exception();
                    }

                    // If input located existing patient and patient
                    // is not current inpatient validation succeeds
                    e.IsValid = true;
                    return;

                }

                // Throw exception if unable to parse input to int
                throw new Exception();

            }
            // Invalid if any exception caught
            catch (Exception)
            {
                e.IsValid = false;
            }
        }

        // Validate Bed Id exists in Database
        protected void BedIdValidate(object sender, ServerValidateEventArgs e)
        {
            try
            {
                int id;
                // Try parse input to int
                if (int.TryParse(e.Value, out id))
                {
                    // Get bed from database with provided id
                    // using stored procedure
                    Bed bed = BedUtility.GetBed(id);

                    // Valid if found
                    if (bed != null)
                    {
                        e.IsValid = true;
                        return;
                    }
                }

                // Throw exception if no valid bed found
                // or input not parsable to int
                throw new Exception();

            }
            // Invalid if any exception caught
            catch (Exception)
            {
                e.IsValid = false;
            }
        }

        // Add new visit submit
        protected void AssignClick(object sender, EventArgs e)
        {
            try
            {
                // Hide error messages when assign button is clicked
                DatabaseError.Visible = false;
                AssignSuccess.Visible = false;
                AssignFail.Visible = false;

                // Get patient and doctor
                Patient patient = PatientUtility.GetPatient(
                    int.Parse(PatientId.Text));
                Doctor doctor = DoctorUtility.GetDoctor(
                    int.Parse(DoctorId.Text));

                // Break if either not found, error messages will
                // be given by validators
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

                    // Attempt to add object to database throw exception
                    // on failure
                    if (!VisitUtility.AddVisit(inVisit))
                        throw new Exception();
                }
                // If outpatient
                else
                {
                    // Set discharge date to visit date
                    string discharge = date;
                    // Create new outvisit object
                    OutVisit outVisit = new OutVisit(visitId, patient.id,
                        visitType, doctor.id, date, discharge);

                    // Attempt to add object to database throw exception
                    // on failure
                    if (!VisitUtility.AddVisit(outVisit))
                        throw new Exception();
                }

                // If no exception thrown operation was a success, show
                // confirmation message and hide errors
                AssignFail.Visible = false;
                AssignSuccess.Visible = true;
                AssignSubmit.Enabled = false;
            }
            // If exception is caught there is an issue with database connection
            // show appropriate error messages
            catch (Exception)
            {
                AssignSuccess.Visible = false;
                AssignFail.Visible = true;
                DatabaseError.Visible = true;
            }
        }

        // Search doctor patient history submit
        protected void InfoClick(object sender, EventArgs e)
        {
            try
            {
                int id;

                // Hide grid and error messages each time submit is clicked
                InfoErrorMessage.Visible = false;
                DoctorPatientGridView.Visible = false;
                DatabaseError.Visible = false;

                // Try parse text field to int
                if (int.TryParse(DoctorId1.Text, out id))
                {
                    // Try get doctor from database with id from
                    // text box
                    Doctor doctor = DoctorUtility.GetDoctor(id);

                    // If no doctor exists return control to
                    // allow validators to produce error messages
                    if (doctor == null)
                        return;
                }
                // if not a valid int return control to allow
                // validators to produce error messages
                else
                    return;

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
            // If an exception is caught here there was an issue
            // with the database and database error is shown
            catch (Exception)
            {
                DatabaseError.Visible = true;
            }
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

        // Return bed name from id for gridview
        protected string GetBedId(object bed)
        {
            // If database has null bed return
            // empty string
            if (bed.ToString() == "Null")
                return "";
            // Else return bed name as string
            else
                return BedUtility.GetBed(Convert.ToInt32(
                    bed.ToString())).name;

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