# Sacred-Heart-Hospital<br />
Hospital Billing and Doctor/Patient Information System<br />
Author: Robert Willis Email: robert@robertgwillis.com <br />
v1.2 09/2016<br />

==================================================================
Sacred Heart Hospital Billing System
==================================================================
==================================================================
Scope
==================================================================

The main goals I wanted to accomplish with this project were
database integration (with a focus on sanitised input and
parameterised queries), verbose user input validation, graceful
error handing and adherence to an object oriented, webform based
design pattern.

All pages are written in asp.net utilising c# codebehind.

JQuery validation is used for payment processing.

Any issues please contact me at: robert@robertgwillis.com

Site available at: http://robertgwillis.com/SacredHeartHospital

==================================================================
Workflow
==================================================================

The landing page will request you to login to continue. Home,
Login and Sitemap are all accessable via navigation links.

Attempting to access any page other than the above will redirect
you to the login page until a global user is set. To log in
use the following credentials:

Username: admin
Password: admin

Once logged in all pages can be accessed via the sitemap. For
individual page inputs and scope see descriptions below.

=================================================================
Pages
=================================================================
----------------------------------
Beds.aspx
----------------------------------
- Fetches all current beds from associated database and displays
  in a grid view
- Does not take input
----------------------------------
DischargeInPatient.aspx
----------------------------------
- Prompts for valid user ID of a patient that is currently in
  the hospital (registered as a patient, has a current invisit,
  current invisit has no discharge date)
- If a valid invisit is found calculates amount owing and
  prompts for payment information
- If payment information is valid patient is discharged and
  discharge date is updated in database

Inputs:

 - Patient ID. Validates to '111'. At least 1 int, Max 3.
 - Credit Card Number. Validates to exactly 16 ints. This
   is just a simple Jquery validation, it does not expect
   a valid credit card number input.
 - Credit Card Name. Validates to 'A A'. 2 string with a
   space in between. At least 1 char and maximum 25 chars
   per string. Validation is performed in Jquery.
 - Credit Card Expiry. Validates to 01/01. 2 ints, forward
   slash, 2 ints. Requires date to be after 08/16. Validation
   performed in Jquery.
 - CSV. Validates to '000' Exactly 3 ints. Validation performed
   in Jquery.

----------------------------------
DoctorPatientInfo.aspx
----------------------------------
- Allows viewing of what doctor is assigned to which patient
  as well as assigning doctors to patients (creating a new
  visit).
- Prompts for doctor id, patient id, visit type (inpatient
  or outpatient), and if inpatient, bed number
- Grabs server time and creates new visit at time of submission

Inputs:

 - Doctor ID. Validates to 1-3 ints. eg: '111'.
 - Patient ID. Same validation as above.
 - Bed ID. Same validation as above.
 
----------------------------------
Doctors.aspx
----------------------------------
- Fetches all current beds from associated database and displays
  in a grid view
- Does not take input

----------------------------------
Login.aspx
----------------------------------
- Takes username and password and checks for user in database
- Username and password are stored in real text. User creation
  and password encryption are out of the scope of this project.
- Use 'admin' 'admin' to log in.

Inputs:

 - Username. Validates field has been entered. No further
   validation.
 - Password. Same as above.
 
----------------------------------
Patients.aspx
----------------------------------
- Fetches all current beds from associated database and displays
  in a grid view
- Has search field. On valid input will display search results
  of patient name in gridview.
  
Inputs:

 - Patient Name. Validates that field has been entered. No
   further validation (no results are displayed if a corresponding
   patient can not be found.
   
----------------------------------
Registration.aspx
----------------------------------
 - Allows for registration of a new patient.
 
 Inputs:
 
  - Name. Validates to 2 strings with a space in between. Minimum
    1 char, max 25 chars both strings. eg: 'A A'.
  - Address. Validates to 1-3 ints, space, 2 strings, minumum 1 char
    maximum 100 chars with a space in between. eg: '1 A A'.
  - Date of birth. Validates to MM/DD/YYYY. Validates that date exists.
    NOTE: Does not recognise leap years.
  - Phone. Validates to exactly 10 ints. eg: '1234567890'.
  - Emergency Contact: Validates text entered and string is 1-255 chars
    long. eg: 'A'.

----------------------------------
Sitemap.aspx
----------------------------------
- Reads Web.sitemap file and creates a tree view of all pages of site.
- Used for navigation once logged in.
- Does not take input.

----------------------------------
Visits.aspx
----------------------------------
- Shows gridview of all registered visits
- Searchable by patient name, date of visit and date of discharge

Inputs

 - Search input. Validates text was entered. If no search results
   found does not display any results in grid view.
  
