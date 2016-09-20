/********************************************************************
 * Registration.aspx.cs                                  v1.2 09/2016
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
using SacredHeartHospital.Utility;

namespace SacredHeartHospital
{
    public partial class Registration : System.Web.UI.Page
    {
        // On Pageload redirect if not logged in
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Global.user] == null)
                Response.Redirect("Login.aspx");
        }

        // Add new patient submit
        protected void PatientClick(object sender, EventArgs e)
        {
            try
            {
                // Hide Error Messages every time button pressed
                PatientSuccessMessage.Visible = false;
                PatientErrorMessage.Visible = false;
                DatabaseErrorMessage.Visible = false;

                // Get form data for new patient
                int newId = PatientUtility.GetNewId();
                string newName = name.Text;
                string newAddress = address.Text;
                string dateOfBirth = dob.Text;
                string newPhone = phone.Text;
                string newEmergency = econtact.Text;

                // Get time and split to Database datetime format
                string time = DateTime.Now.ToString();
                string[] timeSplit = time.Split(' ');
                string[] dateSplit = timeSplit[0].Split('/');
                string registrationTime = String.Format("{0}/{1}/{2} {3}",
                    dateSplit[2], dateSplit[1], dateSplit[0], timeSplit[1]);

                // Create new patient
                Patient patient = new Patient(newId, newName, newAddress, dateOfBirth,
                    newPhone, newEmergency, registrationTime);

                // Attempt to load new patient into database
                if (PatientUtility.AddPatient(patient))
                {
                    PatientSuccessMessage.Visible = true;
                    PatientErrorMessage.Visible = false;
                }
                // Throw exception on database update failure
                else
                {
                    throw new Exception();
                }
            }
            // Display error message if an exception is caught
            catch (Exception)
            {
                PatientSuccessMessage.Visible = false;
                PatientErrorMessage.Visible = true;
                DatabaseErrorMessage.Visible = true;
            }

        }

        // Validate date
        protected void dateValidate(object sender, ServerValidateEventArgs e)
        {
            try
            {
                // Split entered date into day/month/year
                string[] date = ((string)e.Value).Split('/');
                int month = int.Parse(date[0]);
                int day = int.Parse(date[1]);
                int year = int.Parse(date[2]);

                // Make sure month is between 01-12
                if (month > 12 || month < 01)
                    throw new Exception();

                // Make sure day is between 01-31
                if (day > 31 || day < 01)
                    throw new Exception();

                // If September/April/June/November
                if (month == 9 || month == 4 ||
                    month == 6 || month == 11)
                {
                    // Check if date is less than or equal to 30
                    if (day <= 30)
                        throw new Exception();
                }
                // If February
                else if (month == 02)
                {
                    // Check date is less than or equal to 28
                    if (day <= 28)
                        throw new Exception();
                }
                // If any other month
                else
                {
                    // Check date is less than or equal to 31
                    if (day <= 31)
                        throw new Exception();
                }

                // If year is greater than 2016 validation fails
                if (year > 2016)
                    throw new Exception();

                // If year is lesser than 1900 validation fails
                if (year < 1900)
                    throw new Exception();

                // If no exception thrown, input is valid
                e.IsValid = true;

            }
            // Validation fails if an exception is caught
            catch(Exception)
            {
                e.IsValid = false;
            }


        }
    }
}