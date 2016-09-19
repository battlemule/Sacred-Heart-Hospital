/********************************************************************
 * Registration.aspx.cs                                  v1.0 11/2014
 * Sacred Heart Hospital                                Robert Willis
 *
 * Code Behind File for Registration.aspx.
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
    public partial class Registration : System.Web.UI.Page
    {
        // On Pageload redirect if not logged in
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");
        }

        // Toggles panel visibility based on radio button choice
        protected void SelectionToggle(object sender, EventArgs e)
        {
            if (SelectionRadioButtonList.SelectedItem.ToString() == "New Patient")
            {
                NewPatientPanel.Visible = true;
                NewVisitPanel.Visible = false;
            }

            if (SelectionRadioButtonList.SelectedItem.ToString() == "New Visit")
            {
                NewVisitPanel.Visible = true;
                NewPatientPanel.Visible = false;
            }
        }

        // Displays extra fields for inpatient when radio button is chosen
        protected void typeToggle(object sender, EventArgs e)
        {
            if (RadioButtonList2.SelectedItem.ToString() == "InPatient")
                inpatientPanel.Visible = true;

            if (RadioButtonList2.SelectedItem.ToString() == "OutPatient")
                inpatientPanel.Visible = false;
        }

        // Add new patient submit
        protected void PatientClick(object sender, EventArgs e)
        {

            int newId = PatientUtility.GetNewId();
            string newName = name.Text;
            string newAddress = address.Text;
            string dateOfBirth = dob.Text;
            string newPhone = phone.Text;
            string newEmergency = econtact.Text;
            string newRegistration = registration.Text;

            // Create new patient
            Patient patient = new Patient(newId, newName, newAddress, dateOfBirth,
                newPhone, newEmergency, newRegistration);

            // Attempt to load new patient into database
            // Display error message if database update did not go through
            if (PatientUtility.AddPatient(patient))
            {
                PatientSuccessMessage.Visible = true;
                PatientErrorMessage.Visible = false;
            }
            else
            {
                PatientSuccessMessage.Visible = false;
                PatientErrorMessage.Visible = true;
            }

        }

        // Add new visit submit
        protected void VisitClick(object sender, EventArgs e)
        {
            // Get patient
            Patient patient = PatientUtility.GetPatient(int.Parse(id.Text));

            // Only commit if patient exists
            if (patient != null)
            {
                int visitId = VisitUtility.GetNewId();
                int visitType;

                // Set visit type to inpatient our outpatient
                if (RadioButtonList2.SelectedItem.ToString() == "InPatient")
                    visitType = 0;
                else
                    visitType = 1;

                // Assign doctor and visit date
                int visitDoctor = int.Parse(doctor.Text);
                string visitEntry = visit.Text;
                string visitDischarge = null;
                
                // If inpatient
                if (visitType == 0)
                {
                    // Assign bed
                    int visitBed = int.Parse(bed.Text);
                    visitDischarge = discharge.Text;

                    // Create new inVisit object
                    InVisit inVisit = new InVisit(visitId, patient.id, visitType,
                        visitDoctor, visitEntry, visitDischarge, visitBed);

                    // Attempt to add visit to database
                    if (VisitUtility.AddVisit(inVisit))
                    {
                        // Show message on success
                        VisitErrorMessage.Visible = false;
                        VisitSuccessMessage.Visible = true;
                    }
                    else
                    {
                        // Show error on failure
                        VisitErrorMessage.Visible = true;
                        VisitSuccessMessage.Visible = false;
                    }
                }
                // If outpatient
                else
                {
                    // Set discharge date to visit date
                    visitDischarge = visitEntry;

                    // Create new outVisit object
                    OutVisit outVisit = new OutVisit(visitId, patient.id, visitType,
                        visitDoctor, visitEntry, visitDischarge);

                    // Attempt to add visit to database
                    if (VisitUtility.AddVisit(outVisit))
                    {
                        // Show message on success
                        VisitErrorMessage.Visible = false;
                        VisitSuccessMessage.Visible = true;
                    }
                    else
                    {
                        // Show error on failure
                        VisitErrorMessage.Visible = true;
                        VisitSuccessMessage.Visible = false;
                    }
                }
                    
            }

        }

        // Custom Validator test for patient exist
        protected void idValidate(object sender, ServerValidateEventArgs e)
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
        
        // Custom Validator test for doctor exist
        protected void doctorValidate(object sender, ServerValidateEventArgs e)
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

        // Custom Validator test for bed exists
        protected void bedValidate(object sender, ServerValidateEventArgs e)
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

        // Date Validator
        protected void dateValidate(object sender, ServerValidateEventArgs e)
        {
            string[] s = ((string)e.Value).Split('/');
            int month = int.Parse(s[0]);
            int day = int.Parse(s[1]);
            int year = int.Parse(s[2]);

            if (month > 12)
                e.IsValid = false;

            if (month < 01)
                e.IsValid = false;

            if (day < 01)
                e.IsValid = false;

            if (month == 9 || month == 4 ||
                month == 6 || month == 11)
            {
                if (day > 30)
                    e.IsValid = false;
            }
            else if (month == 02)
            {
                if (day > 28)
                    e.IsValid = false;
            }
            else
                if (day > 31)
                    e.IsValid = false;

            if (year > 2014)
                e.IsValid = false;

            if (year < 1900)
                e.IsValid = false;

        }

    }
}